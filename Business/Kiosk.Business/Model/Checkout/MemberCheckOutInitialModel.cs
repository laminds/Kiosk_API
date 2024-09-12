using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Checkout
{
    public class MemberCheckOutInitialModel
    {
        public InitialCheckoutPlanModel PlanInitialInformation { get; set; }
        public EmailResponseObjModel EmailResponseObj { get; set; }
        public PersonalInformationModel PersonalInformation { get; set; }
        public BillingAddressModel BillingInfo { get; set; }
        public PaymentInformationModel PaymentInformation { get; set; }
        public PaymentInformationModel SecondarypaymentInformation { get; set; }
        public PaymentInformationModel PTPaymentInformation { get; set; }
        public PaymentInformationModel SecondaryPTPaymentInformation { get; set; }
        public BankingDetailObj BankingDetailObj { get; set; }
        public PTBankingDetailObj PTBankingDetailObj { get; set; }
        public IsPrepaidCCard IsPrepaidCCard { get; set; }
        public IsPTPrepaidCCard IsPTPrepaidCCard { get; set; }

        //public PTPaymentObjModel PTPaymentObjInfo { get; set; }
    }

    public class EmailResponseObjModel
    {
        public string EMAIL { get; set; }
        public string FINDING { get; set; }
        public string COMMENT { get; set; }
        public string COMMENT_CODE { get; set; }
        public string SUGG_EMAIL { get; set; }
        public string SUGG_COMMENT { get; set; }
        public string ERROR_RESPONSE { get; set; }
        public string ERROR { get; set; }
        public string UUID { get; set; }
        public bool IsValid { get; set; }
    }

    public class PersonalInformationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public EmployeeDetailModel SalesPersonObj { get; set; }
        public bool IsKeepMeUpdate { get; set; }
        public bool IsReceiveTextMessages { get; set; }
        public bool IsKeepMeUpdate_Old { get; set; }
        public bool IsReceiveTextMessages_Old { get; set; }
        public string MemberId { get; set; }
        public string AgreementNumber { get; set; }
        public string RecurringServiceId { get; set; }
        public string SGTRecurringServiceId { get; set; }
        public string HubSpotId { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyContact { get; set; }
        public string SalesPersonId { get; set; }
        public string Employer { get; set; }
        public int? LeadId { get; set; }
        public string membershiptypeplan { get; set; }
    }

    public class BillingAddressModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
    public class PaymentInformationModel
    {
        public string CreditCardFirstName { get; set; }
        public string CreditCardLastName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpMonth { get; set; }
        public string CreditCardExpYear { get; set; }
        public string CreditCardCVV { get; set; }
        public string CreditCardZipCode { get; set; }
        public string CreditCardType { get; set; }
        public string PaymentType { get; set; }
    }

    public class BankingDetailObj
    {
        public bool? IsUseBankingDetails { get; set; }
        public string DraftAccountFirstName { get; set; }
        public string DraftAccountLastName { get; set; }
        public string DraftAccountRoutingNumber { get; set; }
        public string DraftAccountNumber { get; set; }
        public string DraftAccountType { get; set; }
        public string PaymentType { get; set; }

    }

    public class PTBankingDetailObj
    {
        public bool? IsPTUseBankingDetails { get; set; }
        public string PTDraftAccountFirstName { get; set; }
        public string PTDraftAccountLastName { get; set; }
        public string PTDraftAccountRoutingNumber { get; set; }
        public string PTDraftAccountNumber { get; set; }
        public string PTDraftAccountType { get; set; }
        public string PTPaymentType { get; set; }

    }

    public class RecurringPaymentInfoModel
    {
        public string RecurringPaymentMethod { get; set; }
        public bool IsSameCard { get; set; }
        public PaymentInformationModel CreditCardInformation { get; set; }
        public BankAccountInformationModel BankAccountInformation { get; set; }
        public RecurringPaymentMethodObjModel RecurringPaymentMethodObj { get; set; }
    }
    public class BankAccountInformationModel
    {
        public string DraftAccountFirstName { get; set; }
        public string DraftAccountLastName { get; set; }
        public string DraftAccountType { get; set; }
        public string DraftAccountNumber { get; set; }
        public string DraftAccountRoutingNumber { get; set; }
    }
    public class RecurringPaymentMethodObjModel
    {
        public int id { get; set; }
        public string methodName { get; set; }
        public string paymentMethodType { get; set; }
    }
    public class PTPaymentObjModel
    {
        public PaymentInformationModel PaymentInformation { get; set; }
        public RecurringPaymentInfoModel RecurrPaymentInfo { get; set; }
    }
    public class IsPrepaidCCard
    {
        public bool IsPrepaidCreditCard { get; set; }
        public bool IsUserChangedPrepaidCard { get; set; }
    }
    public class IsPTPrepaidCCard
    {
        public bool IsPrepaidCreditCard { get; set; }
        public bool IsUserChangedPrepaidCard { get; set; }
    }

    public class AddOnAmenities
    {
        public string PlanName { get; set; }
        public Nullable<decimal> PlanPrice { get; set; }
    }
    public class EmployeeDetailModel
    {
        public int EmployeeId { get; set; }
        public string PaychexId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeStatus { get; set; }
        public string EpFullName { get; set; }
        public string EmpBranchCode { get; set; }
        public Nullable<int> SPClubNumber { get; set; }
        public string SPEmployeeId { get; set; }
        public int SalesPersonMissing { get; set; }
        public string BarCode { get; set; }
    }

}
