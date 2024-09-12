using Microsoft.Extensions.Configuration;
using System;
using System.Reflection.Metadata;

namespace Kiosk.Business.Helpers
{
    public partial class AppSettings
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiUrl => _configuration.GetSection("Api")["ApiUrl"];

        public static string DefaultConnectionString => _configuration.GetConnectionString("DefaultConnection");
        public static string KioskConnectionString => _configuration.GetConnectionString("KioskConnection");
        public static string PrdPlanConnectionString => _configuration.GetConnectionString("PrdPlanConnection");
        public static string ABCConnectionString => _configuration.GetConnectionString("ABCConnectionString");
        public static string DownloadFileName => _configuration.GetSection("Path").GetSection("DownloadFileName").Value;
        public static string TwoFactorAuthUrl => _configuration.GetSection("TwoAuthRedirection").GetSection("RedirectUrl").Value;
        public static string LogPath => _configuration.GetSection("Path").GetSection("LogFile").Value;

    }

    public partial class MailSettings
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  string MailFrom => _configuration.GetSection("MailSettings")["From"];
        public  string MailHost => _configuration.GetSection("MailSettings")["Host"];
        public  string MailPort => _configuration.GetSection("MailSettings")["Port"];
        public  string MailPassword => _configuration.GetSection("MailSettings")["Password"];
        public  bool EnableMail => Convert.ToBoolean(_configuration.GetSection("MailSettings")["EnableMail"]);

        public static string SMTPHost => _configuration.GetSection("MailSettings")["SMTPHost"];
        public static string SMTPFromMail => _configuration.GetSection("MailSettings")["SMTPFromMail"];
        public static string SMTPToMail => _configuration.GetSection("MailSettings")["SMTPToMail"];
        public static string SMTPEmailPassword => _configuration.GetSection("MailSettings")["SMTPEmailPassword"];
        public static string SMTPPort => _configuration.GetSection("MailSettings")["SMTPPort"];


        public static string Server => _configuration.GetSection("MailSettings")["Server"];
        public static string SenderName => _configuration.GetSection("MailSettings")["SenderName"];
        public static string SenderEmail => _configuration.GetSection("MailSettings")["SenderEmail"];
        public static int PortNumber => Convert.ToInt32(_configuration.GetSection("MailSettings")["PortNumber"]);
        public static string EmailPassword => _configuration.GetSection("MailSettings")["EmailPassword"];
        public static string ReceiverEmail => _configuration.GetSection("MailSettings")["ReceiverEmail"];


    }

    public partial class Jwt
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Key => _configuration.GetSection("Jwt")["Key"];
        public string Issuer => _configuration.GetSection("Jwt")["Issuer"];
    }


    public partial class HubSpotConfig
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string Token => _configuration.GetSection("HubSpot")["Token"];
        public static string CreateContacts => _configuration.GetSection("HubSpot")["CreateContacts"];
        public static string SearchContactsApi => _configuration.GetSection("HubSpot")["SearchContactsApi"];
        public static string[] SearchContractProperties => _configuration.GetSection("HubSpot")["SearchContractProperties"].Split(',');
        public static string EmailPropertyName => _configuration.GetSection("HubSpot")["EmailPropertyName"];
        public static string PhonePropertyName => _configuration.GetSection("HubSpot")["PhonePropertyName"];
        public static string MemberIDProperty => _configuration.GetSection("HubSpot")["MemberIDProperty"];
        public static string ReferringMemberIDProperty => _configuration.GetSection("HubSpot")["ReferringMemberIDProperty"];
        public static string EqualOperator => _configuration.GetSection("HubSpot")["EqualOperator"];
        public static string HSObjectId => _configuration.GetSection("HubSpot")["HSObjectId"];
        public static string ContainsTokenOperator => _configuration.GetSection("HubSpot")["ContainsTokenOperator"];
        public static string CreateDatePropertyName => _configuration.GetSection("HubSpot")["CreateDatePropertyName"];
        public static string DescendingDirectionName => _configuration.GetSection("HubSpot")["DescendingDirectionName"];
        public static int HubSpotLimit => Convert.ToInt32(_configuration.GetSection("HubSpot")["HubSpotLimit"]);
        public static string EmailHubSpotLimit => _configuration.GetSection("HubSpot")["EmailHubSpotLimit"];
        public static string MemberStatusReasonActiveValues => _configuration.GetSection("HubSpot")["MemberStatusReasonActiveValues"];
        public static string MemberStatusReasonInactiveValues => _configuration.GetSection("HubSpot")["MemberStatusReasonInactiveValues"];
        public static string HubSpotDomain => _configuration.GetSection("HubSpot")["HubSpotDomain"];
        public static string HSIdField => _configuration.GetSection("HubSpot")["HSIdField"];
        public static string UpdateContactUrl => _configuration.GetSection("HubSpot")["UpdateContactUrl"];
        public static string GetAppointmentIdAPI => _configuration.GetSection("HubSpot")["GetAppointmentIdAPI"];
        public static string GetAppointmentDetail => _configuration.GetSection("HubSpot")["GetAppointmentDetail"];
        public static string[] AppointmentProperties => _configuration.GetSection("HubSpot")["AppointmentProperties"].Split(',');
        public static string UpdateApptStatusAPI => _configuration.GetSection("HubSpot")["UpdateApptStatusAPI"];
        public static string ApptStatusShowValue => _configuration.GetSection("HubSpot")["ApptStatusShowValue"];

    }

    public partial class ABC
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string APIname => _configuration.GetSection("ABC")["ABCAPIName"];
        public static string ABCMemberSignUpPlanURL => _configuration.GetSection("ABC")["ABCMemberSignUpPlanURL"];
        public static string[] FlashSaleMembershipPlan => _configuration.GetSection("ABC")["FlashSaleMembershipPlan"].ToLower().Split(',');
        public static string[] YearlyPlan => _configuration.GetSection("ABC")["YearlyPlan"].ToLower().Split(',');
        public static string RemoveFeatureInYearlyTwoMonthsPlan => _configuration.GetSection("ABC")["RemoveFeatureInYearlyTwoMonthsPlan"];
        public static string TwoMonthFreePlan => _configuration.GetSection("ABC")["TwoMonthFreePlan"];
        public static string MonthPIF => _configuration.GetSection("ABC")["MonthPIF"];
        public static string AnnualFeeDueThirtyDays => _configuration.GetSection("ABC")["AnnualFeeDueThirtyDays"];
        public static string AnnualFeeDueSixtyDays => _configuration.GetSection("ABC")["AnnualFeeDueSixtyDays"];
        public static string ABCClubWisePlansURL => _configuration.GetSection("ABC")["ABCClubWiseRecurringServicePlansURL"];
        public static string StatePrefixes => _configuration.GetSection("ABC")["StatePrefixes"];
        public static string ClubPrefixes => _configuration.GetSection("ABC")["ClubPrefixes"];
        public static string RemoveWordsPTPlan => _configuration.GetSection("ABC")["RemoveWordsPTPlan"];
        public static string ABCPlanDetailsURL => _configuration.GetSection("ABC")["ABCRecurringServicePlanDetailByIdURL"];
        public static string CheckinUrl => _configuration.GetSection("ABC")["ABCCheckinServiceURL"];
        public static string ABCMemberSignUpPlanDetailByIdURL => _configuration.GetSection("ABC")["ABCMemberSignUpPlanDetailByIdURL"];
        public static string GuestCheckInEnrtySource => _configuration.GetSection("ABC")["GuestCheckInEnrtySource"];
        public static string ClassPassEnrtySource => _configuration.GetSection("ABC")["ClassPassEnrtySource"];
        public static string GuestForTheDayEnrtySource => _configuration.GetSection("ABC")["GuestForTheDayEnrtySource"];
        public static string CanSaveLeadLocalDB => _configuration.GetSection("ABC")["CanSaveLeadLocalDB"];
        public static string ABCProspectURL => _configuration.GetSection("ABC")["ABCProspectURL"];
        public static string ABCServiceURL => _configuration.GetSection("ABC")["ABCServiceURL"];
        public static string SilverFit_ASH_PromoCode => _configuration.GetSection("ABC")["SilverFit_ASH_PromoCode"];
        public static string ABCUpdateMemberPersonalInfoURL => _configuration.GetSection("ABC")["ABCUpdateMemberPersonalInfoURL"];
        public static string ABCUpdateMemberPaymentInfo => _configuration.GetSection("ABC")["ABCUpdateMemberPaymentInfo"];
        public static string ABCClubAccountPTPaymentMethod => _configuration.GetSection("ABC")["ABCClubAccountPTPaymentMethod"];
        public static string CanSavePTInfoInABC => _configuration.GetSection("ABC")["CanSavePTInfoInABC"];
        public static string CanSaveSGTInfoInABC => _configuration.GetSection("ABC")["CanSaveSGTInfoInABC"];
        public static string ABCMemberAddCardOnFile => _configuration.GetSection("ABC")["ABCMemberAddCardOnFile"];
        public static string ABCErrorMessages => _configuration.GetSection("ABC")["ABCErrorMessages"];

    }

    public partial class MemberPlanList
    {
        private static IConfiguration _configuration;
        
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string smallGroupTrainingTabName => _configuration.GetSection("MemberPlanList")["smallGroupTrainingTabName"];
        public static string PTType => _configuration.GetSection("MemberPlanList")["PT"];
        public static string SGTType => _configuration.GetSection("MemberPlanList")["SGT"];
    }

    public partial class FreshEmail
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string CanValidateEmail => _configuration.GetSection("FreshEmail")["CanValidateEmail"];
        public static string FreshAddressServiceURL => _configuration.GetSection("FreshEmail")["FreshAddressServiceURL"];
    }

    public partial class AmazonSettings
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string AmazonS3URL => _configuration.GetSection("Amazon")["AmazonS3URL"];
        public static string AmazonS3BucketName_Contract => _configuration.GetSection("Amazon")["AmazonS3BucketName_Contract"];
        public static string AmazonS3BucketName_Disclaimer => _configuration.GetSection("Amazon")["AmazonS3BucketName_Disclaimer"];
        public static string AmazonS3BucketClubNumbers => _configuration.GetSection("Amazon")["AmazonS3BucketClubNumbers"];
        public static string AmazonS3BucketName => _configuration.GetSection("Amazon")["AmazonS3BucketName"];
        public static string AWSAccessKey => _configuration.GetSection("Amazon")["AWSAccessKey"];
        public static string AWSSecretKey => _configuration.GetSection("Amazon")["AWSSecretKey"];
        public static string ContractReportURLc => _configuration.GetSection("Amazon")["ContractReportURLc"];
        public static string DisclaimerReportURL => _configuration.GetSection("Amazon")["DisclaimerReportURL"];
        public static bool CanSaveBucketToDateFolder => Convert.ToBoolean(_configuration.GetSection("Amazon")["CanSaveBucketToDateFolder"]);
    }
    public partial class ASH
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string ASH_URL => _configuration.GetSection("ASH")["ASH_URL"];
        public static string ASH_UserName => _configuration.GetSection("ASH")["ASH_UserName"];
        public static string ASH_Password => _configuration.GetSection("ASH")["ASH_Password"];
        public static string ASH_PIN => _configuration.GetSection("ASH")["ASH_PIN"];
    }

    public partial class MemberCheckOutSettings
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string CanSaveMemberSignUpABC => _configuration.GetSection("MemberCheckOutSettings")["CanSaveMemberSignUpABC"];
        public static string CanSavePTInfoInABC => _configuration.GetSection("MemberCheckOutSettings")["CanSavePTInfoInABC"];
        public static string CanSaveSGTInfoInABC => _configuration.GetSection("MemberCheckOutSettings")["CanSaveSGTInfoInABC"];
        public static string InterestedInMembershipEntrySource => _configuration.GetSection("MemberCheckOutSettings")["InterestedInMembershipEntrySource"];
        public static string GuestCheckInEntrySource => _configuration.GetSection("MemberCheckOutSettings")["GuestCheckInEntrySource"];
        public static string ClassPassEntrySource => _configuration.GetSection("MemberCheckOutSettings")["ClassPassEntrySource"];
        public static string GuestForTheDayEntrySource => _configuration.GetSection("MemberCheckOutSettings")["GuestForTheDayEntrySource"];
        public static string ABCMemberSignUpPostAgreementURL => _configuration.GetSection("MemberCheckOutSettings")["ABCMemberSignUpPostAgreementURL"];
        public static string Avoid_ProcessFee_for_PlanName => _configuration.GetSection("MemberCheckOutSettings")["Avoid_ProcessFee_for_PlanName"];
        public static string IsSchedules_Processfee_Applicable => _configuration.GetSection("MemberCheckOutSettings")["IsSchedules_Processfee_Applicable"];
        public static string schedules_Processfee => _configuration.GetSection("MemberCheckOutSettings")["schedules_Processfee"];
        public static string CampaignId => _configuration.GetSection("MemberCheckOutSettings")["CampaignId"];
        public static string ABCGetClubAccountPTPaymentMethod => _configuration.GetSection("MemberCheckOutSettings")["ABCGetClubAccountPTPaymentMethod"];
        public static string ABCMemberRecurringPOSTServiceURL => _configuration.GetSection("MemberCheckOutSettings")["ABCMemberRecurringPOSTServiceURL"];
        public static string CommissionsEmployeeID => _configuration.GetSection("MemberCheckOutSettings")["CommissionsEmployeeID"];
        public static string ServiceEmployeeID => _configuration.GetSection("MemberCheckOutSettings")["ServiceEmployeeID"];
        public static string ABCGetMemberInformationURL => _configuration.GetSection("MemberCheckOutSettings")["ABCGetMemberInformationURL"];
        public static string ABCMemberAddCardOnFile => _configuration.GetSection("MemberCheckOutSettings")["ABCMemberAddCardOnFile"];
        public static string MemberShipAgreementType => _configuration.GetSection("MemberCheckOutSettings")["MemberShipAgreementType"];
        public static string PTAgreementType => _configuration.GetSection("MemberCheckOutSettings")["PTAgreementType"];
        public static string SGTAgreementType => _configuration.GetSection("MemberCheckOutSettings")["SGTAgreementType"];
        public static string YearlyMembershipPlan => _configuration.GetSection("MemberCheckOutSettings")["YearlyMembershipPlan"];
        public static string BiweeklyMembershipPlan => _configuration.GetSection("MemberCheckOutSettings")["BiweeklyMembershipPlan"];
        public static string[] MembershipBasedOnClub => _configuration.GetSection("MemberCheckOutSettings")["MembershipBasedOnClub"].ToLower().Split(',');
        public static string[] SetBiweeklyState => _configuration.GetSection("MemberCheckOutSettings")["SGTAgreementType"].ToLower().Split(',');
        public static string BiweeklyTermSSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["BiweeklyTermSSRSReportURL"];
        public static string BiweeklyMTMSSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["BiweeklyMTMSSRSReportURL"];
        public static string NEW_BiweeklyTermURL => _configuration.GetSection("MemberCheckOutSettings")["NEW_BiweeklyTermURL"];
        public static string NEW_BiweeklyMTMURL => _configuration.GetSection("MemberCheckOutSettings")["NEW_BiweeklyMTMURL"];
        public static string NEW_PIFMemberShipURL => _configuration.GetSection("MemberCheckOutSettings")["NEW_PIFMemberShipURL"];
        public static string NEW_YearlyMemberShipURL => _configuration.GetSection("MemberCheckOutSettings")["NEW_YearlyMemberShipURL"];
        public static string NEW_MemberShipURL => _configuration.GetSection("MemberCheckOutSettings")["NEW_MemberShipURL"];
        public static string MTMOldStates => _configuration.GetSection("MemberCheckOutSettings")["MTMOldStates"];
        public static string TermOldStates => _configuration.GetSection("MemberCheckOutSettings")["TermOldStates"];
        public static string YearlyMemberShipSSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["YearlyMemberShipSSRSReportURL"];
        public static string MemberShipSSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["MemberShipSSRSReportURL"];
        public static string PTSSRSUserName => _configuration.GetSection("MemberCheckOutSettings")["PTSSRSUserName"];
        public static string PTSSRSPassword => _configuration.GetSection("MemberCheckOutSettings")["PTSSRSPassword"];
        public static string ReportSourceKey => _configuration.GetSection("MemberCheckOutSettings")["ReportSourceKey"];
        public static string SSRSTermReportURL_old => _configuration.GetSection("MemberCheckOutSettings")["SSRSTermReportURL_old"];
        public static string SSRSTermReportURL => _configuration.GetSection("MemberCheckOutSettings")["SSRSTermReportURL"];
        public static string SmallGroupTrainingSSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["SmallGroupTrainingSSRSReportURL"];
        public static string SSRSReportURL_old => _configuration.GetSection("MemberCheckOutSettings")["SSRSReportURL_old"];
        public static string SSRSPIFReportURL => _configuration.GetSection("MemberCheckOutSettings")["SSRSPIFReportURL"];
        public static string SSRSReportURL => _configuration.GetSection("MemberCheckOutSettings")["SSRSReportURL"];
        public static string InterestedInMembershipEnrtySource => _configuration.GetSection("MemberCheckOutSettings")["InterestedInMembershipEnrtySource"];
        public static string GuestCheckInEnrtySource => _configuration.GetSection("MemberCheckOutSettings")["GuestCheckInEnrtySource"];
        public static string ClassPassEnrtySource => _configuration.GetSection("MemberCheckOutSettings")["ClassPassEnrtySource"];
        public static string GuestForTheDayEnrtySource => _configuration.GetSection("MemberCheckOutSettings")["GuestForTheDayEnrtySource"];
        public static string MemberPersonalTraining => _configuration.GetSection("MemberCheckOutSettings")["MemberPersonalTraining"];
        public static string CreditCardBinApiKey => _configuration.GetSection("MemberCheckOutSettings")["CreditCardBinApiKey"];
        public static string SaveMemberInformationLocalDBMessage => _configuration.GetSection("LogMessage")["SaveMemberInformationLocalDBMessage"];
        public static string ABCAgreementResponsecount => _configuration.GetSection("LogMessage")["ABCAgreementResponsecount"];
        public static string SavePTMemberInformationLocalDBMessage => _configuration.GetSection("LogMessage")["SavePTMemberInformationLocalDBMessage"];
        public static string ABCAgreementABCResponseMessage => _configuration.GetSection("LogMessage")["ABCAgreementABCResponseMessage"];
        public static string SaveSGTMemberInformationLocalDBMessage => _configuration.GetSection("LogMessage")["SaveSGTMemberInformationLocalDBMessage"];
        public static string ABCErrorMessage => _configuration.GetSection("MemberCheckOutSettings")["ABCErrorMessage"];
        public static string SiteName => _configuration.GetSection("MemberCheckOutSettings")["SiteName"];
    }

    public partial class PlaidSettings
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string Environment => _configuration.GetSection("Plaid")["Environment"];
        public static string ClientId => _configuration.GetSection("Plaid")["ClientId"];
        public static string ClientSecret => _configuration.GetSection("Plaid")["ClientSecret"];
        public static string DevClientSecret => _configuration.GetSection("Plaid")["DevClientSecret"];
        public static string ProClientSecret => _configuration.GetSection("Plaid")["ProClientSecret"];
        public static string LinkTokenURL => _configuration.GetSection("Plaid")["LinkTokenURL"];
        public static string DevLinkTokenURL => _configuration.GetSection("Plaid")["DevLinkTokenURL"];
        public static string ProLinkTokenURL => _configuration.GetSection("Plaid")["ProLinkTokenURL"];
        public static string PublicTokenURL => _configuration.GetSection("Plaid")["PublicTokenURL"];
        public static string DevPublicTokenURL => _configuration.GetSection("Plaid")["DevPublicTokenURL"];
        public static string ProPublicTokenURL => _configuration.GetSection("Plaid")["ProPublicTokenURL"];
        public static string AccessTokenURL => _configuration.GetSection("Plaid")["AccessTokenURL"];
        public static string DevAccessTokenURL => _configuration.GetSection("Plaid")["DevAccessTokenURL"];
        public static string ProAccessTokenURL => _configuration.GetSection("Plaid")["ProAccessTokenURL"];
        public static string AccountDetURL => _configuration.GetSection("Plaid")["AccountDetURL"];
        public static string DevAccountDetURL => _configuration.GetSection("Plaid")["DevAccountDetURL"];
        public static string ProAccountDetURL => _configuration.GetSection("Plaid")["ProAccountDetURL"];
    }
   
    public partial class ADFSSettings
    {
        private static IConfiguration _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string AD_Path => _configuration.GetSection("ADFSSettings")["AD_Path"];
    }

}