using Amazon.Runtime;
using AutoMapper;
using Kiosk.Business.Helpers;
using Kiosk.Business.Model.HubSpot;
using Kiosk.Business.Model.Search;
using Kiosk.Business.ViewModels.General;
using Kiosk.Domain.Data;
using Kiosk.Interfaces.Services;
using Kiosk.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
//using StandardExceptionLoggerExtention.ExternalAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Kiosk.Business.Model.Staff.SurveyModel;

namespace Kiosk.Services.HubSpot
{
    public class HubSpotService : ServiceBase, IHubSpotService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _controllerName = "HubSpotService";
        public HubSpotService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HubSpotResponseModel> SearchContact(string email, string phoneNumber)
        {
            HubSpotResponseModel response = new HubSpotResponseModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, HubSpotConfig.SearchContactsApi))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Accept", "application/json");
                        httpRequest.Headers.TryAddWithoutValidation("User-Agent", "curl/7.60.0");

                        //var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("username:password"));
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {HubSpotConfig.Token}");

                        //var request = (dynamic)null;

                        var request = new HubSpotRequestModel();
                        request.limit = HubSpotConfig.HubSpotLimit;
                        request.filterGroups = new List<FilterGroup>();

                        GenerateFilterGroupObject(request, email, phoneNumber, false);

                        GenerateFilterGroupObject(request, email, phoneNumber, true);

                        request.sorts = new List<Sort>();
                        request.sorts.Add(new Sort()
                        {
                            propertyName = HubSpotConfig.CreateDatePropertyName,
                            direction = HubSpotConfig.DescendingDirectionName
                        });

                        request.properties = new List<string>();
                        request.properties.AddRange(HubSpotConfig.SearchContractProperties);

                        var jsonString = JsonConvert.SerializeObject(request);

                        httpRequest.Content = new StringContent(jsonString);
                        httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var httpresponse = await httpClient.SendAsync(httpRequest);
                        if (httpresponse.IsSuccessStatusCode)
                        {
                            string apiContent = httpresponse.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<HubSpotResponseModel>(apiContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "SearchContact(string email, string phoneNumber)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return response;
        }

        public void GenerateFilterGroupObject(HubSpotRequestModel request, string email, string phoneNumber, bool isPhoneUSFormate)
        {
            List<Filter> filter = new List<Filter>();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(phoneNumber))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.EmailPropertyName, @operator = HubSpotConfig.EqualOperator, value = email },
                                new Filter { propertyName = HubSpotConfig.PhonePropertyName, @operator = HubSpotConfig.ContainsTokenOperator, value = !isPhoneUSFormate ? $"*{phoneNumber}" : MakeUSFormattedPhone(phoneNumber) },
                            });
            }
            else if (!string.IsNullOrEmpty(email))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.EmailPropertyName, @operator = HubSpotConfig.EqualOperator, value = email }
                            });
            }
            else if (!string.IsNullOrEmpty(phoneNumber))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.PhonePropertyName, @operator =  HubSpotConfig.ContainsTokenOperator, value = !isPhoneUSFormate ? $"*{phoneNumber}" : MakeUSFormattedPhone(phoneNumber) },
                            });
            }
            request.filterGroups.Add(new FilterGroup
            {
                filters = filter,
            });
        }

        public string MakeUSFormattedPhone(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                return $"({phoneNumber.Substring(0, 3)}) {phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6)}";
            }

            return string.Empty;
        }

        public async Task<int> InsertUpdateContact(ContactModel PostData)
        {
            var PassDurationDate = string.IsNullOrEmpty(PostData.PassDurationDay) ? 0 : (Convert.ToInt32(PostData.PassDurationDay) - 1);
            var expiration_date = PostData.BeginDate == null && PostData.ExpiredDate == null ? DateTime.Now.AddDays(PassDurationDate).ToString() :
                                     PostData.BeginDate != null && PostData.ExpiredDate == null ? PostData.BeginDate?.AddDays(PassDurationDate).ToString() : PostData.ExpiredDate.ToString();

            using (var con = new SqlConnection())
            {
                con.ConnectionString = Convert.ToString(AppSettings.KioskConnectionString);
                try
                {
                    int ContactId = 0;
                    using (var comm = new SqlCommand())
                    {
                        comm.CommandText = "[Kiosk].[usp_SaveContacts_HubSpot]";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Connection = con;
                        comm.CommandTimeout = 600;
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@hs_object_id", DbType = System.Data.DbType.String, Value = PostData.HSId == null ? "" : PostData.HSId });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@firstname", DbType = System.Data.DbType.String, Value = PostData.FirstName == null ? "" : PostData.FirstName });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@lastname", DbType = System.Data.DbType.String, Value = PostData.LastName == null ? "" : PostData.LastName });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@email", DbType = System.Data.DbType.String, Value = PostData.Email == null ? "" : PostData.Email });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@phone", DbType = System.Data.DbType.String, Value = PostData.PhoneNumber == null ? "" : PostData.PhoneNumber });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@address", DbType = System.Data.DbType.String, Value = PostData.AddressLine1 == null ? "" : PostData.AddressLine1 });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@city", DbType = System.Data.DbType.String, Value = PostData.City == null ? "" : PostData.City });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@state", DbType = System.Data.DbType.String, Value = PostData.State == null ? "" : PostData.State });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@zip", DbType = System.Data.DbType.String, Value = PostData.ZipCode == null ? "" : PostData.ZipCode });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@gender", DbType = System.Data.DbType.String, Value = PostData.Gender == null ? "" : PostData.Gender });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@birthdate", DbType = System.Data.DbType.DateTime, Value = PostData.DOB == null ? null : PostData.DOB });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ContactCreatedOn", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ContactCreatedBy", DbType = System.Data.DbType.String, Value = "" });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ContactModifiedOn", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ContactModifiedBy", DbType = System.Data.DbType.String, Value = "" });
                        //comm.Parameters.Add(new SqlParameter { ParameterName = "@IsUpdated", DbType = System.Data.DbType.Boolean, Value = PostData.HS_IsUpdated });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@Source", DbType = System.Data.DbType.String, Value = "DGR" });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@HS_Status", DbType = System.Data.DbType.String, Value = "InQueue" });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@Action", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(PostData.HSId) ? "Create" : "Update" });

                        //Contact Details
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@homeClub", DbType = System.Data.DbType.String, Value = PostData.ClubNumber });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@abcID", DbType = System.Data.DbType.String, Value = PostData.MemberId == null ? "" : PostData.MemberId });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@campaign_details", DbType = System.Data.DbType.String, Value = PostData.EntrySource == null ? "" : PostData.EntrySource });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@sales_person_paycom_id", DbType = System.Data.DbType.String, Value = PostData.salesPersonObj == null ? "" : PostData.salesPersonObj.FullName });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@referring_member_id", DbType = System.Data.DbType.String, Value = PostData.referringMemberId == null ? "" : PostData.referringMemberId });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@referring_member_first", DbType = System.Data.DbType.String, Value = PostData.referringMemberFirst == null ? "" : PostData.referringMemberFirst });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@referring_member_last", DbType = System.Data.DbType.String, Value = PostData.referringMemberLast == null ? "" : PostData.referringMemberLast });

                        comm.Parameters.Add(new SqlParameter { ParameterName = "@dgr_new_contact_record", DbType = System.Data.DbType.Boolean, Value = true });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@agreement_number", DbType = System.Data.DbType.String, Value = "" });


                        if (PostData.isFreePass == true || PostData.isAppointmentTour == true)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_expiration_date", DbType = System.Data.DbType.DateTime, Value = PostData.ExpiredDate == null ? expiration_date : PostData.ExpiredDate });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_activation_date", DbType = System.Data.DbType.DateTime, Value = PostData.BeginDate == null ? DateTime.Now : PostData.BeginDate });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_download_date", DbType = System.Data.DbType.DateTime, Value = PostData.Downloaddate == null ? DateTime.Now : PostData.Downloaddate });
                        }
                        else if (PostData.isFreePass == null && PostData.isAppointmentTour == null)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_expiration_date", DbType = System.Data.DbType.DateTime, Value = null });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_activation_date", DbType = System.Data.DbType.DateTime, Value = null });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_download_date", DbType = System.Data.DbType.DateTime, Value = null });
                        }
                        else
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_expiration_date", DbType = System.Data.DbType.DateTime, Value = PostData.BeginDate == null ? null : PostData.BeginDate });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_activation_date", DbType = System.Data.DbType.DateTime, Value = PostData.ExpiredDate == null ? null : PostData.ExpiredDate });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_download_date", DbType = System.Data.DbType.DateTime, Value = PostData.Downloaddate == null ? null : PostData.Downloaddate });
                        }

                        if (PostData.isFreePass == true || PostData.isAppointmentTour == true)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_duration", DbType = System.Data.DbType.String, Value = PostData.PassDurationDay });
                        }
                        else if (PostData.isFreePass == null && PostData.isAppointmentTour == null)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_duration", DbType = System.Data.DbType.String, Value = null });
                        }
                        else
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_duration", DbType = System.Data.DbType.String, Value = PostData.PassDurationDay == null ? null : PostData.PassDurationDay });
                        }

                        //    comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_expiration_date", DbType = System.Data.DbType.String, Value = 
                        //    PostData.PassDurationDay == null || (string.IsNullOrEmpty(PostData.HSId) && PostData.EntrySource == "walk_in") || PostData.GuestType == "paidPass" ? null : DateTime.Now.AddDays(Convert.ToInt32(PostData.PassDurationDay) - 1).ToString() });

                        //comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_activation_date", DbType = System.Data.DbType.DateTime, Value = 
                        //    string.IsNullOrEmpty(PostData.HSId) && PostData.EntrySource == "walk_in" || PostData.GuestType == "paidPass"  ? null : DateTime.Now });

                        //comm.Parameters.Add(new SqlParameter { ParameterName = "@guest_pass_duration", DbType = System.Data.DbType.String, Value = 
                        //    PostData.PassDurationDay == null || (string.IsNullOrEmpty(PostData.HSId) && PostData.EntrySource == "walk_in") || PostData.GuestType == "paidPass"  ? null : PostData.PassDurationDay });

                        if (PostData.IsKeepMeUpdate)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@phone_opt_in", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@sms_opt_in", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@email_opt_in", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                        }
                        else
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@phone_opt_in", DbType = System.Data.DbType.DateTime, Value = null });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@sms_opt_in", DbType = System.Data.DbType.DateTime, Value = null });
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@email_opt_in", DbType = System.Data.DbType.DateTime, Value = null });
                        }
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@lastmodifieddate", DbType = System.Data.DbType.DateTime, Value = DateTime.Now });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@IsOpenHouse", DbType = System.Data.DbType.Boolean, Value = PostData.isOpenHouse });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@guestPaidPass_Date", DbType = System.Data.DbType.DateTime, Value = PostData.guestPaidPass_Date == null ? null : PostData.guestPaidPass_Date });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@isGuestPaidPass", DbType = System.Data.DbType.Boolean, Value = PostData.isGuestPaidPass });
                        await con.OpenAsync();
                        SqlDataReader reader = await comm.ExecuteReaderAsync();

                        while (reader.Read())
                        {
                            ContactId = !DBNull.Value.Equals(reader["ContactId"]) ? Convert.ToInt32(reader["ContactId"]) : 0;
                        }
                        return ContactId;
                    }
                }
                catch (Exception ex)
                {
                    //ExternalExceptionLogger.LogException(ex, _controllerName, "InsertUpdateContact(ContactModel PostData)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                    return 0;
                }
                finally
                {
                    await con.CloseAsync();
                }
            }
        }

        public async Task<bool> SaveSignatureHubspot(int Id, string HSId, string ImageSrc, string userName, DateTime ClubDate, string ClubTimeZone, bool IsReceiveTextMessages, bool IsKeepMeUpdate, bool ShowReceiveTextMessages, bool ShowKeepMeUpdate, bool IsMember, string InitialImageSrc, long? LeadId, string ImagePath)
        {
            string authorName = ImageSrc.Substring(22);
            byte[] bytes = Convert.FromBase64String(authorName);

            string InitialauthorName = InitialImageSrc != null ? InitialImageSrc.Substring(22) : null;
            byte[] Initialbytes = InitialauthorName != null ? Convert.FromBase64String(InitialauthorName) : null;

            using (var con = new SqlConnection())
            {
                con.ConnectionString = Convert.ToString(AppSettings.KioskConnectionString);
                try
                {
                    using (var comm = new SqlCommand())
                    {
                        await con.OpenAsync();
                        comm.CommandText = "[Kiosk].[usp_InsertSignature_HubSpot]";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Connection = con;
                        comm.CommandTimeout = 600;
                        if (Id > 0)
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@ContactId", DbType = System.Data.DbType.Int32, Value = Id });
                        }
                        if (!string.IsNullOrEmpty(HSId))
                        {
                            comm.Parameters.Add(new SqlParameter { ParameterName = "@hs_object_id", DbType = System.Data.DbType.String, Value = HSId });
                        }
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@IsKeepMeUpdate", DbType = System.Data.DbType.Boolean, Value = IsKeepMeUpdate }); ;
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@IsReceiveTextMessages", DbType = System.Data.DbType.Boolean, Value = IsReceiveTextMessages });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ShowKeepMeUpdate", DbType = System.Data.DbType.Boolean, Value = ShowKeepMeUpdate });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@ShowReceiveTextMessages", DbType = System.Data.DbType.Boolean, Value = ShowReceiveTextMessages });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@IsMember", DbType = System.Data.DbType.Boolean, Value = ShowReceiveTextMessages });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@LeadId", DbType = System.Data.DbType.Int32, Value = LeadId });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SignatureName", DbType = System.Data.DbType.String, Value = string.Format("{0} Signature", DateTime.Now.ToString("MM/dd/yyyy HH:mm tt")) });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SignatureContentType", DbType = System.Data.DbType.String, Value = "image/png" });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SignatureBodyLength", DbType = System.Data.DbType.Int32, Value = bytes.Length });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SignatureBody", DbType = System.Data.DbType.Binary, Value = bytes });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SignatureImagePath", DbType = System.Data.DbType.String, Value = ImagePath });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@InitialSignatureName", DbType = System.Data.DbType.String, Value = InitialImageSrc != null ? string.Format("{0} InitialSignature", DateTime.Now.ToString("MM/dd/yyyy HH:mm tt")) : null });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@InitialSignatureContentType", DbType = System.Data.DbType.String, Value = InitialImageSrc != null ? "image/png" : null });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@InitialSignatureBodyLength", DbType = System.Data.DbType.Int32, Value = Initialbytes == null ? 0 : Initialbytes.Length });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@InitialSignatureBody", DbType = System.Data.DbType.Binary, Value = InitialImageSrc != null ? Initialbytes : null });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@InitialSignatureImagePath", DbType = System.Data.DbType.String, Value = InitialImageSrc != null ? ImagePath : null });

                        var count = await comm.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                   await con.CloseAsync();
                }
            }
        }

        #region Check Email Use By Other Contact
        public async Task<ContactEmailResponseModel> CheckEmailUseByOtherContact(string email)
        {
            ContactEmailResponseModel response = new ContactEmailResponseModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, HubSpotConfig.SearchContactsApi))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Accept", "application/json");
                        httpRequest.Headers.TryAddWithoutValidation("User-Agent", "curl/7.60.0");
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {HubSpotConfig.Token}");

                        List<EmailFilter> filter = new List<EmailFilter>();
                        filter.AddRange(new List<EmailFilter> {
                            new EmailFilter { propertyName = HubSpotConfig.EmailPropertyName, @operator = HubSpotConfig.EqualOperator, value = email }
                        });

                        var request = (dynamic)null;
                        request = new CheckContactEmailHSRequestModel();
                        request.limit = HubSpotConfig.HubSpotLimit;
                        request.filterGroups = new List<EmailFilterGroup>();
                        request.filterGroups.Add(new EmailFilterGroup
                        {
                            filters = filter,
                        });

                        request.sorts = new List<EmailSort>();
                        request.sorts.Add(new EmailSort()
                        {
                            propertyName = HubSpotConfig.CreateDatePropertyName,
                            direction = HubSpotConfig.DescendingDirectionName
                        });

                        request.properties = new List<string>();
                        request.properties.AddRange(HubSpotConfig.SearchContractProperties);

                        var jsonString = JsonConvert.SerializeObject(request);

                        httpRequest.Content = new StringContent(jsonString);
                        httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var httpresponse = await httpClient.SendAsync(httpRequest);
                        if (httpresponse.IsSuccessStatusCode)
                        {
                            string apiContent = httpresponse.Content.ReadAsStringAsync().Result;
                            response = JsonConvert.DeserializeObject<ContactEmailResponseModel>(apiContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "CheckEmailUseByOtherContact(string email)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return response;
        }
        #endregion

        public async Task<RestResponse> UpdateEmailInHS(UpdateEmail postdata)
        {
            RestResponse response = new RestResponse();
            try
            {
                if (!string.IsNullOrEmpty(postdata.email))
                {
                    var client = new RestClient(HubSpotConfig.HubSpotDomain);
                    var request = new RestRequest(String.Format(HubSpotConfig.UpdateContactUrl, postdata.HsId), Method.Post);
                    request.AddHeader("Authorization", $"Bearer {HubSpotConfig.Token}");
                    request.AddHeader("ContentType", "application/Json");
                    request.RequestFormat = DataFormat.Json;

                    request.AddBody(new
                    {
                        properties = new[] {
                                new { property = "email", value = postdata.email},
                            }
                    });
                    var requestJsonbody = JsonConvert.SerializeObject(request);
                    response = client.Execute(request);
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateEmailInHS(UpdateEmail postdata)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return response;
        }


        public async Task<AppointmentModel> UpdateAppointment(string HubSpotId)
        {
            AppointmentModel response = new AppointmentModel();
            try
            {
                var client = new RestClient(HubSpotConfig.HubSpotDomain);
                var request = new RestRequest(string.Format(HubSpotConfig.GetAppointmentIdAPI, HubSpotId));
                request.AddHeader("Authorization", $"Bearer {HubSpotConfig.Token}");
                var res = client.ExecuteGet(request);

                if (res.IsSuccessStatusCode)
                {
                    var apptIds = JsonConvert.DeserializeObject<AppointmentIdResponse>(res.Content);
                    if (apptIds.results.Any())
                    {
                        response = GetAppointmentDetail(apptIds.results.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateAppointment(string HubSpotId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return response;
        }


        public AppointmentModel GetAppointmentDetail(List<AppointmentIdResult> postData)
        {
            AppointmentModel response = new AppointmentModel();

            try
            {
                var client = new RestClient(HubSpotConfig.HubSpotDomain);
                var request = new RestRequest(string.Format(HubSpotConfig.GetAppointmentDetail));
                request.AddHeader("Authorization", $"Bearer {HubSpotConfig.Token}");

                var reqJson = new AppointmentRequest();
                reqJson.filterGroups = new List<AppointmentRequestFilterGroup>();

                foreach (var item in postData)
                {
                    List<AppointmentRequestFilter> filter = new List<AppointmentRequestFilter>();
                    filter.AddRange(new List<AppointmentRequestFilter> {
                                        new AppointmentRequestFilter { propertyName = HubSpotConfig.HSIdField, @operator =  HubSpotConfig.EqualOperator, value = item.id },
                                    });

                    reqJson.filterGroups.Add(new AppointmentRequestFilterGroup
                    {
                        filters = filter,
                    });
                }

                reqJson.properties = new List<string>();
                reqJson.properties.AddRange(HubSpotConfig.AppointmentProperties);

                request.AddBody(new AppointmentRequest
                {
                    filterGroups = reqJson.filterGroups,
                    properties = reqJson.properties
                });

                var res = client.ExecutePost(request);

                if (res.IsSuccessStatusCode)
                {
                    var apptDetail = JsonConvert.DeserializeObject<AppointmentDetailResponse>(res.Content);
                    AppointmentDetailResponseResult filterApptDetail = apptDetail.results
                                .Where(x => Convert.ToDateTime(x.properties.appointment_date).Date == DateTime.Today)
                                .OrderBy(o => o.properties.appointment_date).FirstOrDefault();

                    if (filterApptDetail != null && !string.IsNullOrEmpty(filterApptDetail.properties.hs_object_id))
                    {
                        response = UpdateAppointmentStatus(filterApptDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "GetAppointmentDetail(List<AppointmentIdResult> postData)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }

            return response;
        }
        public AppointmentModel UpdateAppointmentStatus(AppointmentDetailResponseResult postData)
        {
            AppointmentModel response = new AppointmentModel();

            try
            {
                var client = new RestClient(HubSpotConfig.HubSpotDomain);
                var request = new RestRequest(string.Format(HubSpotConfig.UpdateApptStatusAPI, postData.properties.hs_object_id), Method.Patch);
                request.AddHeader("Authorization", $"Bearer {HubSpotConfig.Token}");

                var reqJson = new UpdateApptStatusRequest
                {
                    properties = new UpdateApptStatusRequestProperties
                    {
                        appointment_status = HubSpotConfig.ApptStatusShowValue
                    }
                };
                request.AddParameter("application/json", JsonConvert.SerializeObject(reqJson), ParameterType.RequestBody);
                request.AddBody(new UpdateApptStatusRequest
                {
                    properties = new UpdateApptStatusRequestProperties
                    {
                        appointment_status = HubSpotConfig.ApptStatusShowValue
                    }
                });

                var res = client.Execute(request);

                if (res.IsSuccessStatusCode)
                {
                    UpdateApptStatusResponse updatedStatusResponse = JsonConvert.DeserializeObject<UpdateApptStatusResponse>(res.Content);

                    response = new AppointmentModel
                    {
                        appt_date = postData.properties.appointment_date,
                        appointment_type = postData.properties.appointment_subject,
                        appt_status = updatedStatusResponse != null && updatedStatusResponse.properties != null ? updatedStatusResponse.properties.appointment_status : null
                    };
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateAppointmentStatus(AppointmentDetailResponseResult postData)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }

            return response;
        }

        #region Survey
        public bool InsertHSSurveyDetails(SurveyQLA data)
        {
            SurveyResponseModel SD = new SurveyResponseModel();
            string XMLData = GetHSSurveyInformationXML(data);
            using (var con = new SqlConnection())
            {
                con.ConnectionString = Convert.ToString(AppSettings.KioskConnectionString);
                try
                {
                    using (var comm = new SqlCommand())
                    {
                        comm.CommandText = "[Kiosk].[usp_InsertDeleteSurveyQuestionsdetails]";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Connection = con;
                        comm.CommandTimeout = 600;

                        comm.Parameters.Add(new SqlParameter { ParameterName = "@SurveyQLADetailsXml", DbType = System.Data.DbType.Xml, Value = XMLData });
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@Createdby", DbType = System.Data.DbType.String, Value = "" });

                        con.Open();
                        var count = comm.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    //ExternalExceptionLogger.LogException(ex, _controllerName, "InsertHSSurveyDetails(SurveyQLA data)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                    return false;
                }
            }
        }

        private string GetHSSurveyInformationXML(SurveyQLA response)
        {
            var str = new XElement("SurveyQLADetails",
                            from m in response.SurveyObjDetails.QLAList
                            select new XElement("QLADetailsModel",
                                new XElement("FirstName", response.SurveyObjDetails.FirstName),
                                new XElement("LastName", response.SurveyObjDetails.LastName),
                                new XElement("PhoneNumber", response.SurveyObjDetails.PhoneNumber),
                                new XElement("Email", response.SurveyObjDetails.Email),
                                new XElement("ClubNumber", response.SurveyObjDetails.ClubNumber),
                                new XElement("ContactId", response.SurveyObjDetails.ContactId),
                                new XElement("hs_object_id", response.SurveyObjDetails.hs_object_id),
                                new XElement("QuestionId", m.QuestionId),
                                new XElement("OptionId", m.OptionId),
                                new XElement("QuestionsTypeName", m.QuestionsTypeName),
                                new XElement("AnswerName", m.AnswerName),
                                new XElement("SalesPerson", m.SalesPerson)
                                ));
            return Regex.Replace(str.ToString(System.Xml.Linq.SaveOptions.DisableFormatting), @"\t|\n|\r", "");
        }
        #endregion Survey


        public async Task<HubSpotResponseModel> SearchGuestByLimeCardMember(string memberId)
        {
            HubSpotResponseModel response = new HubSpotResponseModel();
            try
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, HubSpotConfig.SearchContactsApi))
                        {
                            httpRequest.Headers.TryAddWithoutValidation("Accept", "application/json");
                            httpRequest.Headers.TryAddWithoutValidation("User-Agent", "curl/7.60.0");

                            //var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("username:password"));
                            httpRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {HubSpotConfig.Token}");

                            //var request = (dynamic)null;

                            var request = new HubSpotRequestModel();
                            request.limit = HubSpotConfig.HubSpotLimit;
                            request.filterGroups = new List<FilterGroup>();

                            GenerateFilterGroupObjectForLimeCard(request, memberId);

                            request.sorts = new List<Sort>();
                            request.sorts.Add(new Sort()
                            {
                                propertyName = HubSpotConfig.CreateDatePropertyName,
                                direction = HubSpotConfig.DescendingDirectionName
                            });

                            request.properties = new List<string>();
                            request.properties.AddRange(HubSpotConfig.SearchContractProperties);

                            var jsonString = JsonConvert.SerializeObject(request);

                            httpRequest.Content = new StringContent(jsonString);
                            httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                            var httpresponse = await httpClient.SendAsync(httpRequest);
                            if (httpresponse.IsSuccessStatusCode)
                            {
                                string apiContent = httpresponse.Content.ReadAsStringAsync().Result;
                                response = JsonConvert.DeserializeObject<HubSpotResponseModel>(apiContent);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    //ExternalExceptionLogger.LogException(ex, _controllerName, "SearchGuestByLimeCardMember(string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "SearchGuestByLimeCardMember(string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return response;
        }

        public void GenerateFilterGroupObjectForLimeCard(HubSpotRequestModel request, string memberid)
        {
            List<Filter> filter = new List<Filter>();
            if (!string.IsNullOrEmpty(memberid))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.ReferringMemberIDProperty, @operator = HubSpotConfig.EqualOperator, value = memberid }
                            });
            }
            request.filterGroups.Add(new FilterGroup
            {
                filters = filter,

            });
        }

        public void FilterGroupObject(SearchContactRequestModel request, string memberId, string hs_objectId)
        {
            List<Filter> filter = new List<Filter>();
            if (!string.IsNullOrEmpty(memberId))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.MemberIDProperty, @operator = HubSpotConfig.EqualOperator, value = memberId }
                            });
            }
            else if (!string.IsNullOrEmpty(hs_objectId))
            {
                filter.AddRange(new List<Filter> {
                                new Filter { propertyName = HubSpotConfig.HSObjectId, @operator = HubSpotConfig.EqualOperator, value = hs_objectId }
                            });
            }
            request.filterGroups.Add(new FilterGroup
            {
                filters = filter,
            });
        }

    }
}
