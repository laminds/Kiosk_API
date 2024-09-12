using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Checkout
{
    public class MemberAgreementModel
    {
        public ABCAgreementContactInfoModel AgreementContactInfoModel { get; set; }
        public ABCTodayBillingInfoModel TodayBillingInfoModel { get; set; }
        public ABCDraftCreditCardModel DraftCreditCardModel { get; set; }
        public ABCDraftBankAccountModel DraftBankAccountModel { get; set; }
    }
    public class ABCAgreementRequestModel
    {
        public string paymentPlanId { get; set; }
        public string planValidationHash { get; set; }
        public bool activePresale { get; set; }
        public bool sendAgreementEmail { get; set; }
        public string salesPersonId { get; set; }
        public string campaignId { get; set; }
        public ABCAgreementContactInfoModel agreementContactInfo { get; set; }
    }
    public class ABCAgreementRequestModel<T, D>
    {
        public string paymentPlanId { get; set; }
        public string planValidationHash { get; set; }
        public bool activePresale { get; set; }
        public bool sendAgreementEmail { get; set; }
        public string salesPersonId { get; set; }
        public string campaignId { get; set; }
        public ABCAgreementContactInfoModel agreementContactInfo { get; set; }
        public T todayBillingInfo { get; set; }
        public D draftBillingInfo { get; set; }
        public string[] schedules { get; set; }
    }
    public class ABCAgreementContactInfoModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string homePhone { get; set; }
        public string cellPhone { get; set; }
        public string birthday { get; set; }
        public string employer { get; set; }
        public int ClubNumber { get; set; }
        public string PlanName { get; set; }
        public string PTPlanName { get; set; }
        public string PersonalInformation { get; set; }
        public ABCAgreementAddressInfoModel agreementAddressInfo { get; set; }
        public ABCAgreementEmergencyContactModel emergencyContact { get; set; }
    }
    public class ABCAgreementAddressInfoModel
    {
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
    }
    public class ABCAgreementEmergencyContactModel
    {
        public string ecFirstName { get; set; }
        public string ecLastName { get; set; }
        public string ecPhone { get; set; }
    }
    public class ABCTodayBillingInfoModel
    {
        public bool isTodayBillingSameAsDraft { get; set; }
        public string todayCcFirstName { get; set; }
        public string todayCcLastName { get; set; }
        public string todayCcType { get; set; }
        public string todayCcAccountNumber { get; set; }
        public string todayCcExpMonth { get; set; }
        public string todayCcExpYear { get; set; }
        public string todayCcCvvCode { get; set; }
        public string todayCcBillingZip { get; set; }
    }

    public class ABCDraftBillingInfoModel
    {
        public ABCDraftCreditCardModel draftCreditCard { get; set; }
        public ABCDraftBankAccountModel draftBankAccount { get; set; }
    }
    public class ABCDraftCreditCardModel
    {
        public string creditCardFirstName { get; set; }
        public string creditCardLastName { get; set; }
        public string creditCardType { get; set; }
        public string creditCardAccountNumber { get; set; }
        public string creditCardExpMonth { get; set; }
        public string creditCardExpYear { get; set; }
    }
    public class ABCDraftBankAccountModel
    {
        public string draftAccountFirstName { get; set; }
        public string draftAccountLastName { get; set; }
        public string draftAccountRoutingNumber { get; set; }
        public string draftAccountNumber { get; set; }
        public string draftAccountType { get; set; }
    }

}
