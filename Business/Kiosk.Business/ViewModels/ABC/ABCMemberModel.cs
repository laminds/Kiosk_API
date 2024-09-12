using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.ABC
{
    public class ABCMemberResponseExceptionModel
    {
        public ABCMemberModel Response { get; set; }
        public bool? StopRequest { get; set; }
        public bool? Reattempt { get; set; }
    }
    public class ABCMemberModel
    {
        public ABCMemberStatusModel status { get; set; }
        public ABCMemberRequestModel request { get; set; }
        public List<ABCMembersModel> members { get; set; }
    }
    public class ABCMemberStatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
    }

    public class ABCMemberRequestModel
    {
        public string clubNumber { get; set; }
        public string page { get; set; }
        public string size { get; set; }
        public string memberId { get; set; }
    }
    public class ABCMembersModel
    {
        public string memberId { get; set; }
        public ABCMemberPersonalModel personal;
        public ABCMemberAgreementModel agreement;
    }

    public class ABCMemberPersonalModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleInitial { get; set; }
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public string homeClub { get; set; }
        public string email { get; set; }
        public string primaryPhone { get; set; }
        public string mobilePhone { get; set; }
        public string workPhoneExt { get; set; }
        public string emergencyContactName { get; set; }
        public string emergencyPhone { get; set; }
        public string emergencyExt { get; set; }
        public string barcode { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string employer { get; set; }
        public string isActive { get; set; }
        public string memberStatus { get; set; }
        public string joinStatus { get; set; }
        public string isConvertedProspect { get; set; }
        public string hasPhoto { get; set; }
        public string memberStatusReason { get; set; }
        public string firstCheckInTimestamp { get; set; }
        public string memberStatusDate { get; set; }
        public string lastCheckInTimestamp { get; set; }
        public string totalCheckInCount { get; set; }
        public string createTimestamp { get; set; }
        public string lastModifiedTimestamp { get; set; }
        public string addressLine2 { get; set; }
        public string workPhone { get; set; }
        public string emergencyAvailability { get; set; }
        public string occupation { get; set; }
        public string @group { get; set; }
        public string convertedDate { get; set; }
        public string wellnessProgramId { get; set; }
        public string memberMisc1 { get; set; }
    }

    public class ABCMemberAgreementModel
    {
        public string agreementNumber { get; set; }
        public string isPrimaryMember { get; set; }
        public string isNonMember { get; set; }
        public string ordinal { get; set; }
        public string salesPersonId { get; set; }
        public string salesPersonName { get; set; }
        public string salesPersonHomeClub { get; set; }
        public string paymentPlan { get; set; }
        public string paymentPlanId { get; set; }
        public string term { get; set; }
        public string paymentFrequency { get; set; }
        public string membershipType { get; set; }
        public string membershipTypeAbcCode { get; set; } //New Field
        public string managedType { get; set; }
        public string campaignId { get; set; }
        public string campaignName { get; set; }
        public string isPastDue { get; set; }
        public string renewalType { get; set; }
        public string agreementPaymentMethod { get; set; }
        public string downPayment { get; set; }
        public string nextDueAmount { get; set; }
        public string projectedDueAmount { get; set; }
        public string pastDueBalance { get; set; }
        public string lateFeeAmount { get; set; }
        public string serviceFeeAmount { get; set; }
        public string totalPastDueBalance { get; set; }
        public string clubAccountPastDueBalance { get; set; }
        public string currentQueue { get; set; }
        public string queueTimestamp { get; set; }
        public string agreementEntrySource { get; set; }
        public string agreementEntrySourceReportName { get; set; }
        public string sinceDate { get; set; }
        public string beginDate { get; set; }
        public string signDate { get; set; }

        public string firstPaymentDate { get; set; }
        public string nextBillingDate { get; set; }
        //public string draftPurchaseName { get; set; }
        public string referringMemberId { get; set; }
        public string referringMemberHomeClub { get; set; }
        public string referringMemberName { get; set; }
        public string campaignGroup { get; set; }
        public string downPaymentPendingPOS { get; set; }
        public string stationLocation { get; set; }
        public string expirationDate { get; set; }
        public string convertedDate { get; set; }
        public string lastRenewalDate { get; set; }
        public string lastRewriteDate { get; set; }
        public string renewalDate { get; set; }
        //public primaryBillingAccountHolder primaryBillingAccountHolder { get; set; }

    }

}
