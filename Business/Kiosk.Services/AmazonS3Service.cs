using Amazon.S3.Transfer;
using Amazon.S3;
using AutoMapper;
using Kiosk.Interfaces.Services;
using Kiosk.Repositories;
using Kiosk.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosk.Business.Model.Checkout;
using Kiosk.Interfaces.Repositories;
using Kiosk.Business.Helpers;
using Amazon;
using Kiosk.Domain.Models;
using AutoMapper.Execution;
using System.IO;
using Kiosk.Domain.Models.SPModel;
using System.Data;
using Kiosk.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Services
{
    public partial class AmazonS3Service : ServiceBase, IAmazonS3Service
    {
        public readonly IGuestRepository _guestRepository;
        public readonly Logger _logger = new Logger();
        public readonly ABCLoggerService _abclogger;
        protected KioskContext _context;

        public AmazonS3Service(IUnitOfWork unitOfWork, IMapper mapper, IGuestRepository guestRepository, KioskContext context) : base(unitOfWork, mapper)
        {
            _guestRepository = guestRepository;
            _context = context;
        }

        public async Task<string> AgreementContractUploadFile(int Id, MemberCheckOutInitialModel PostData, string AgreementType)
        {
            _logger.checkOutMessageLogDetail("AgreementContractUploadFile :", "AgreementContractUploadFile", PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, PostData.PlanInitialInformation.ClubNumber, PostData.PlanInitialInformation.SourceName);

            try
            {
                var MemberShipAgreementType = MemberCheckOutSettings.MemberShipAgreementType;
                var PTAgreementType = MemberCheckOutSettings.PTAgreementType;
                var sgtAgreementType = MemberCheckOutSettings.SGTAgreementType;

                //var data = await _guestRepository.GetStatusByClubNumber(PostData.PlanInitialInformation.ClubNumber);
                //usp_GetClubStateBYClubNumber_Result stateclubs = new usp_GetClubStateBYClubNumber_Result();
                string stateclubs = "";
                try
                {
                    var database = _context;
                    var connection = database.Database.GetDbConnection();
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();
                    command.CommandText = "[Kiosk].[usp_GetClubStateBYClubNumber]";
                    command.CommandType = CommandType.StoredProcedure;
                    var Data = command.CreateParameter(); Data.ParameterName = "@ClubNumber"; Data.Value = PostData.PlanInitialInformation.ClubNumber; command.Parameters.Add(Data);

                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        stateclubs = (string)((!DBNull.Value.Equals(reader["State"])) ? reader["State"] : null);
                    }
                    await connection.CloseAsync();
                    _logger.checkOutMessageLogDetail("usp_GetClubStateBYClubNumber : ", stateclubs, PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, null, PostData.PlanInitialInformation.ClubNumber, PostData.PlanInitialInformation.SourceName);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    _logger.Log("GetClubByUserList Method from ClubRepository" + ex.ToString());
                }

                string State = !string.IsNullOrEmpty(stateclubs) ? stateclubs : "";

                // Export PDF FROM SSRS and STORE in amzon s3 bucket
                byte[] pdfByteArray = null;

                if (MemberCheckOutSettings.ABCAgreementResponsecount == "true" || MemberCheckOutSettings.SavePTMemberInformationLocalDBMessage == "true" || MemberCheckOutSettings.SaveSGTMemberInformationLocalDBMessage == "true")
                {
                    _logger.checkOutMessageLogDetail("Agreement contract URL From ABC : ", AgreementType, PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, null, PostData.PlanInitialInformation.ClubNumber, PostData.PlanInitialInformation.SourceName);
                }

                if (AgreementType == MemberShipAgreementType)
                {
                    pdfByteArray = await _guestRepository.ExportMemberShipPDFFromSSRS(Id, PostData.PersonalInformation.MemberId, State, false, PostData.PlanInitialInformation.PlanTypeDetail, false, PostData.PlanInitialInformation.PlanType, PostData.PlanInitialInformation.PlanName);
                }
                else if (AgreementType == PTAgreementType)
                {
                    pdfByteArray = await _guestRepository.ExportPDFFromSSRS(Id, PostData.PersonalInformation.MemberId, Convert.ToInt32(PostData.PlanInitialInformation.ClubNumber), State, false, PostData.PlanInitialInformation.PTPlanType, true, false, PostData.PlanInitialInformation.PtPlanNameType, PostData.PlanInitialInformation.PTPlanName);
                }
                else if (AgreementType == sgtAgreementType)
                {
                    pdfByteArray = await _guestRepository.ExportPDFFromSSRS(Id, PostData.PersonalInformation.MemberId, Convert.ToInt32(PostData.PlanInitialInformation.ClubNumber), State, false, PostData.PlanInitialInformation.PTPlanType, true, false, PostData.PlanInitialInformation.SGTPlanNameType, PostData.PlanInitialInformation.PTPlanName);
                }
                string bucketName = AmazonSettings.AmazonS3BucketName;
                string AWSAccessKey = AmazonSettings.AWSAccessKey;
                string AWSSecretKey = AmazonSettings.AWSSecretKey;

                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(AWSAccessKey, AWSSecretKey);
                RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
                IAmazonS3 s3Client = new AmazonS3Client(awsCredentials, bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);

                string memberRecurringId = AgreementType == MemberShipAgreementType ? PostData.PersonalInformation.MemberId : AgreementType == PTAgreementType ? PostData.PersonalInformation.RecurringServiceId : "";
                string fileName = PostData.PersonalInformation.FirstName + PostData.PersonalInformation.LastName + memberRecurringId + ".pdf";
                string restBucketPath = string.Format(@"{0}/{1}/{2}/{3}", PostData.PlanInitialInformation.ClubNumber, DateTime.Today.Year, DateTime.Today.ToString("MM"), DateTime.Today.ToString("dd"));
                bucketName = bucketName + "/" + restBucketPath;

                var httpPostedFileBase = new FileSettings(pdfByteArray, fileName, "pdf");

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    InputStream = httpPostedFileBase.InputStream,
                    Key = fileName,
                    StorageClass = S3StorageClass.StandardInfrequentAccess
                };
                fileTransferUtilityRequest.Metadata.Add("PhoneNumber", PostData.PersonalInformation.PhoneNumber);
                fileTransferUtilityRequest.Metadata.Add("Email", PostData.PersonalInformation.Email);
                fileTransferUtility.Upload(fileTransferUtilityRequest);

                return bucketName + "/" + fileName;
            }
            catch (AmazonS3Exception e)
            {
                _logger.checkOutMessageLogDetail("AgreementContractUploadFile :", e.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, PostData.PlanInitialInformation.ClubNumber, PostData.PlanInitialInformation.SourceName);

                _abclogger.SendEmail(e, "Club = " + PostData.PlanInitialInformation.ClubNumber + ", Email = " + PostData.PersonalInformation.Email + ", PhoneNumber = " + PostData.PersonalInformation.PhoneNumber + ", MemberFullName = " + PostData.PersonalInformation.FirstName + " " + PostData.PersonalInformation.LastName);
                _logger.Log("AgreementContractUploadFile (AmazonS3Exception) : " + e.ToString() + "\nId : " + Id.ToString());
                throw e;
            }
            catch (Exception e)
            {
                _logger.checkOutMessageLogDetail("AgreementContractUploadFile :", e.ToString(), PostData.PersonalInformation.FirstName, PostData.PersonalInformation.LastName, PostData.PersonalInformation.Email, PostData.PersonalInformation.MemberId, PostData.PlanInitialInformation.ClubNumber, PostData.PlanInitialInformation.SourceName);
                _abclogger.SendEmail(e, "Club = " + PostData.PlanInitialInformation.ClubNumber + ", Email = " + PostData.PersonalInformation.Email + ", PhoneNumber = " + PostData.PersonalInformation.PhoneNumber + ", MemberFullName = " + PostData.PersonalInformation.FirstName + " " + PostData.PersonalInformation.LastName);
                _logger.Log("AgreementContractUploadFile : " + e.ToString() + "\nId : " + Id.ToString());
                throw e;
            }
        }


    }
}
