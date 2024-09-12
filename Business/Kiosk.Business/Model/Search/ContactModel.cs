using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Search
{
    public class ContactModel
    {
        public string ClubNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HSId { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string MemberId { get; set; }
        public Nullable<DateTime> ExpiredDate { get; set; }
        public Nullable<DateTime> BeginDate { get; set; }
        public Nullable<DateTime> Downloaddate { get; set; }
        public Nullable<DateTime> GuestPassFirstUseDate { get; set; }
        public int SourceId { get; set; }
        public string PassDurationDay { get; set; }
        public string EntrySource { get; set; }
        public string NewEntrySource { get; set; }
        public bool Auto_Dialer_Opt_In { get; set; }
        public DateTime? Auto_Dialer_Opt_In_Date_Time__c { get; set; }
        public bool Text_Opt_In { get; set; }
        public DateTime? Text_Opt_In_Date_Time__c { get; set; }
        public bool Phone_Call_Opt_In { get; set; }
        public DateTime? Phone_Call_Opt_In_Date_Time__c { get; set; }
        public bool? Auto_Dialer_Opt_Out { get; set; }
        public bool? Text_Opt_Out { get; set; }
        public DateTime? Email_Opt_In_Date_Time__c { get; set; }
        public string CreatedBy { get; set; }
        public string ClubStationId { get; set; }
        public string HearAbout { get; set; }
        public bool IsNewLead { get; set; }
        public string GuestType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string DriverLicenseEncode { get; set; }
        public bool IsRFC { get; set; }
        public bool IsKeepMeUpdate { get; set; }
        public bool IsGuestModule { get; set; }
        public string HS_Status { get; set; }
        public string Action { get; set; }
        public bool HS_IsUpdated { get; set; }
        public SalesPersonModel salesPersonObj { get; set; }
        public string MemberStatus { get; set; }
        public string referringMemberId { get; set; }
        public string referringMemberFirst { get; set; }
        public string referringMemberLast { get; set; }
        public bool? IsActive { get; set; }
        public bool? isOpenHouse { get; set; }
        public bool? isAppointmentTour { get; set; }
        public bool? isFreePass { get; set; }
        public bool? isGuestPaidPass { get; set; }
        public DateTime? guestPaidPass_Date { get; set; }
        public int? EquipmentId { get; set; }
        public string SourceName { get; set; }
        public Nullable<DateTime> LastCheckInDate { get; set; }
        public Nullable<DateTime> dgr_recent_sign_date { get; set; }

        public string MemberType { get; set; }
        public string LeadId { get; set; }
    }

    public class SalesEmployeeDetail
    {
        public int employee_id { get; set; }
        public string paychex_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string FullName { get; set; }
        public string employee_phone { get; set; }
        public string employee_email { get; set; }
        public string employee_status { get; set; }
        public string EpFullName { get; set; }
        public string Branch_Code { get; set; }
        public Nullable<int> clubNumber { get; set; }
        public string SPEmployeeId { get; set; }
        public int SalesPersonMissing { get; set; }
        public string BarCode { get; set; }
    }

    public class usp_GetMemberDetails_Result
    {
        public int ClubNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AgreementNumber { get; set; }
        public string Barcode { get; set; }
        public string MemberId { get; set; }
        public string ReferralMemberId { get; set; }
        public string GuestType { get; set; }
        public string MemberStatus { get; set; }
        public string MembershipType { get; set; }
        public string MemberCheckInStatus { get; set; }
        public string SFId { get; set; }
        public string HomeClubNumber { get; set; }
        public int? RenewMemberId { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PTMemberId { get; set; }
        public bool IsFlipPlan { get; set; }
    }

    public class SearchMemberAndProspectModel
    {
        public List<usp_GetMemberDetails_Result> MemberList { get; set; }
        public List<ContactModel> ProspectList { get; set; }
    }

    public class MemberInitialInfoModel {
        public int clubNumber { get; set; }
        public string clubName { get; set; }
        public string clubStationId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string guestType { get; set; }
        public string email { get; set; }
        public string AgreementNumber { get; set; }
        public string homeClubNumber { get; set; }
        public DateTime? dOB { get; set; }
        public string isFlipPlan { get; set; }
        public string memberId { get; set; }
        public string memberStatus { get; set; }
        public string memberType { get; set; }
        public string phoneNumber { get; set; }
        public SalesPersonModel salesPersonObj { get; set; }
        public string action { get; set; }
        public string hSId { get; set; }
        public string hS_Status { get; set; }
        public bool isGuestModule { get; set; }

    }
    public class SalesPersonModel
    {
        public int employee_id { get; set; }
        public string paychex_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string FullName { get; set; }
        public string employee_phone { get; set; }
        public string employee_email { get; set; }
        public string employee_status { get; set; }
        public string EpFullName { get; set; }
        public string Branch_Code { get; set; }
        public Nullable<int> clubNumber { get; set; }
        public string SPEmployeeId { get; set; }
        public int SalesPersonMissing { get; set; }
        public string BarCode { get; set; }
    }
    public class SearchLeadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string clubNumber { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }

    }
    public class ChildCarePlanDetails : AddOnAmenities
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string MemberId { get; set; }
        public int ClubNumber { get; set; }
        public string MemberType { get; set; }
        public int SourceId { get; set; }
        public string GuestType { get; set; }
        public string SourceName { get; set; }
        public SalesPersonModel salesPersonObj { get; set; }
    }
    public class AddOnAmenities
    {
        public string PlanName { get; set; }
        public Nullable<decimal> PlanPrice { get; set; }
    }
    public class UpgradeMembershipModel
    {
        public string PlanName { get; set; }
        public string MemberId { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string CampaignCode { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
    public class ABCMissingProspectsModel
    {
        public string HSId { get; set; }
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string ClubNumber { get; set; }
    }

    public class memberGuest
    {
        public string MemberId { get; set; }
        public string Barcode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class GuestLastCheckIn
    {
        public string memberId { get; set; }
        public DateTime? lastCheckInTimestamp { get; set; }
    }
}