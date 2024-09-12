using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Staff
{
    public class StaffModel
    {
        public DateTime? TimeStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string GuestType { get; set; }
        public string Note { get; set; }
        public string ABCCheckIn { get; set; }
        public string VisitorType { get; set; }
        public string CheckInStatus { get; set; }
        public string CheckInMessage { get; set; }
        public bool HasAppointment { get; set; }
        public Nullable<DateTime> AppointmentStart { get; set; }
        public Nullable<DateTime> AppointmentEnd { get; set; }
        public string AppointmentSubject { get; set; }
        public bool AppointmentShown { get; set; }
        public int LeadId { get; set; }
        public int DataTrakCheckinId { get; set; }
        public string SourceName { get; set; }
        public string SFId { get; set; }
        public string ContractUrl { get; set; }
        public string MemberId { get; set; }
        public string ReferralMemberId { get; set; }
        public bool IsCheckOut { get; set; }
        public DateTime? CheckOutTimeStamp { get; set; }
        public string GymTime { get; set; }
        public string TotalMinutesGymTime { get; set; }
        public string DisclaimerUrl { get; set; }
        public string MemberType { get; set; }
        public int? SeprationLine { get; set; }
        public string Color { get; set; }
        public string HealthInsurance { get; set; }
        public string PlanName { get; set; }
        public string SalesPersonEmployeeNumber { get; set; }
        public string SalesPersonName { get; set; }
        public bool IsRFC { get; set; }
        public string BorderColor { get; set; }
        public bool IsRFCViewed { get; set; }
        public string EquipmentName { get; set; }
        public string TotSpotBabysitting { get; set; }
        public bool IsSurvey { get; set; }
        public string TabName { get; set; }
    }

    public class StaffSearchModel
    {
        public int ClubNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class validateStaffCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Supervisor { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public System.Guid Guid { get; set; }
        public Nullable<int> OrganizationalUnitId { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string DefaultPassword { get; set; }
        public string EmployeeNumber { get; set; }
        public string Company { get; set; }
        public string DepartmentName { get; set; }
        public bool IsPasswordChangeRequired { get; set; }
        public bool IsSetAnswerOnNextLogin { get; set; }
        public Nullable<int> QuestionId1 { get; set; }
        public Nullable<int> QuestionId2 { get; set; }
        public Nullable<int> QuestionId3 { get; set; }
        public Nullable<bool> IsNewUser { get; set; }
        public Nullable<System.DateTime> NewUserCreatedOn { get; set; }
        public Nullable<bool> IsNewUserImportedToAD { get; set; }
        public Nullable<System.DateTime> NewUserImportedOnAD { get; set; }
        public string OUPath { get; set; }
        public bool IsAdmin { get; set; }
        public string SharedEmail { get; set; }
        public string LogonName { get; set; }
        public bool IsClubUser { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClubAdmin { get; set; }
        public Nullable<int> TotalNoOfReminder { get; set; }
        public bool UltimateSync { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public string Warning { get; set; }
        public bool IsLocked { get; set; }
        public Nullable<System.DateTime> LockedDate { get; set; }
        public string LockedBy { get; set; }
        public string EmployeeCode { get; set; }
        public bool EnableTwoFactorAuth { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> TwoFactorAuthDate { get; set; }
        public string TwoFactorEmail { get; set; }
        public bool SkpPasswordChange { get; set; }
        public bool IsAGMUser { get; set; }
    }
    public class StaffContactDetail
    {
        public string Email { get; set; }
    }
}
