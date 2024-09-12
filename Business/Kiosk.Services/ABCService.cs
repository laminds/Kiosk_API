using AutoMapper;
using Kiosk.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosk.Interfaces.Services;
using Kiosk.Business.Model.ABCResponseModels;
using System.Data;
using Kiosk.Business.Helpers;
using Kiosk.Services.Security;
using Kiosk.Business.Extension;
using Newtonsoft.Json;
using System.Net;
using Kiosk.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Kiosk.Business.ViewModels.ABC;
using System.IO;
using Hangfire;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using Kiosk.Business.Model.Plans;
using Castle.Components.DictionaryAdapter.Xml;
using Kiosk.Interfaces.Repositories;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Business.Model.Checkout;
using Kiosk.Business.Model.CreditCard;
using System.Net.Http;
using System.Text.RegularExpressions;
using Kiosk.Business.Model.ManageMembership;
using Kiosk.Business.Model.HubSpot;
using Amazon.Runtime;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Kiosk.Domain.Models;
using Hangfire.Logging;
using System.Reflection;
using ThirdParty.Json.LitJson;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;

namespace Kiosk.Services
{
    public partial class ABCService : ServiceBase, IABCService
    {
        public static KioskContext _context;
        public static IPlanRepository _planRepository;
        public static Logger _logger;
        public static ABCLoggerService _abclogger;

        public ABCService(IUnitOfWork unitOfWork, IMapper mapper, IPlanRepository planRepository) : base(unitOfWork, mapper)
        {
            _context = context;
            _planRepository = planRepository;
        }

        public static Task<ABCCredentialModel> ABC_CredentialData = GetABCCredential();
        private static string _controllerName = "ABCService";

        public static async Task<ABCCredentialModel> GetABCCredential()
        {
            List<ABCCredentialModel> Result = new List<ABCCredentialModel>();
            //checkOutMessageLogDetail("GetABC Credential (Exception) : ", "Get ABC Credential", "", null, null, null, null, null);

            using (var con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = Convert.ToString(AppSettings.KioskConnectionString);
                    using (var comm = new SqlCommand())
                    {
                        comm.CommandText = "[Kiosk].[usp_GetApiSettingsByKey]";
                        comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Connection = con;
                        comm.CommandTimeout = 600;
                        comm.Parameters.Add(new SqlParameter { ParameterName = "@apiname", DbType = System.Data.DbType.String, Value = ABC.APIname });
                        con.Open();
                        SqlDataReader reader = comm.ExecuteReader();
                        while (await reader.ReadAsync())
                        {
                            Result.Add(new ABCCredentialModel
                            {
                                username = !DBNull.Value.Equals(reader["username"]) ? Convert.ToString(reader["username"]) : "",
                                password = !DBNull.Value.Equals(reader["password"]) ? AESCryptography.Decrypt(Convert.ToString(reader["password"])) : "",
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ExternalExceptionLogger.LogException(ex, _controllerName, "GetABCCredential()", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                }
            }
            return Result.Any() ? Result[0] : null;
        }
        public async Task<List<RecurringServicePlan>> GetPTPlans(string clubNumber, List<RecurringServicePlan> recurringServicePlans)
        {
            List<RecurringServicePlan> Response = new List<RecurringServicePlan>();
            try
            {
                bool BlnBreak = false;
                int reAttemptIndex = 1;

                while (!BlnBreak)
                {
                    var ResponseData = await GetClubWisePlanListFromABC(clubNumber, recurringServicePlans);
                    if (ResponseData != null && ResponseData.Response != null && ResponseData.Response != null && ResponseData.Response.Any())
                    {
                        Response = ResponseData.Response;
                    }

                    if (ResponseData.Reattempt.HasValue && ResponseData.Reattempt.Value)
                    {
                        reAttemptIndex++;
                        if (reAttemptIndex < 4)
                            continue;
                    }

                    if (reAttemptIndex >= 4)
                    {
                        BlnBreak = true;
                    }
                    if (ResponseData.StopRequest.HasValue && ResponseData.StopRequest.Value)
                    {
                        BlnBreak = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber);
                //ExternalExceptionLogger.LogException(ex, _controllerName, "GetPTPlans(string clubNumber)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return Response;
        }

        public async static Task<ABCClubPlanResponseExceptionModel> GetClubWisePlanListFromABC(string clubNumber, List<RecurringServicePlan> recurringServicePlans)
        {
            try
            {
                if (recurringServicePlans != null && recurringServicePlans.Count > 0)
                {
                    recurringServicePlans = recurringServicePlans.Where(x => (!x.recurringServicePlanName.ToLower().Contains(" vfp ") && !x.recurringServicePlanName.ToLower().StartsWith("vfp")) && !x.recurringServicePlanName.ToLower().Contains("youfit on demand")).ToList();
                    foreach (var plan in recurringServicePlans)
                    {
                        plan.originalRecurringServicePlanName = plan.recurringServicePlanName;
                        plan.PlainRecurringServicePlanName = plan.recurringServicePlanName;
                        plan.recurringServicePlanName = plan.recurringServicePlanName.GetPlainPlan();

                        var planObj = await GetPlanDetailById(plan, clubNumber);

                        plan.billing = planObj.billing;
                        plan.purchaseToday = planObj.purchaseToday;
                        plan.validationHash = planObj.validationHash;
                        plan.totalValue = plan.recurringServicePlanName.Contains("SGT Addon Web") && planObj.billing != null ? Convert.ToInt32(planObj.billing.totalInvoicePricisionValue.Replace(",", "")) : planObj.purchaseToday != null ? Convert.ToInt32(planObj.purchaseToday.totalInvoicePricisionValue.Replace(",", "")) : 0;
                        plan.recurringServicePlanId = plan.recurringServicePlanName.Contains("SGT Addon Web") && planObj.billing != null ? planObj.recurringServicePlanId : planObj.purchaseToday != null ? planObj.recurringServicePlanId : null;

                        plan.totalInvoicePricisionValue = plan.recurringServicePlanName.Contains("SGT Addon Web") && planObj.billing != null ? plan.billing.totalInvoicePricisionValue : planObj.purchaseToday != null ? plan.purchaseToday.totalInvoicePricisionValue : null;

                        plan.totalInvoiceScaleValue = plan.recurringServicePlanName.Contains("SGT Addon Web") && planObj.billing != null ? plan.billing.totalInvoiceScaleValue : planObj.purchaseToday != null ? plan.purchaseToday.totalInvoiceScaleValue : null;

                        plan.unitPrice = plan.recurringServicePlanName.Contains("SGT Addon Web") && planObj.billing != null ? plan.billing.unitPrice : planObj.purchaseToday != null ? plan.purchaseToday.unitPrice : null;
                        plan.ProcessingFee = planObj.ProcessingFee;

                        if (plan.billing != null && plan.billing.invoiceTotal == "0.01")
                        {
                            plan.billing.invoiceTotal = "0.00";
                        }
                    }
                    return new ABCClubPlanResponseExceptionModel() { Response = recurringServicePlans, StopRequest = true };
                }
                else
                {
                    return new ABCClubPlanResponseExceptionModel() { Response = recurringServicePlans, StopRequest = true };
                }

            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber);
                return new ABCClubPlanResponseExceptionModel() { StopRequest = ex.Message.Contains("401") || ex.Message.Contains("500"), Reattempt = true };
            }
        }

        public async static Task<RecurringServicePlan> GetPlanDetailById(RecurringServicePlan plan, string clubNumber)
        {
            try
            {
                var responseData = JsonConvert.DeserializeObject<ABCClubPlansDetailsResponseModel>(plan.PlanDetailsJson);
                if (responseData != null && responseData.recurringServicePlanDetail != null)
                {
                    plan = responseData.recurringServicePlanDetail;

                    string[] separatePrice = plan.purchaseToday.unitPrice.Replace("$", "").Split('.');
                    plan.purchaseToday.unitPricisionValue = separatePrice[0];
                    plan.purchaseToday.unitScaleValue = separatePrice.Length > 1 ? separatePrice[1] : "";

                    string[] separateToalPrice = plan.purchaseToday.totalServiceQuantity.Replace("$", "").Split('.');
                    plan.purchaseToday.totalInvoicePricisionValue = separateToalPrice[0];
                    plan.purchaseToday.totalInvoiceScaleValue = separateToalPrice.Length > 1 ? separateToalPrice[1] : "";

                    string[] billingSeparatePrice = plan.billing.unitPrice.Replace("$", "").Split('.');
                    plan.billing.unitPricisionValue = billingSeparatePrice[0];
                    plan.billing.unitScaleValue = billingSeparatePrice.Length > 1 ? billingSeparatePrice[1] : "";

                    string[] billingSeparateToalPrice = plan.billing.invoiceTotal.Replace("$", "").Split('.');
                    plan.billing.totalInvoicePricisionValue = billingSeparateToalPrice[0];
                    plan.billing.totalInvoiceScaleValue = billingSeparateToalPrice.Length > 1 ? billingSeparateToalPrice[1] : "";

                    if (plan.additionalCatalogItems != null && plan.additionalCatalogItems.Count > 0)
                    {
                        plan.ProcessingFee = plan.additionalCatalogItems.Where(q => q.additionalCatalogItemName.ToLower().Contains("processing fee")).Select(q => q.additionalCatalogItemAmount).FirstOrDefault();
                    }
                }
                return plan;
            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber + ", palnId = " + plan.recurringServicePlanId);
                return plan;
            }
        }

        public async Task<ProspectResponse> InsertABCProspect(string clubNumber, string firstName, string lastName, string email, string phoneNumber, string gender, string referringMemberId)
        {
            ProspectResponse Response = new ProspectResponse();
            checkOutMessageLogDetail("Insert ABC Prospect : ", "Insert ABC Prospect", "", firstName, lastName, email, null, clubNumber);
            try
            {
                var currentUrl = string.Format(ABC.ABCProspectURL, clubNumber);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                ProspectRequest request = new ProspectRequest();

                PersonalModel personalModel = new PersonalModel();
                personalModel.memberId = "";
                personalModel.firstName = firstName.GetString();
                personalModel.lastName = lastName.GetString();
                personalModel.email = email;
                personalModel.mobilePhone = phoneNumber;
                personalModel.primaryPhone = phoneNumber;
                personalModel.gender = gender;

                AgreementModel agreementModel = new AgreementModel();
                agreementModel.beginDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                if (referringMemberId != "" || referringMemberId != null)
                {
                    agreementModel.referringMemberId = referringMemberId;
                }
                ProspectModel prospectModel = new ProspectModel();
                prospectModel.personal = personalModel;
                prospectModel.agreement = agreementModel;

                request.prospects = new ProspectsModel[] { new ProspectsModel { prospect = prospectModel } };

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonData = JsonConvert.SerializeObject(request);

                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Response = JsonConvert.DeserializeObject<ProspectResponse>(result);
                        checkOutMessageLogDetail("Insert ABC Prospect : ", httpResponse.StatusCode.ToString().ToUpper(), "", firstName, lastName, email, null, clubNumber);
                        return Response;
                    }
                }
            }
            catch (WebException e)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    var result = r.ReadToEnd();

                    Response.status = new ProspectStatusModel();
                    Response.status.message = result;
                }
                checkOutMessageLogDetail("Insert ABC Prospect : ", e.ToString(), "", firstName, lastName, email, null, clubNumber);
                _abclogger.SendEmail(e, "clubNumber = " + clubNumber + ", firstName = " + firstName + ", lastName = " + lastName + ", email = " + email + ", phoneNumber = " + phoneNumber);
            }
            catch (Exception ex)
            {
                Response.status = new ProspectStatusModel();
                Response.status.message = ex.ToString();
                checkOutMessageLogDetail("Insert ABC Prospect : ", ex.ToString(), "", firstName, lastName, email, null, clubNumber);
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber + ", firstName = " + firstName + ", lastName = " + lastName + ", email = " + email + ", phoneNumber = " + phoneNumber);
            }
            return Response;
        }

        public async Task<string> UploadContractFile(int club, string Email, string PhoneNumber, string fileName, string filePath)
        {
            string bucketName = AmazonSettings.AmazonS3BucketName_Contract;
            if (Convert.ToBoolean(AmazonSettings.CanSaveBucketToDateFolder))
            {
                bucketName = string.Format(@"{0}/{1}/{2}/{3}/{4}", bucketName, club, DateTime.Today.Year, DateTime.Today.ToString("MM"), DateTime.Today.ToString("dd"));
            }
            await UploadFile(club, Email, PhoneNumber, fileName, filePath, bucketName);
            return bucketName;
        }

        public async Task<string> UploadDisclaimerFile(int club, string Email, string PhoneNumber, string fileName, string filePath)
        {

            string bucketName = AmazonSettings.AmazonS3BucketName_Disclaimer;
            if (Convert.ToBoolean(AmazonSettings.CanSaveBucketToDateFolder))
            {
            }
            await UploadFile(club, Email, PhoneNumber, fileName, filePath, bucketName);
            return bucketName;
        }

        public async static Task<bool> UploadFile(int club, string Email, string PhoneNumber, string fileName, string filePath, string bucketName)
        {
            try
            {
                string AWSAccessKey = AmazonSettings.AWSAccessKey;
                string AWSSecretKey = AmazonSettings.AWSSecretKey;
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(AWSAccessKey, AWSSecretKey);
                RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
                IAmazonS3 s3Client = new AmazonS3Client(awsCredentials, bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);

                //if (Convert.ToBoolean(ConfigurationManager.AppSettings["CanSaveBucketToDateFolder"]))
                //{
                //    bucketName = string.Format(@"{0}/{1}/{2}/{3}/{4}", bucketName, club, DateTime.Today.Year, DateTime.Today.ToString("MM"), DateTime.Today.ToString("dd"));
                //}

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    Key = fileName,
                    StorageClass = S3StorageClass.StandardInfrequentAccess
                };
                fileTransferUtilityRequest.Metadata.Add("PhoneNumber", PhoneNumber);
                fileTransferUtilityRequest.Metadata.Add("Email", Email);
                fileTransferUtility.Upload(fileTransferUtilityRequest);

                File.Delete(filePath);
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                _abclogger.SendEmail(ex, "club = " + club.ToString() + ", Email = " + Email + ", PhoneNumber = " + PhoneNumber + ", fileName = " + fileName + ", filePath = " + filePath);
                throw ex;
            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "club = " + club.ToString() + ", Email = " + Email + ", PhoneNumber = " + PhoneNumber + ", fileName = " + fileName + ", filePath = " + filePath);
                throw ex;
            }
        }

        public async Task<ABCMemberPlanInitialModel> GetMemberPlansFromABC(string clubNumber, InitialPlanModel planName)
        {
            try
            {
                ABCMemberPlanResponseModel responseData = new ABCMemberPlanResponseModel();
                responseData.plans = planName.AllPlans;
                if (responseData != null && responseData.plans != null && responseData.plans.Count > 0)
                {
                    #region Separate monthly and yearly plan
                    responseData.InitialPlanList = await SeparateMemberShipPlan(responseData.plans, planName.PlanDetails);
                    #endregion

                    var isFlashSaleFlag = responseData.InitialPlanList.Where(x => x.TabName.ToLower().Contains("flash sale")).ToList();
                    responseData.PlanOrderTab = planName.MemberShipTabOrder;

                    if (isFlashSaleFlag.Count == 0)
                    {
                        responseData.PlanOrderTab = planName.MemberShipTabOrder.Where(x => x.TabName.ToLower() != "flash sale").ToList();
                    }
                    else
                    {
                        responseData.PlanOrderTab = planName.MemberShipTabOrder;
                    }

                    for (int i = 0; i < responseData.PlanOrderTab.Count; i++)
                    {
                        responseData.PlanOrderTab[i].RowNo = i + 1;
                    }

                    await GetABCPlanAndFeatureInformation(responseData.InitialPlanList, clubNumber);
                    responseData.plans = new List<memberSignUpPlanModel>();

                    var getAllAmenitiesList = await _planRepository.GetAllAmenitiesList(clubNumber);

                    var DistinctPlanTypes = getAllAmenitiesList.Select(x => x.PlanType).Distinct().ToList();

                    foreach (var s in DistinctPlanTypes)
                    {
                        var PlansWithTypes = responseData.InitialPlanList.Where(x => x.membershipType.ToLower() == s.ToLower()).ToList();

                        var amenityFeatures = getAllAmenitiesList.Where(x => x.PlanType.ToLower() == s.ToLower()).Select(x => new PlanFeatureModel { feature = x.AmenitiesName, isSelected = true }).ToList();

                        PlansWithTypes.ForEach(x => x.features = amenityFeatures);
                    }
                }
                return new ABCMemberPlanInitialModel() { ABCPlans = responseData, StopRequest = true };
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message.ToString());
                return new ABCMemberPlanInitialModel() { StopRequest = ex.Message.Contains("401") || ex.Message.Contains("500"), Reattempt = true };
            }
        }

        public async static Task<List<memberSignUpPlanModel>> SeparateMemberShipPlan(List<memberSignUpPlanModel> responseData, List<MemberPlanModel> MPlan)
        {
            List<memberSignUpPlanModel> res = new List<memberSignUpPlanModel>();

            foreach (var mtm in MPlan)
            {
                try
                {
                    var mplans = responseData.Where(x => string.Equals(x.planName.ToLower(), mtm.DataTrakPlans?.ToLower()) && !x.planName.ToLower().Contains("on demand")).ToList();

                    var list = mplans.Select(bre => new memberSignUpPlanModel
                    {
                        planName = bre.planName,
                        planId = bre.planId,
                        agreementDescription = bre.agreementDescription,
                        agreementTerms = bre.agreementTerms,
                        agreementNote = bre.agreementNote,
                        planValidation = bre.planValidation,
                        clubFeeTotalAmount = bre.clubFeeTotalAmount,
                        initiationFee = bre.initiationFee,
                        scheduleTotalAmount = bre.scheduleTotalAmount,
                        firstMonthDues = bre.firstMonthDues,
                        downPaymentTotalAmount = bre.downPaymentTotalAmount,
                        planFeesPricisionValue = bre.planFeesPricisionValue,
                        planFeesScaleValue = bre.planFeesScaleValue,
                        enrollFeePricisionValue = bre.enrollFeePricisionValue,
                        features = bre.features,
                        activePresale = bre.activePresale,
                        promoCode = bre.promoCode,
                        promoName = bre.promoName,
                        annualFeeDueDays = bre.annualFeeDueDays,
                        membershipType = bre.membershipType,
                        marketingPlanName = !String.IsNullOrEmpty(mtm.MarketingPlans) ? mtm.MarketingPlans : mtm.DataTrakPlans,
                        Strikeout_field = mtm.Strikeout_field,
                        TabName = mtm.TabName,
                        TabOrder = mtm.TabOrder,
                        OrderNumber = mtm.OrderNumber,
                        OriginalPlanTypeName = mtm.OriginalPlanTypeName,
                        BannerText = mtm.BannerText,
                        PlanDetailsJson = bre.PlanDetailsJson
                    }).ToList();

                    res.AddRange(list);
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message.ToString());
                }
            }

            return res;
        }

        public static async Task<memberSignUpPlanModel> GetABCPlanAndFeatureInformation(List<memberSignUpPlanModel> Plans, string clubNumber)
        {
            memberSignUpPlanModel memberPlans = new memberSignUpPlanModel();

            var MainFeatureList = new List<string>();
            foreach (var plan in Plans)
            {
                try
                {
                    var planObj = await GetMemberSignUpPlanDetailById(plan, clubNumber);
                    plan.agreementNote = planObj.agreementNote;
                    plan.agreementTerms = planObj.agreementTerms;
                    plan.clubFeeTotalAmount = planObj.clubFeeTotalAmount;
                    plan.annualFeeDueDays = planObj.annualFeeDueDays;
                    plan.initiationFee = planObj.initiationFee;
                    plan.firstMonthDues = planObj.firstMonthDues;
                    plan.planValidation = planObj.planValidation;
                    plan.planFeesPricisionValue = planObj.planFeesPricisionValue;
                    plan.planFeesScaleValue = planObj.planFeesScaleValue;
                    plan.enrollFeePricisionValue = planObj.enrollFeePricisionValue;
                    plan.downPaymentTotalAmount = planObj.downPaymentTotalAmount;
                    plan.activePresale = planObj.activePresale;
                    plan.Salestax = planObj.Salestax;
                    plan.PrepaidDues = planObj.PrepaidDues;
                    plan.AnnualDues = planObj.AnnualDues;
                    plan.TotalDue = planObj.TotalDue;
                    plan.PaidToday = planObj.PaidToday;
                    plan.TotalAmount = planObj.TotalAmount;
                    plan.PlanSubType = planObj.PlanSubType;
                    plan.PlanTypeDetail = planObj.PlanTypeDetail;
                    plan.schedulePreTaxAmount = planObj.schedulePreTaxAmount;
                    plan.firstDueDate = planObj.firstDueDate;
                    plan.Prorated_subTotal = planObj.Prorated_subTotal;
                    plan.Prorated_tax = planObj.Prorated_tax;
                    plan.Prorated_total = planObj.Prorated_total;
                    plan.expirationDate = planObj.expirationDate;
                    memberPlans = planObj;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message.ToString());
                }
            }
            return memberPlans;
        }

        public async static Task<memberSignUpPlanModel> GetABCMemberSignUpPlanDetailById(memberSignUpPlanModel plan, string clubNumber)
        {
            string ABCMemberSignUpPlanDetailByIdURL = ABC.ABCMemberSignUpPlanDetailByIdURL;
            var currentUrl = string.Format(ABCMemberSignUpPlanDetailByIdURL, clubNumber, plan.planId);
            string jsonHttpWebRequest = string.Empty;
            string response = string.Empty;
            try
            {
                string[] separatePrice = null;
                var initiationFee = "";
                var firstMonthDues = "";
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("app_id", ABC_CredentialData.Result.username);
                    client.Headers.Add("app_key", ABC_CredentialData.Result.password);
                    //Logger.Log("Reading from Service for club : " + clubNumber + " PlanId : " + planId);

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    response = client.DownloadString(currentUrl);
                    if (response.Contains("<count>0</count>"))
                    {
                        return plan;
                    }
                    var planDetails = JsonConvert.DeserializeObject<ABCMemberPlanDetailModel>(response);

                    if (planDetails != null && planDetails != null)
                    {
                        string annualFeeDueThirtyDays = ABC.AnnualFeeDueThirtyDays;
                        string annualFeeDueSixtyDays = ABC.AnnualFeeDueSixtyDays;

                        plan.membershipType = planDetails.paymentPlan.membershipType;
                        plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.subTotal).FirstOrDefault();

                        if ((planDetails.paymentPlan.planName.ToLower().Contains(ABC.TwoMonthFreePlan) || planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF)) && !string.IsNullOrEmpty(plan.initiationFee))
                        {
                            plan = planDetails.paymentPlan;

                            //initiationFee
                            plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.subTotal).FirstOrDefault();
                            initiationFee = plan.initiationFee != null ? plan.initiationFee.Replace("$", "") : "0.00";
                            plan.PrepaidDues = plan.initiationFee != null && planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? plan.initiationFee.Replace("$", "") : "0.00";

                            //firstMonthDues
                            plan.firstMonthDues = planDetails.paymentPlan.downPayments.Count == 1 ? null : planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.subTotal).FirstOrDefault();
                            firstMonthDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";

                            separatePrice = plan.initiationFee != null ? plan.initiationFee.Replace("$", "").Split('.') : null;
                            plan.planFeesPricisionValue = separatePrice != null ? Convert.ToInt32(separatePrice[0]) : 0;
                            plan.planFeesScaleValue = separatePrice == null ? "" : separatePrice.Length > 1 ? separatePrice[1] : "";

                            var tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.tax).FirstOrDefault();
                            plan.Salestax = tax != null ? tax.Replace("$", "") : "0.00";

                            var total = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.total).FirstOrDefault();
                            plan.TotalAmount = total != null ? total.Replace("$", "") : "0.00";

                            var Prorated_subTotalDetail = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.subTotal).FirstOrDefault();
                            plan.Prorated_subTotal = Prorated_subTotalDetail != null ? Prorated_subTotalDetail.Replace("$", "") : "0.00";

                            var expirationDateDetail = planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? planDetails.paymentPlan.expirationDate : null;
                            plan.firstDueDate = expirationDateDetail;
                            plan.expirationDate = expirationDateDetail;

                            //string[] separateInitiationFee = plan.initiationFee.Replace("$", "").Split('.');
                            plan.enrollFeePricisionValue = "0";
                        }
                        else
                        {
                            plan = planDetails.paymentPlan;

                            //initiationFee
                            plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Initiation Fee").Select(x => x.subTotal).FirstOrDefault();
                            initiationFee = plan.initiationFee != null ? plan.initiationFee.Replace("$", "") : "0.00";
                            //float floatValue = float.Parse(plan.initiationFee);

                            // firstmonthdues
                            var firstmonthdues = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.subTotal).FirstOrDefault();
                            plan.firstMonthDues = string.IsNullOrEmpty(firstmonthdues) ? planDetails.paymentPlan.downPayments.Where(x => x.name == "First Biweekly Payment").Select(x => x.subTotal).FirstOrDefault() : firstmonthdues;
                            firstMonthDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";


                            separatePrice = planDetails.paymentPlan.schedules.FirstOrDefault() != null ? planDetails.paymentPlan.schedules.FirstOrDefault().schedulePreTaxAmount.Replace("$", "").Split('.') : null;
                            plan.planFeesPricisionValue = Convert.ToInt32(separatePrice[0]);
                            plan.planFeesScaleValue = separatePrice.Length > 1 ? separatePrice[1] : "";

                            string[] separateInitiationFee = plan.initiationFee.Replace("$", "").Split('.');
                            if (separateInitiationFee[1] == "00" || separateInitiationFee[1] == "0")
                            {
                                plan.enrollFeePricisionValue = separateInitiationFee[0];
                            }
                            else
                            {
                                plan.enrollFeePricisionValue = plan.initiationFee.Replace("$", "");
                            }

                            string annualFeeDueDays = planDetails.paymentPlan.clubFees.Where(x => x.feeName.Contains(annualFeeDueThirtyDays)).Select(x => x.feeName).FirstOrDefault();
                            plan.annualFeeDueDays = !string.IsNullOrEmpty(annualFeeDueDays) && annualFeeDueDays.Contains(annualFeeDueThirtyDays) ? annualFeeDueThirtyDays : annualFeeDueSixtyDays;

                            var expirationDateDetail = planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? planDetails.paymentPlan.expirationDate : null;
                            plan.expirationDate = expirationDateDetail;
                        }
                    }
                    if (!planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF))
                    {
                        //initiationFee TAX
                        plan.InitiationFee_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "Initiation Fee").Select(x => x.tax).FirstOrDefault();
                        var InitiationFee_tax = plan.InitiationFee_tax != null ? plan.InitiationFee_tax.Replace("$", "") : "0.00";

                        //firstmonthdues_Tax

                        if (plan.planName.Contains("Biweekly"))
                        {
                            plan.FirstMonthDues_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Biweekly Payment").Select(x => x.tax).FirstOrDefault();
                        }
                        else
                        {
                            plan.FirstMonthDues_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.tax).FirstOrDefault();
                        }

                        var FirstMonthDues_tax = plan.FirstMonthDues_tax != null ? plan.FirstMonthDues_tax.Replace("$", "") : "0.00";

                        decimal IFT = decimal.Parse(InitiationFee_tax);
                        decimal FMT = decimal.Parse(FirstMonthDues_tax);
                        decimal IF = decimal.Parse(initiationFee);
                        decimal FM = decimal.Parse(firstMonthDues);
                        plan.Salestax = String.Format("{0:0.00}", FMT + IFT);

                        // ANNUAL DUES = clubFeeTotalAmount
                        plan.AnnualDues = planDetails.paymentPlan.clubFeeTotalAmount;

                        // PREPAID DUES = downPayments -> (First Biweekly Payment OR First Month Dues)
                        plan.PrepaidDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";

                        //PAID TODAY = FirstMonthDues_tax + InitiationFee_tax + InitiationFee_subTotal + FirstMonthDues_subTotal
                        plan.PaidToday = String.Format("{0:0.00}", IFT + FMT + IF + FM);
                        plan.TotalDue = plan.PaidToday;

                        plan.TotalAmount = planDetails.paymentPlan.schedules != null ? planDetails.paymentPlan.schedules.Where(x => x.profitCenter == "Dues").Select(x => x.scheduleAmount).FirstOrDefault() : "0.00";
                        plan.TotalAmount = plan.TotalAmount != null ? plan.TotalAmount.Replace("$", "") : "0.00";

                        // PlanSubType 
                        var PlanType = planDetails.paymentPlan.scheduleFrequency;
                        var firstDueDate = planDetails.paymentPlan.firstDueDate;
                        var expirationDate = planDetails.paymentPlan.expirationDate;

                        int? monthsApart = 12 * (expirationDate?.Year - firstDueDate?.Year) + expirationDate?.Month - firstDueDate?.Month;
                        plan.PlanSubType = expirationDate != null && monthsApart > 3 || expirationDate != null && firstDueDate == null ? "Yearly" : "Monthly";

                        //PlanType
                        plan.PlanTypeDetail = PlanType != null && PlanType == "Bi-Weekly" ? "BiWeekly" : plan.PlanSubType;

                        plan.schedulePreTaxAmount = planDetails.paymentPlan.schedules != null ? planDetails.paymentPlan.schedules.Where(x => x.profitCenter == "Dues").Select(x => x.schedulePreTaxAmount).FirstOrDefault() : "0.00";
                        plan.schedulePreTaxAmount = plan.schedulePreTaxAmount != null ? plan.schedulePreTaxAmount.Replace("$", "") : "0.00";

                        plan.firstDueDate = planDetails.paymentPlan.firstDueDate;

                        var Prorated_subTotal = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.subTotal).FirstOrDefault();
                        plan.Prorated_subTotal = Prorated_subTotal != null ? Prorated_subTotal.Replace("$", "") : "0.00";

                        var Prorated_tax = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.tax).FirstOrDefault();
                        plan.Prorated_tax = Prorated_tax != null ? Prorated_tax.Replace("$", "") : "0.00";

                        var Prorated_total = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.total).FirstOrDefault();
                        plan.Prorated_total = Prorated_total != null ? Prorated_total.Replace("$", "") : "0.00";

                        plan.expirationDate = expirationDate;
                    }
                    return plan;
                }
            }
            catch (Exception ex)
            {

                //var MemberlogId = SaveABCLog(jsonHttpWebRequest, response, "GetMemberSignUpPlanDetailById", currentUrl, clubNumber, null, null, null, null, plan.planName, null, plan.TabName, ex.Message, null);
                return plan;
            }
        }



        public async static Task<memberSignUpPlanModel> GetMemberSignUpPlanDetailById(memberSignUpPlanModel plan, string clubNumber)
        {
            string ABCMemberSignUpPlanDetailByIdURL = ABC.ABCMemberSignUpPlanDetailByIdURL;
            var currentUrl = string.Format(ABCMemberSignUpPlanDetailByIdURL, clubNumber, plan.planId);
            string jsonHttpWebRequest = string.Empty;
            string response = string.Empty;
            try
            {
                string[] separatePrice = null;
                var initiationFee = "";
                var firstMonthDues = "";

                var planDetails = JsonConvert.DeserializeObject<ABCMemberPlanDetailModel>(plan.PlanDetailsJson);

                if (planDetails != null && planDetails != null)
                {
                    string annualFeeDueThirtyDays = ABC.AnnualFeeDueThirtyDays;
                    string annualFeeDueSixtyDays = ABC.AnnualFeeDueSixtyDays;

                    plan.membershipType = planDetails.paymentPlan.membershipType;
                    plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.subTotal).FirstOrDefault();

                    if ((planDetails.paymentPlan.planName.ToLower().Contains(ABC.TwoMonthFreePlan) || planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF)) && !string.IsNullOrEmpty(plan.initiationFee))
                    {
                        plan = planDetails.paymentPlan;

                        //initiationFee
                        plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.subTotal).FirstOrDefault();
                        initiationFee = plan.initiationFee != null ? plan.initiationFee.Replace("$", "") : "0.00";
                        plan.PrepaidDues = plan.initiationFee != null && planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? plan.initiationFee.Replace("$", "") : "0.00";

                        //firstMonthDues
                        plan.firstMonthDues = planDetails.paymentPlan.downPayments.Count == 1 ? null : planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.subTotal).FirstOrDefault();
                        firstMonthDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";

                        separatePrice = plan.initiationFee != null ? plan.initiationFee.Replace("$", "").Split('.') : null;
                        plan.planFeesPricisionValue = separatePrice != null ? Convert.ToInt32(separatePrice[0]) : 0;
                        plan.planFeesScaleValue = separatePrice == null ? "" : separatePrice.Length > 1 ? separatePrice[1] : "";

                        var tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.tax).FirstOrDefault();
                        plan.Salestax = tax != null ? tax.Replace("$", "") : "0.00";

                        var total = planDetails.paymentPlan.downPayments.Where(x => x.name == "Pre-Paid Dues").Select(x => x.total).FirstOrDefault();
                        plan.TotalAmount = total != null ? total.Replace("$", "") : "0.00";

                        var Prorated_subTotalDetail = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.subTotal).FirstOrDefault();
                        plan.Prorated_subTotal = Prorated_subTotalDetail != null ? Prorated_subTotalDetail.Replace("$", "") : "0.00";

                        var expirationDateDetail = planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? planDetails.paymentPlan.expirationDate : null;
                        plan.firstDueDate = expirationDateDetail;
                        plan.expirationDate = expirationDateDetail;

                        //string[] separateInitiationFee = plan.initiationFee.Replace("$", "").Split('.');
                        plan.enrollFeePricisionValue = "0";
                    }
                    else
                    {
                        plan = planDetails.paymentPlan;

                        //initiationFee
                        plan.initiationFee = planDetails.paymentPlan.downPayments.Where(x => x.name == "Initiation Fee").Select(x => x.subTotal).FirstOrDefault();
                        initiationFee = plan.initiationFee != null ? plan.initiationFee.Replace("$", "") : "0.00";
                        //float floatValue = float.Parse(plan.initiationFee);

                        // firstmonthdues
                        var firstmonthdues = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.subTotal).FirstOrDefault();
                        plan.firstMonthDues = string.IsNullOrEmpty(firstmonthdues) ? planDetails.paymentPlan.downPayments.Where(x => x.name == "First Biweekly Payment").Select(x => x.subTotal).FirstOrDefault() : firstmonthdues;
                        firstMonthDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";


                        separatePrice = planDetails.paymentPlan.schedules.FirstOrDefault() != null ? planDetails.paymentPlan.schedules.FirstOrDefault().schedulePreTaxAmount.Replace("$", "").Split('.') : null;
                        plan.planFeesPricisionValue = Convert.ToInt32(separatePrice[0]);
                        plan.planFeesScaleValue = separatePrice.Length > 1 ? separatePrice[1] : "";

                        string[] separateInitiationFee = plan.initiationFee.Replace("$", "").Split('.');
                        if (separateInitiationFee[1] == "00" || separateInitiationFee[1] == "0")
                        {
                            plan.enrollFeePricisionValue = separateInitiationFee[0];
                        }
                        else
                        {
                            plan.enrollFeePricisionValue = plan.initiationFee.Replace("$", "");
                        }

                        string annualFeeDueDays = planDetails.paymentPlan.clubFees.Where(x => x.feeName.Contains(annualFeeDueThirtyDays)).Select(x => x.feeName).FirstOrDefault();
                        plan.annualFeeDueDays = !string.IsNullOrEmpty(annualFeeDueDays) && annualFeeDueDays.Contains(annualFeeDueThirtyDays) ? annualFeeDueThirtyDays : annualFeeDueSixtyDays;

                        var expirationDateDetail = planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF) ? planDetails.paymentPlan.expirationDate : null;
                        plan.expirationDate = expirationDateDetail;
                    }
                }
                if (!planDetails.paymentPlan.planName.ToLower().Contains(ABC.MonthPIF))
                {
                    //initiationFee TAX
                    plan.InitiationFee_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "Initiation Fee").Select(x => x.tax).FirstOrDefault();
                    var InitiationFee_tax = plan.InitiationFee_tax != null ? plan.InitiationFee_tax.Replace("$", "") : "0.00";

                    //firstmonthdues_Tax

                    if (plan.planName.Contains("Biweekly"))
                    {
                        plan.FirstMonthDues_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Biweekly Payment").Select(x => x.tax).FirstOrDefault();
                    }
                    else
                    {
                        plan.FirstMonthDues_tax = planDetails.paymentPlan.downPayments.Where(x => x.name == "First Month Dues").Select(x => x.tax).FirstOrDefault();
                    }

                    var FirstMonthDues_tax = plan.FirstMonthDues_tax != null ? plan.FirstMonthDues_tax.Replace("$", "") : "0.00";

                    decimal IFT = decimal.Parse(InitiationFee_tax);
                    decimal FMT = decimal.Parse(FirstMonthDues_tax);
                    decimal IF = decimal.Parse(initiationFee);
                    decimal FM = decimal.Parse(firstMonthDues);
                    plan.Salestax = String.Format("{0:0.00}", FMT + IFT);

                    // ANNUAL DUES = clubFeeTotalAmount
                    plan.AnnualDues = planDetails.paymentPlan.clubFeeTotalAmount;

                    // PREPAID DUES = downPayments -> (First Biweekly Payment OR First Month Dues)
                    plan.PrepaidDues = plan.firstMonthDues != null ? plan.firstMonthDues.Replace("$", "") : "0.00";

                    //PAID TODAY = FirstMonthDues_tax + InitiationFee_tax + InitiationFee_subTotal + FirstMonthDues_subTotal
                    plan.PaidToday = String.Format("{0:0.00}", IFT + FMT + IF + FM);
                    plan.TotalDue = plan.PaidToday;

                    plan.TotalAmount = planDetails.paymentPlan.schedules != null ? planDetails.paymentPlan.schedules.Where(x => x.profitCenter == "Dues").Select(x => x.scheduleAmount).FirstOrDefault() : "0.00";
                    plan.TotalAmount = plan.TotalAmount != null ? plan.TotalAmount.Replace("$", "") : "0.00";

                    // PlanSubType 
                    var PlanType = planDetails.paymentPlan.scheduleFrequency;
                    var firstDueDate = planDetails.paymentPlan.firstDueDate;
                    var expirationDate = planDetails.paymentPlan.expirationDate;

                    int? monthsApart = 12 * (expirationDate?.Year - firstDueDate?.Year) + expirationDate?.Month - firstDueDate?.Month;
                    plan.PlanSubType = expirationDate != null && monthsApart > 3 || expirationDate != null && firstDueDate == null ? "Yearly" : "Monthly";

                    //PlanType
                    plan.PlanTypeDetail = PlanType != null && PlanType == "Bi-Weekly" ? "BiWeekly" : plan.PlanSubType;

                    plan.schedulePreTaxAmount = planDetails.paymentPlan.schedules != null ? planDetails.paymentPlan.schedules.Where(x => x.profitCenter == "Dues").Select(x => x.schedulePreTaxAmount).FirstOrDefault() : "0.00";
                    plan.schedulePreTaxAmount = plan.schedulePreTaxAmount != null ? plan.schedulePreTaxAmount.Replace("$", "") : "0.00";

                    plan.firstDueDate = planDetails.paymentPlan.firstDueDate;

                    var Prorated_subTotal = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.subTotal).FirstOrDefault();
                    plan.Prorated_subTotal = Prorated_subTotal != null ? Prorated_subTotal.Replace("$", "") : "0.00";

                    var Prorated_tax = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.tax).FirstOrDefault();
                    plan.Prorated_tax = Prorated_tax != null ? Prorated_tax.Replace("$", "") : "0.00";

                    var Prorated_total = planDetails.paymentPlan.downPayments.Where(x => x.name.ToLower() == "prorated annual fee").Select(x => x.total).FirstOrDefault();
                    plan.Prorated_total = Prorated_total != null ? Prorated_total.Replace("$", "") : "0.00";

                    plan.expirationDate = expirationDate;
                }
                return plan;
            }
            catch (Exception ex)
            {
                //var MemberlogId = SaveABCLog(jsonHttpWebRequest, response, "GetMemberSignUpPlanDetailById", currentUrl, clubNumber, null, null, null, null, plan.planName, null, plan.TabName, ex.Message, null);
                return plan;
            }
        }

        public async Task<ABCMemberPlanResponseModel> GetMemberPlans(string clubNumber, InitialPlanModel planName)
        {
            ABCMemberPlanResponseModel Response = new ABCMemberPlanResponseModel();
            try
            {
                bool BlnBreak = false;
                int reAttemptIndex = 1;

                while (!BlnBreak)
                {
                    var ResponseData = await GetMemberPlansFromABC(clubNumber, planName);
                    if (ResponseData != null && ResponseData.ABCPlans != null && ResponseData.ABCPlans.InitialPlanList != null && ResponseData.ABCPlans.InitialPlanList.Any())
                    {
                        Response = ResponseData.ABCPlans;
                    }

                    if (ResponseData.Reattempt.HasValue && ResponseData.Reattempt.Value)
                    {
                        reAttemptIndex++;
                        if (reAttemptIndex < 4)
                            continue;
                    }

                    if (reAttemptIndex >= 4)
                    {
                        BlnBreak = true;
                    }
                    if (ResponseData.StopRequest.HasValue && ResponseData.StopRequest.Value)
                    {
                        BlnBreak = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message.ToString());
            }
            return Response;

        }

        public async static Task<ABCAgreementResponseModel> CreateMemberSignUpAgreement(string clubNumber, string PaymentPlanId, string PlanValidationHash, bool ActivePresale,
                                                              string SalesPersonId,
                                                              ABCAgreementContactInfoModel AgreementContactInfoModel,
                                                              ABCTodayBillingInfoModel TodayBillingInfoModel,
                                                              ABCDraftCreditCardModel DraftCreditCardModel,
                                                              ABCDraftBankAccountModel DraftBankAccountModel, long NewMemberId, int SourceId, string PlanName, string PTPlanName, string MemberId, bool IsRecurringPaymentFlag)
        {
            checkOutMessageLogDetail("Create Member SignUp Agreement : ", "Create Member SignUp Agreement : ", AgreementContactInfoModel.firstName, AgreementContactInfoModel.lastName, AgreementContactInfoModel.email, clubNumber, null, null);

            var methodInfo = MethodBase.GetCurrentMethod();
            var fullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;


            var request = (dynamic)null;
            string jsonData = string.Empty;
            string currentUrl = string.Empty;

            ABCAgreementResponseModel Response = new ABCAgreementResponseModel();
            try
            {
                currentUrl = string.Format(MemberCheckOutSettings.ABCMemberSignUpPostAgreementURL, clubNumber);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                //Remove from live While making live
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------

                if (TodayBillingInfoModel == null)
                {
                    request = new ABCAgreementRequestModel();
                }
                else if (IsRecurringPaymentFlag == true)
                {

                    ABCTodayBillingInfoModel aBCTodayBillingInfoModel = new ABCTodayBillingInfoModel();
                    aBCTodayBillingInfoModel.isTodayBillingSameAsDraft = IsRecurringPaymentFlag;
                    aBCTodayBillingInfoModel.todayCcBillingZip = TodayBillingInfoModel.todayCcBillingZip;
                    aBCTodayBillingInfoModel.todayCcCvvCode = TodayBillingInfoModel.todayCcCvvCode;

                    request = new ABCAgreementRequestModel<ABCTodayBillingInfoModel, ABCDraftBillingInfoModel>();
                    request.todayBillingInfo = aBCTodayBillingInfoModel;
                    request.draftBillingInfo = new ABCDraftBillingInfoModel() { draftBankAccount = null, draftCreditCard = DraftCreditCardModel };
                }
                else
                {
                    request = new ABCAgreementRequestModel<ABCTodayBillingInfoModel, ABCDraftBillingInfoModel>();
                    request.todayBillingInfo = TodayBillingInfoModel;
                    request.draftBillingInfo = new ABCDraftBillingInfoModel() { draftBankAccount = DraftBankAccountModel, draftCreditCard = DraftCreditCardModel };
                }

                if (TodayBillingInfoModel != null && DraftCreditCardModel != null && DraftBankAccountModel == null)
                {
                    if (!PlanName.ToLower().Contains(MemberCheckOutSettings.Avoid_ProcessFee_for_PlanName.ToLower()))
                    {
                        if (MemberCheckOutSettings.IsSchedules_Processfee_Applicable == "true")
                        {
                            request.schedules = new string[] { MemberCheckOutSettings.schedules_Processfee };
                        }
                    }
                }

                request.paymentPlanId = PaymentPlanId;
                request.planValidationHash = PlanValidationHash;
                request.activePresale = ActivePresale;
                request.sendAgreementEmail = true;
                if (!string.IsNullOrEmpty(SalesPersonId))
                {
                    request.salesPersonId = SalesPersonId;
                }
                request.campaignId = MemberCheckOutSettings.CampaignId;
                request.agreementContactInfo = AgreementContactInfoModel;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonData = JsonConvert.SerializeObject(request);

                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Response = JsonConvert.DeserializeObject<ABCAgreementResponseModel>(result);
                        checkOutMessageLogDetail("Create Member SignUp Agreement : ", httpResponse.StatusCode.ToString().ToUpper(), AgreementContactInfoModel.firstName, AgreementContactInfoModel.lastName, AgreementContactInfoModel.email, clubNumber, null, null);

                        //Save ABC Logs into DB
                        Response.MemberlogId = ABCLoggerService.SAVEABCLogs_MemberSignUpAgreement(jsonData, AgreementContactInfoModel, "Guest(Refresh)", result, Response.status.message, PlanName, "MemberSignUpAgreement",
                            Response.result.memberId, clubNumber, NewMemberId, currentUrl, fullName);
                        return Response;

                    }
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        Response.status = new ABCStatusModel();
                        Response.status.message = result;
                        Response.status.count = "0";

                        checkOutMessageLogDetail("Create Member SignUp Agreement : ", httpResponse.StatusCode.ToString().ToUpper(), AgreementContactInfoModel.firstName, AgreementContactInfoModel.lastName, AgreementContactInfoModel.email, clubNumber, null, null);

                        //Save ABC Logs into DB
                        Response.MemberlogId = ABCLoggerService.SAVEABCLogs_MemberSignUpAgreement(jsonData, AgreementContactInfoModel, "Guest(Refresh)", result, Response.status.message, PlanName, "MemberSignUpAgreement",
                            null, clubNumber, NewMemberId, currentUrl, fullName);

                    }
                }
            }
            catch (WebException e)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    var result = r.ReadToEnd();

                    checkOutMessageLogDetail("Create Member SignUp Agreement (WebException): ", "Create Member SignUp Agreement", AgreementContactInfoModel.firstName, AgreementContactInfoModel.lastName, AgreementContactInfoModel.email, clubNumber, null, null);


                    Response.status = new ABCStatusModel();
                    Response.status.message = result;
                    Response.status.count = "0";

                    //Save ABC Logs into DB
                    Response.MemberlogId = ABCLoggerService.SAVEABCLogs_MemberSignUpAgreement(jsonData, AgreementContactInfoModel, "Guest(Refresh)", result, Response.status.message, PlanName, "MemberSignUpAgreement",
                        null, clubNumber, NewMemberId, currentUrl, fullName);
                    _abclogger.SendEmail(e, "clubNumber = " + clubNumber);
                }
            }
            catch (Exception ex)
            {
                Response.status = new ABCStatusModel();
                Response.status.message = ex.ToString();
                Response.status.count = "0";

                checkOutMessageLogDetail("Create Member SignUp Agreement (Exception): ", ex.ToString(), AgreementContactInfoModel.firstName, AgreementContactInfoModel.lastName, AgreementContactInfoModel.email, clubNumber, null, null);


                //Save ABC Logs into DB
                Response.MemberlogId = ABCLoggerService.SAVEABCLogs_MemberSignUpAgreement(jsonData, AgreementContactInfoModel, "Guest(Refresh)", Response.status.message, "Internal Server Error", PlanName, "MemberSignUpAgreement",
                    null, clubNumber, NewMemberId, currentUrl, fullName);
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber);
            }
            return Response;
        }

        

        public async Task<string> CheckCreditCardNumber(string CCnumber)
        {
            string message = "";
            try
            {
                string CreditCardBinApiKey = MemberCheckOutSettings.CreditCardBinApiKey;
                if (!string.IsNullOrEmpty(CCnumber) && CCnumber != "undefined")
                {
                    CCnumber = CCnumber.Replace(" ", "");
                    // Luhn algorithm validation
                    int sum = 0;
                    bool alternate = false;
                    for (int i = CCnumber.Length - 1; i >= 0; i--)
                    {
                        int n = int.Parse(CCnumber.Substring(i, 1));
                        if (alternate)
                        {
                            n *= 2;
                            if (n > 9)
                            {
                                n = (n % 10) + 1;
                            }
                        }
                        sum += n;
                        alternate = !alternate;
                    }
                    bool isValid = (sum % 10 == 0);

                    // BIN lookup
                    if (isValid)
                    {
                        CCnumber = CCnumber.Substring(0, 6);

                        var binDbService = new BinDBService(new HttpClient(), CreditCardBinApiKey);
                        var binData = await binDbService.LookupBin(CCnumber);
                        string json = JsonConvert.SerializeObject(binData);

                        var cardData = JsonConvert.DeserializeObject<CreditCardRoot>(json);
                        if (cardData != null)
                        {
                            if (cardData.result != null)
                            {
                                bool? isPrepaid = (bool?)cardData.result.prepaid;
                                if (isPrepaid == true)
                                {
                                    message = "1";
                                }
                                else
                                {
                                    message = "2";
                                }
                            }
                            else
                            {
                                message = "0";
                            }
                        }
                        else
                        {
                            message = "0";
                        }
                    }
                    else
                    {
                        message = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "CheckCreditCardNumber", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return message;
        }

        public async static Task<ABCPTUpdateClubAccountResponseModel> UpdateClubAccountPT(MemberCheckOutInitialModel PostData, RecurringServicePlan recurringPlan, int MemberlogId, string Source, long NewPTMemberId)
        {
            var methodInfo = MethodBase.GetCurrentMethod();
            var fullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            checkOutMessageLogDetail("Update Club Account PT : ", "Update Club Account PT ", PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, null);


            ABCPTUpdateClubAccountResponseModel Response = new ABCPTUpdateClubAccountResponseModel();
            string jsonHttpWebRequest = string.Empty;
            string result = string.Empty;
            var currentUrl = string.Format(MemberCheckOutSettings.ABCGetClubAccountPTPaymentMethod, PostData.PlanInitialInformation.ClubNumber, PostData.PersonalInformation.MemberId);
            var request = (dynamic)null;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                //Remove from live While making live
                //ServicePointManager.DefaultConnectionLimit = 1000;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------



                request = await PTPaymentInfo(PostData);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonHttpWebRequest = JsonConvert.SerializeObject(request);

                    streamWriter.Write(jsonHttpWebRequest);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                        Response = JsonConvert.DeserializeObject<ABCPTUpdateClubAccountResponseModel>(result);

                        checkOutMessageLogDetail("Update Club Account PT : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, null);

                        //Save ABC Logs into DB
                        await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, Source, result, Response.status.message, "UpdateClubAccountPT", PostData.PersonalInformation.MemberId,
                              MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                        //var Member_logId = SaveABCLog(jsonHttpWebRequest, result, "UpdateClubAccountPT", currentUrl, PostData.PlanInitialInformation.ClubNumber.ToString(), PostData.PersonalInformation?.FirstName, PostData.PersonalInformation?.LastName, PostData.PersonalInformation?.Email, PostData.PersonalInformation?.MemberId, PostData.PlanInitialInformation.PlanName, null, Source, Response?.status?.message, null);
                        return Response;
                    }
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var jsonHttpWebResponse = streamReader.ReadToEnd();

                        Response.status = new ABCRecurringStatusModel();
                        Response.status.message = "CheckOut:" + " " + jsonHttpWebResponse;
                        Response.status.count = "0";
                        checkOutMessageLogDetail("Update Club Account PT : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, null);

                        //Save ABC Logs into DB
                        await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, Source, jsonHttpWebResponse, Response.status.message, "UpdateClubAccountPT", PostData.PersonalInformation.MemberId,
                            MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                    }
                }
            }
            catch (WebException e)
            {
                _abclogger.SendEmail(e, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    result = r.ReadToEnd();

                    Response.status = new ABCRecurringStatusModel();
                    Response.status.message = result;

                    checkOutMessageLogDetail("Update Club Account PT : ", e.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, null);

                    //Save ABC Logs into DB
                    await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, Source, result, Response.status.message, "UpdateClubAccountPT", PostData.PersonalInformation.MemberId,
                        MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);
                }
                //ExternalExceptionLogger.LogException(e, _controllerName, "UpdateClubAccountPT(MemberSignUpRequestModel PostData)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);
                Response.status = new ABCRecurringStatusModel();
                Response.status.message = ex.ToString();

                checkOutMessageLogDetail("Update Club Account PT : ", ex.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, null);

                //Save ABC Logs into DB
                await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, Source, Response.status.message, "Internal Server Error", "UpdateClubAccountPT", PostData.PersonalInformation.MemberId,
                    MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateClubAccountPT(MemberSignUpRequestModel PostData)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return Response;
        }

        public static ABCPTUpdateClubAccountResponseModel GetClubAccountPaymentMethod(int clubNumber, string memberId)
        {
            //checkOutMessageLogDetail("Get Club Account PaymentMethod : ", "Get Club Account Payment Method", null, null, null, memberId, clubNumber.ToString(), null);

            ABCPTUpdateClubAccountResponseModel Response = new ABCPTUpdateClubAccountResponseModel();
            var currentUrl = string.Format(ABC.ABCClubAccountPTPaymentMethod, clubNumber, memberId);
            string methodName = "GetClubAccountPaymentMethod";
            string jsonHttpWebRequest = string.Empty;
            string jsonHttpWebResponse = string.Empty;
            try
            {

                using (var client = new YoufitWebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("app_id", ABC_CredentialData.Result.username);
                    client.Headers.Add("app_key", ABC_CredentialData.Result.password);
                    client.Headers.Add("cache-control", "no-cache");

                    //Remove from live While making live
                    //ServicePointManager.DefaultConnectionLimit = 1000;
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    jsonHttpWebResponse = client.DownloadString(currentUrl);

                    Response = JsonConvert.DeserializeObject<ABCPTUpdateClubAccountResponseModel>(jsonHttpWebResponse);
                }
            }
            catch (WebException e)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    jsonHttpWebResponse = r.ReadToEnd();

                    Response.status = new ABCRecurringStatusModel();
                    Response.status.message = jsonHttpWebResponse;
                    Response.status.count = "0";
                    var _response = JsonConvert.DeserializeObject<dynamic>(jsonHttpWebResponse);
                    var _message = (string)_response?.status?.message;

                    var Member_logId = SaveABCLog(jsonHttpWebRequest, jsonHttpWebResponse, methodName, currentUrl, clubNumber.ToString(), null, null, null, memberId, null, null, null, _message, null);
                }
                //checkOutMessageLogDetail("Get Club Account PaymentMethod : ", e.ToString(), null, null, null, memberId, clubNumber.ToString(), null);

                //ExternalExceptionLogger.LogException(e, _controllerName, "GetClubAccountPaymentMethod(int clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);

                _abclogger.SendEmail(e, "clubNumber = " + clubNumber + ", memberId = " + memberId);
            }
            catch (Exception ex)
            {
                Response.status = new ABCRecurringStatusModel();
                Response.status.message = ex.ToString();
                Response.status.count = "0";
                var Member_logId = SaveABCLog(jsonHttpWebRequest, jsonHttpWebResponse, methodName, currentUrl, clubNumber.ToString(), null, null, null, memberId, null, null, null, Response.status.message, null);
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber + ", memberId = " + memberId);
                //checkOutMessageLogDetail("Get Club Account PaymentMethod : ", ex.ToString(), null, null, null, memberId, clubNumber.ToString(), null);

                //ExternalExceptionLogger.LogException(ex, _controllerName, "GetClubAccountPaymentMethod(int clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);

            }
            return Response;
        }


        public async static Task<dynamic> PTPaymentInfo(MemberCheckOutInitialModel PostData)
        {
            var request = (dynamic)null;
            //if (!PostData.PTPaymentObjInfo.RecurrPaymentInfo.IsSameCard)
            //{
            //    ABCPTClubAccountDraftAccountModel draftAccountResult = new ABCPTClubAccountDraftAccountModel();
            //    draftAccountResult.draftAccountFirstName = PostData.PTPaymentObjInfo.RecurrPaymentInfo.BankAccountInformation.DraftAccountFirstName.GetString();
            //    draftAccountResult.draftAccountLastName = PostData.PTPaymentObjInfo.RecurrPaymentInfo.BankAccountInformation.DraftAccountLastName.GetString();
            //    draftAccountResult.draftAccountRoutingNumber = PostData.PTPaymentObjInfo.RecurrPaymentInfo.BankAccountInformation.DraftAccountRoutingNumber;
            //    draftAccountResult.draftAccountType = PostData.PTPaymentObjInfo.RecurrPaymentInfo.BankAccountInformation.DraftAccountType.GetDraftAccountType();
            //    draftAccountResult.draftAccountNumber = PostData.PTPaymentObjInfo.RecurrPaymentInfo.BankAccountInformation.DraftAccountNumber;

            //    request = new ABCPTClubAccountRootCreditCardAndDraftAccountModel();
            //    request.draftAccount = draftAccountResult;
            //}
            //else
            //{
            ABCPTClubAccountCreditCardRequestModel creaditCardResult = new ABCPTClubAccountCreditCardRequestModel();
            creaditCardResult.creditCardFirstName = PostData.PTPaymentInformation.CreditCardFirstName.GetString();
            creaditCardResult.creditCardLastName = PostData.PTPaymentInformation.CreditCardLastName.GetString();
            creaditCardResult.creditCardType = Regex.Replace(PostData.PTPaymentInformation.CreditCardType, @"\s+", "").ToLower();
            creaditCardResult.creditCardAccountNumber = PostData.PTPaymentInformation.CreditCardNumber.Replace(" ", "");
            creaditCardResult.creditCardExpMonth = PostData.PTPaymentInformation.CreditCardExpMonth;
            creaditCardResult.creditCardExpYear = PostData.PTPaymentInformation.CreditCardExpYear;
            creaditCardResult.creditCardPostalCode = PostData.PTPaymentInformation.CreditCardZipCode;

            request = new ABCPTClubAccountRootCreditCardAndDraftAccountModel();
            request.creditCard = creaditCardResult;
            //}
            return request;
        }

        public async static Task<ABCPTRecurringServiceResonseModel> CreateMemberRecurringService(MemberCheckOutInitialModel PostData, RecurringServicePlan recurringPlan, string Source, int MemberlogId, long NewPTMemberId)
        {
            var methodInfo = MethodBase.GetCurrentMethod();
            var fullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            checkOutMessageLogDetail("Create Member Recurring Service : ", "Create Member Recurring Service", PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, null, null);


            ABCPTRecurringServiceResonseModel Response = new ABCPTRecurringServiceResonseModel();
            ABCRecurringServiceRequest request = new ABCRecurringServiceRequest();
            string jsonData = string.Empty;
            string currentUrl = string.Empty;

            try
            {
                currentUrl = string.Format(MemberCheckOutSettings.ABCMemberRecurringPOSTServiceURL, PostData.PlanInitialInformation.ClubNumber, PostData.PersonalInformation.MemberId);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                //Remove from live While making live
                //ServicePointManager.DefaultConnectionLimit = 1000;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------


                request.clubNumber = Convert.ToString(PostData.PlanInitialInformation.ClubNumber);
                request.memberId = PostData.PersonalInformation.MemberId;
                request.recurringServicePlanId = recurringPlan.recurringServicePlanId;
                request.validationHash = recurringPlan.validationHash;
                if (PostData.PersonalInformation.SalesPersonObj != null)
                {
                    request.commissionsEmployeeId = PostData.PersonalInformation.SalesPersonObj.SPEmployeeId;
                    request.serviceEmployeeId = PostData.PersonalInformation.SalesPersonObj.SPEmployeeId;
                }
                else
                {
                    request.commissionsEmployeeId = MemberCheckOutSettings.CommissionsEmployeeID.ToString();
                    request.serviceEmployeeId = MemberCheckOutSettings.ServiceEmployeeID.ToString();
                }
                //request.campaignId = WebConfigurationManager.AppSettings["CampaignId"];

                ABCRecurringServiceCreditCardModel creditCard = new ABCRecurringServiceCreditCardModel();

                if (PostData.PTBankingDetailObj.IsPTUseBankingDetails != null)
                {
                    creditCard.creditCardFirstName = PostData.PTPaymentInformation.CreditCardFirstName.GetString();
                    creditCard.creditCardLastName = PostData.PTPaymentInformation.CreditCardLastName.GetString();
                    creditCard.creditCardType = Regex.Replace(PostData.PTPaymentInformation.CreditCardType, @"\s+", "");
                    creditCard.creditCardAccountNumber = PostData.PTPaymentInformation.CreditCardNumber.Replace(" ", "");
                    creditCard.creditCardExpMonth = PostData.PTPaymentInformation.CreditCardExpMonth;
                    creditCard.creditCardExpYear = PostData.PTPaymentInformation.CreditCardExpYear;
                    creditCard.creditCardPostalCode = PostData.PTPaymentInformation.CreditCardZipCode;
                    creditCard.creditCardCvvCode = PostData.PTPaymentInformation.CreditCardCVV;
                }
                else
                {
                    creditCard.creditCardFirstName = PostData.SecondaryPTPaymentInformation.CreditCardFirstName.GetString();
                    creditCard.creditCardLastName = PostData.SecondaryPTPaymentInformation.CreditCardLastName.GetString();
                    creditCard.creditCardType = Regex.Replace(PostData.SecondaryPTPaymentInformation.CreditCardType, @"\s+", "");
                    creditCard.creditCardAccountNumber = PostData.SecondaryPTPaymentInformation.CreditCardNumber.Replace(" ", "");
                    creditCard.creditCardExpMonth = PostData.SecondaryPTPaymentInformation.CreditCardExpMonth;
                    creditCard.creditCardExpYear = PostData.SecondaryPTPaymentInformation.CreditCardExpYear;
                    creditCard.creditCardPostalCode = PostData.SecondaryPTPaymentInformation.CreditCardZipCode;
                    creditCard.creditCardCvvCode = PostData.SecondaryPTPaymentInformation.CreditCardCVV;
                }

                request.creditCard = creditCard;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonData = JsonConvert.SerializeObject(request);

                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Response = JsonConvert.DeserializeObject<ABCPTRecurringServiceResonseModel>(result);

                        //Save ABC Logs into DB
                        await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonData, PostData, Source, result, Response.status.message, "MemberRecurringServiceAgreement", PostData.PersonalInformation.MemberId,
                            MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                        checkOutMessageLogDetail("Create Member Recurring Service : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, null, null);


                        return Response;
                    }
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var jsonHttpWebResponse = streamReader.ReadToEnd();

                        Response.status = new ABCRecurringStatusModel();
                        Response.status.message = jsonHttpWebResponse;
                        Response.status.count = "0";

                        //Save ABC Logs into DB
                        await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonData, PostData, Source, jsonHttpWebResponse, Response.status.message, "MemberRecurringServiceAgreement", PostData.PersonalInformation.MemberId,
                             MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);
                        checkOutMessageLogDetail("Create Member Recurring Service : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, null, null);


                    }
                }
            }
            catch (WebException e)
            {
                //ExternalExceptionLogger.LogException(e, _controllerName, "CreateMemberRecurringService(MemberSignUpRequestModel PostData, RecurringServicePlan recurringPlan, string Source, int MemberlogId, long NewPTMemberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);

                _abclogger.SendEmail(e, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    var result = r.ReadToEnd();
                    Response.status = new ABCRecurringStatusModel();
                    Response.status.message = result;

                    //Save ABC Logs into DB
                    await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonData, PostData, Source, result, Response.status.message, "MemberRecurringServiceAgreement", PostData.PersonalInformation.MemberId,
                         MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                    checkOutMessageLogDetail("Create Member Recurring Service : ", e.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, null, null);

                }
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "CreateMemberRecurringService(MemberSignUpRequestModel PostData, RecurringServicePlan recurringPlan, string Source, int MemberlogId, long NewPTMemberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                _abclogger.SendEmail(ex, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);

                Response.status = new ABCRecurringStatusModel();
                Response.status.message = ex.ToString();

                //Save ABC Logs into DB
                await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonData, PostData, Source, Response.status.message, "Internal Server Error", "MemberRecurringServiceAgreement", PostData.PersonalInformation.MemberId,
                    MemberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                checkOutMessageLogDetail("Create Member Recurring Service : ", ex.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, null, null);

            }
            return Response;
        }

        public static ABCMemberModel GetMemberInformationByMemberId(string clubNumber, string memberId)
        {
            bool BlnBreak = false;
            int reAttemptIndex = 1;

            ABCMemberModel Response = new ABCMemberModel();

            while (!BlnBreak)
            {
                var ResponseData = GetMemberInformationByMemberIdFromABC(clubNumber, memberId);
                if (ResponseData.Response != null)
                {
                    Response = ResponseData.Response;
                }

                if (ResponseData.Reattempt.HasValue && ResponseData.Reattempt.Value)
                {
                    reAttemptIndex++;
                    if (reAttemptIndex < 4)
                        continue;
                }

                if (ResponseData.StopRequest.HasValue && ResponseData.StopRequest.Value)
                {
                    BlnBreak = true;
                }

                reAttemptIndex = 1;
            }

            return Response;
        }

        #region manage Membership flow

        public async Task<UpdateMemberPersonalInfoResponse> UpdateMemberPersonalInfo(PersonalInfoModel postData)
        {
            UpdateMemberPersonalInfoResponse result = new UpdateMemberPersonalInfoResponse();
            var currentUrl = string.Format(ABC.ABCUpdateMemberPersonalInfoURL, postData.clubNumber, postData.memberId);

            checkOutMessageLogDetail("Update Member Personal Info : ", "Update Member Personal Info", postData.FirstName, postData.LastName, postData.email, null, null, null);

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);


                //Remove from live While making live
                //ServicePointManager.DefaultConnectionLimit = 1000;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------

                UpdateMemberPersonalInfoData PostData = new UpdateMemberPersonalInfoData();
                postData.sendEmail = true;
                PostData.requestBody = postData;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonData = JsonConvert.SerializeObject(PostData.requestBody);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var res = streamReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject<UpdateMemberPersonalInfoResponse>(res);
                        result.jsonResponse = res;
                        return result;
                    }
                }
                checkOutMessageLogDetail("Update Member Personal Info : ", httpResponse.StatusCode.ToString().ToUpper(), postData.FirstName, postData.LastName, postData.email, null, null, null);
            }
            catch (Exception ex)
            {
                _abclogger.SendEmail(ex, "clubNumber = " + postData.clubNumber + ", memberId = " + postData.memberId);
                //var MemberlogId = SaveABCLog("", "", "UpdateMemberPersonalInfo", currentUrl, postData.clubNumber.ToString(), null, null, null, postData.memberId, null, null, null, ex.Message, null);
                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateMemberPaymentInfo(string clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                checkOutMessageLogDetail("Update Member Personal Info : ", ex.ToString(), postData.FirstName, postData.LastName, postData.email, null, null, null);
            }
            return result;
        }

        public async Task<UpdateMemberPaymentInfoResponse> UpdateMemberPaymentInfo(PaymentInfoModel PostData)
        {
            UpdateMemberPaymentInfoResponse Response = new UpdateMemberPaymentInfoResponse();
            var currentUrl = string.Format(ABC.ABCUpdateMemberPaymentInfo, PostData.clubNumber, PostData.memberId);
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                //Remove from live While making live
                //ServicePointManager.DefaultConnectionLimit = 1000;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------

                CardOnFileModelRequestModel request = new CardOnFileModelRequestModel();

                CreditCard creditCard = new CreditCard();

                creditCard.creditCardFirstName = PostData.paymentInformation.creditCardFirstName != null ? PostData.paymentInformation.creditCardFirstName.GetString() : "";
                creditCard.creditCardLastName = PostData.paymentInformation.creditCardLastName != null ? PostData.paymentInformation.creditCardLastName.GetString() : "";
                creditCard.creditCardType = PostData.paymentInformation.creditCardType != null ? Regex.Replace(PostData.paymentInformation.creditCardType, @"\s+", "") : "";
                creditCard.creditCardAccountNumber = PostData.paymentInformation.creditCardNumber != null ? PostData.paymentInformation.creditCardNumber.Replace(" ", "") : "";
                //PostData.paymentInformation.creditCardNumber : "";
                creditCard.creditCardExpMonth = PostData.paymentInformation.creditCardExpMonth != null ? PostData.paymentInformation.creditCardExpMonth.month : "";
                creditCard.creditCardExpYear = PostData.paymentInformation.creditCardExpYear != null ? PostData.paymentInformation.creditCardExpYear : "";

                request.creditCard = creditCard;
                request.sendEmail = "true";

                string jsonData = "";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonData = JsonConvert.SerializeObject(request);

                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        Response = JsonConvert.DeserializeObject<UpdateMemberPaymentInfoResponse>(result);
                        var Response1 = JsonConvert.SerializeObject(result);


                        //if (Response != null && Response.status != null && Response.status.count == "1" && Response.status.message == "success")
                        //{
                        //   // Card on file added
                        //    _logger.Log("httpResponse.StatusCode=OK " + result + "\n");
                        //    _logger.Log("Request " + jsonData + "\n");
                        //    _logger.Log("Response " + Response1 + "\n");
                        //}
                        //else
                        //{
                        //    _logger.Log("httpResponse.StatusCode=OK " + result + "\n");
                        //    _logger.Log("Request " + jsonData + "\n");
                        //    _logger.Log("Response " + Response1 + "\n");
                        //    // success
                        //}

                        return Response;
                    }
                }
                else
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        _logger.Log("httpResponse.StatusCode= " + " " + httpResponse.StatusCode.ToString() + " " + result);
                    }
                }

            }
            catch (Exception ex)
            {

                _abclogger.SendEmail(ex, "clubNumber = " + PostData.clubNumber + ", memberId = " + PostData.memberId);
                //var MemberlogId = SaveABCLog("", "", "UpdateMemberPaymentInfo", currentUrl, PostData.clubNumber.ToString(), null, null, null, PostData.memberId, null, null, null, ex.Message, null);
                //ExternalExceptionLogger.LogException(ex, _controllerName, "UpdateMemberPaymentInfo(string clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
            }
            return Response;
        }
        #endregion manage Membership flow
        [Obsolete]
        public class YoufitWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                int timeout = 20 * 60 * 1000;
                w.Timeout = timeout;
                ((HttpWebRequest)w).ReadWriteTimeout = timeout;
                return w;
            }
        }

        private static ABCMemberResponseExceptionModel GetMemberInformationByMemberIdFromABC(string clubNumber, string memberId)
        {
            string responseData = string.Empty;
            string jsonHttpWebResponse = string.Empty;
            var currentUrl = string.Format(MemberCheckOutSettings.ABCGetMemberInformationURL, clubNumber, memberId);
            try
            {
                using (var client = new YoufitWebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("app_id", ABC_CredentialData.Result.username);
                    client.Headers.Add("app_key", ABC_CredentialData.Result.password);
                    client.Headers.Add("cache-control", "no-cache");

                    //Remove from live While making live
                    //ServicePointManager.DefaultConnectionLimit = 1000;
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    responseData = client.DownloadString(currentUrl);

                    if (responseData.Contains("<count>0</count>"))
                    {
                        return new ABCMemberResponseExceptionModel() { StopRequest = true };
                    }

                    var response = (ABCMemberModel)JsonConvert.DeserializeObject<ABCMemberModel>(responseData);

                    if (response.members != null)
                    {
                        return new ABCMemberResponseExceptionModel() { Response = response, StopRequest = response.members.Count < 5000 };
                    }
                    else
                    {
                        return new ABCMemberResponseExceptionModel() { StopRequest = true };
                    }
                }
            }
            catch (WebException e)
            {
                //ExternalExceptionLogger.LogException(e, _controllerName, "GetMemberInformationByMemberIdFromABC(string clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                _abclogger.SendEmail(e, "clubNumber = " + clubNumber + ", memberId = " + memberId);
                return new ABCMemberResponseExceptionModel() { StopRequest = e.Message.Contains("401") || e.Message.Contains("500"), Reattempt = true };
            }
            catch (Exception ex)
            {
                //ExternalExceptionLogger.LogException(ex, _controllerName, "GetMemberInformationByMemberIdFromABC(string clubNumber, string memberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber + ", memberId = " + memberId);
                //var MemberlogId = SaveABCLog(responseData, jsonHttpWebResponse, "GetMemberInformationByMemberIdFromABC", currentUrl, clubNumber, null, null, null, memberId, null, null, null, ex.Message, null);
                return new ABCMemberResponseExceptionModel() { StopRequest = ex.Message.Contains("401") || ex.Message.Contains("500"), Reattempt = true };
            }
        }

        public async Task<UpdateMemberPaymentInfoResponse> AddCardOnFile(MemberCheckOutInitialModel PostData, string Source, int memberlogId, long NewPTMemberId, RecurringServicePlan recurringPlan)
        {
            var methodInfo = MethodBase.GetCurrentMethod();
            var fullName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            checkOutMessageLogDetail("Add Card On File : ", "Add Card On File", PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);


            UpdateMemberPaymentInfoResponse Response = new UpdateMemberPaymentInfoResponse();
            CardOnFileModelRequestModel request = new CardOnFileModelRequestModel();
            var currentUrl = string.Format(ABC.ABCMemberAddCardOnFile, PostData.PlanInitialInformation.ClubNumber, PostData.PersonalInformation.MemberId);
            string jsonHttpWebRequest = string.Empty;
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(currentUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.Accept = "application/json;charset=UTF-8";
                httpWebRequest.Headers.Add("app_id", ABC_CredentialData.Result.username);
                httpWebRequest.Headers.Add("app_key", ABC_CredentialData.Result.password);

                //Remove from live While making live
                //ServicePointManager.DefaultConnectionLimit = 1000;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //------------

                CreditCard creditCard = new CreditCard();

                creditCard.creditCardFirstName = PostData.PTPaymentInformation.CreditCardFirstName.GetString();
                creditCard.creditCardLastName = PostData.PTPaymentInformation.CreditCardLastName.GetString();
                creditCard.creditCardType = Regex.Replace(PostData.PTPaymentInformation.CreditCardType, @"\s+", "");
                creditCard.creditCardAccountNumber = PostData.PTPaymentInformation.CreditCardNumber.Replace(" ", "");
                creditCard.creditCardExpMonth = PostData.PTPaymentInformation.CreditCardExpMonth;
                creditCard.creditCardExpYear = PostData.PTPaymentInformation.CreditCardExpYear;
                //creditCard.creditCardPostalCode = PostData.PTPaymentObjInfo.PaymentInformation.CreditCardZipCode;
                //creditCard.creditCardCvvCode = PostData.PTPaymentObjInfo.PaymentInformation.CreditCardCVV;

                request.creditCard = creditCard;
                request.sendEmail = "true";

                jsonHttpWebRequest = JsonConvert.SerializeObject(request);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonHttpWebRequest);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    if (httpResponse.StatusCode.ToString().ToUpper() == "OK")
                    {
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var jsonHttpWebResponse = streamReader.ReadToEnd();

                            Response = JsonConvert.DeserializeObject<UpdateMemberPaymentInfoResponse>(jsonHttpWebResponse);
                            var Response1 = JsonConvert.SerializeObject(jsonHttpWebResponse);

                            //Save ABC Logs into DB
                            await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, "Member(Refresh)", jsonHttpWebResponse, Response.status.message, "AddCardOnFile", PostData.PersonalInformation.MemberId,
                                memberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);

                            checkOutMessageLogDetail("Add Card On File : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);

                            return Response;
                        }
                    }
                    else
                    {
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var jsonHttpWebResponse = streamReader.ReadToEnd();
                            Response.status = new Status();
                            Response.status.message = jsonHttpWebResponse;
                            Response.status.count = "0";

                            checkOutMessageLogDetail("Add Card On File : ", httpResponse.StatusCode.ToString().ToUpper(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);

                            //Save ABC Logs into DB
                            await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, "Member(Refresh)", jsonHttpWebResponse, Response.status.message, "AddCardOnFile", PostData.PersonalInformation.MemberId,
                                memberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);
                        }
                    }
                }
            }
            catch (WebException e)
            {
                WebResponse errResp = e.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    checkOutMessageLogDetail("Add Card On File (web exception) : ", text, PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);
                }

                checkOutMessageLogDetail("Add Card On File : ", e.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);

                _abclogger.SendEmail(e, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);
                using (StreamReader r = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream()))
                {
                    var jsonHttpWebResponse = r.ReadToEnd();
                    var _response = JsonConvert.DeserializeObject<dynamic>(jsonHttpWebResponse);
                    Response.status = new Status();
                    Response.status.message = jsonHttpWebResponse;
                    var _message = (string)_response?.status?.message;

                    //Save ABC Logs into DB
                    await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, "Member(Refresh)", jsonHttpWebResponse, Response.status.message, "AddCardOnFile", PostData.PersonalInformation.MemberId,
                        memberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);
                }
            }
            catch (Exception ex)
            {
                checkOutMessageLogDetail("Add Card On File (Exception): ", ex.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, NewPTMemberId.ToString(), null, Source);

                _abclogger.SendEmail(ex, "clubNumber = " + PostData.PlanInitialInformation.ClubNumber + ", memberId = " + PostData.PersonalInformation.MemberId);
                Response.status = new Status();
                Response.status.message = ex.ToString();

                //Save ABC Logs into DB
                await ABCLoggerService.SAVEABCLogs_MemberRecurringServiceAgreement(jsonHttpWebRequest, PostData, "Member(Refresh)", Response.status.message, "Internal Server Error", "AddCardOnFile", PostData.PersonalInformation.MemberId,
                    memberlogId, NewPTMemberId, recurringPlan, currentUrl, fullName);
            }
            return Response;
        }

        #region staff
        public string ImportClubCheckinsData(string clubNumber, DateTime fromDate, DateTime toDate)
        {
            int reAttemptIndex;

            fromDate = Convert.ToDateTime(fromDate.ToString("yyyy-MM-dd"));
            toDate = Convert.ToDateTime(toDate.ToString("yyyy-MM-dd")).AddDays(1).AddSeconds(-5);
            bool BlnBreak = false;
            int page = 1;
            reAttemptIndex = 1;

            List<ABCCheckInModel> checkinsModels = new List<ABCCheckInModel>();

            while (!BlnBreak)
            {
                var response = PullCheckinsData(clubNumber, fromDate, toDate, page);
                if (response.Response != null)
                {
                    checkinsModels.AddRange(response.Response.checkins);
                }

                if (response.Reattempt.HasValue && response.Reattempt.Value)
                {
                    reAttemptIndex++;
                    if (reAttemptIndex < 4)
                        continue;
                }

                if (response.StopRequest.HasValue && response.StopRequest.Value)
                {
                    BlnBreak = true;
                }

                if (page > 50)
                {
                    BlnBreak = true;
                }

                reAttemptIndex = 1;
                page++;
            }

            if (checkinsModels != null && checkinsModels.Count > 0)
            {
                var checkinDetailXML = new XElement("Checkins",
                            from res in checkinsModels
                            select new XElement("C",
                                    new XElement("mid", res.member.memberId),
                                    new XElement("cts", res.checkInTimestamp)
                                )
                         );

                return Regex.Replace(checkinDetailXML.ToString(SaveOptions.DisableFormatting), @"\t|\n|\r", "");
            }

            return string.Empty;
        }

        private static ResponseException PullCheckinsData(string clubNumber, DateTime fromDate, DateTime toDate, int page)
        {
            checkOutMessageLogDetail("PullCheckinsData : ", "PullCheckinsData", null, null, null, null, null, null);

            string jsonHttpWebRequest = string.Empty;
            string response = string.Empty;
            var currentUrl = string.Format(ABC.CheckinUrl, clubNumber, GetDateRange(fromDate, toDate), page);
            try
            {
                using (var client = new YoufitWebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("app_id", ABC_CredentialData.Result.username);
                    client.Headers.Add("app_key", ABC_CredentialData.Result.password);
                    client.Headers.Add("cache-control", "no-cache");

                    //Remove from live While making live
                    //ServicePointManager.DefaultConnectionLimit = 1000;
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    response = client.DownloadString(currentUrl);
                    if (response.Contains("<count>0</count>"))
                    {
                        return new ResponseException() { StopRequest = true };
                    }
                    var responseData = JsonConvert.DeserializeObject<ABCCheckInResponse>(response);
                    if (responseData.checkins != null)
                    {
                        return new ResponseException() { Response = responseData, StopRequest = responseData.checkins.Count < 5000 };
                    }

                    else
                    {
                        return new ResponseException() { StopRequest = true };
                    }
                }
            }
            catch (Exception ex)
            {
                checkOutMessageLogDetail("PullCheckinsData : ", ex.ToString(), null, null, null, null, null, null);

                //ExternalExceptionLogger.LogException(ex, _controllerName, "PullCheckinsData(string clubNumber, DateTime fromDate, DateTime toDate, int page)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);
                _abclogger.SendEmail(ex, "clubNumber = " + clubNumber);
                //var MemberlogId = SaveABCLog(jsonHttpWebRequest, response, "PullCheckinsData", currentUrl, clubNumber, null, null, null, null, null, null, null, ex.Message, null);
                return new ResponseException() { StopRequest = ex.Message.Contains("401") || ex.Message.Contains("500"), Reattempt = true };
            }
        }

        private static string GetDateRange(DateTime fromDate, DateTime? toDate)
        {
            string range = fromDate.ToString("yyyy-MM-dd HH:mm:ss.000000");
            if (toDate != null && toDate != DateTime.MinValue) range += "," + toDate.Value.ToString("yyyy-MM-dd HH:mm:ss.000000");
            return range;
        }

        #endregion staff


        #region General Log Method
        public static int SaveABCLog(string JsonRequest, string JsonResponse, string MethodName, string APIUrl, string Club, string FirstName,
            string LastName, string Email, string MemberId, string PlanName, string AgreementType, string Source, string Message, long? NewMemberId)
        {
            try
            {
                NewMemberABCLog newMemberABC = new NewMemberABCLog
                {
                    AgreementType = AgreementType,
                    APIUrl = APIUrl,
                    Club = Club,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    MemberId = MemberId,
                    PlanName = PlanName,
                    CreatedBy = $@"{FirstName} {LastName}",
                    JsonRequest = JsonRequest,
                    JsonResponse = JsonResponse,
                    Message = Message,
                    MethodName = MethodName,
                    NewMemberId = NewMemberId,
                    Source = Source
                };
                return ABCLoggerService.SaveABCLogDetails(newMemberABC);
            }
            catch (Exception ex)
            {

                //ExternalExceptionLogger.LogException(ex, _controllerName, "SaveABCLog(string JsonRequest, string JsonResponse, string MethodName, string APIUrl, string Club, string FirstName, string LastName, string Email, string MemberId, string PlanName, string AgreementType, string Source, string Message, long? NewMemberId)", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);

                _logger.Log("Error = " + " " + ex.ToString());
                return 0;
            }
        }
        #endregion
        private static void checkOutMessageWriteLog(string pStrMsg, string FirstName, string LastName)
        {
            string LogFilePath = Environment.CurrentDirectory + "\\LogFiles\\checkOutMessageLog_" + FirstName + "_" + LastName + "_" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }
            StreamWriter file2 = new StreamWriter(LogFilePath, true);
            file2.WriteLine("\n" + DateTime.UtcNow.ToString());
            file2.WriteLine(pStrMsg);
            file2.Close();
        }

        public static void checkOutMessageLogDetail(string log, string Message, string FirstName, string LastName, string Email, string MemberId, string clubNumber, string sourcename)
        {
            var txtmsg = Message != null ? Message : null;
            var msg = txtmsg.ToLower() == "success" ? "success(green)" : txtmsg.ToLower() == "-111" ? "warning(yellow)" : "error(red)";
            string message = log + ("ClubNumber : " + clubNumber, "\n Name : " + FirstName + " " + LastName, "\n Email : " + Email, "\n MemberId : " + MemberId, "\n SourceName : " + sourcename, "\n Message : " + Message, "\n");
            Console.WriteLine(message);
            checkOutMessageWriteLog(message, FirstName, LastName);
        }

        public static ABCResponsePaymentPlan ReadPlanById(string clubNumber, string planId)
        {
            try
            {
                string ABCMemberSignUpPlanDetailByIdURL = ABC.ABCMemberSignUpPlanDetailByIdURL;

                var currentUrl = string.Format(ABCMemberSignUpPlanDetailByIdURL, clubNumber, planId);
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("app_id", ABC_CredentialData.Result.username);
                    client.Headers.Add("app_key", ABC_CredentialData.Result.password);

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var response = client.DownloadString(currentUrl);
                    if (response.Contains("<count>0</count>"))
                    {
                        return new ABCResponsePaymentPlan();
                    }
                    response = response.Replace("$", "");
                    ABCResponsePaymentPlan responseMain = new ABCResponsePaymentPlan();
                    responseMain.response = response;
                    var responseData = (ABCResponsePaymentPlan)JsonConvert.DeserializeObject<ABCResponsePaymentPlan>(response);
                    return responseData;
                }
            }
            catch (Exception ex)
            {
                return new ABCResponsePaymentPlan();
            }
        }
    }
}
