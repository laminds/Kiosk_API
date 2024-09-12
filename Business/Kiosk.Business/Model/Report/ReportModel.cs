using Kiosk.Business.Model.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Report
{
    public class ReportModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool Rendered { get; set; }
    }

    public class ReportPlanModel
    {
        public string annualDues { get; set; }
        public string bannerText { get; set; }
        public string clubFeeTotalAmount { get; set; }
        public string downPaymentTotalAmount { get; set; }
        public string firstDueDate { get; set; }
        public string firstMonthDues_tax { get; set; }
        public string firstMonthDues { get; set; }
        public string initiationFee { get; set; }
        public string initiationFee_tax { get; set; }
        public string membershipType { get; set; }
        public string originalPlanTypeName { get; set; }
        public string paidToday { get; set; }
        public string planFeesPricisionValue { get; set; }
        public string planFeesScaleValue { get; set; }
        public string planId { get; set; }
        public string planName { get; set; }
        public string planType { get; set; }
        public string planSubType { get; set; }
        public string planTypeDetail { get; set; }
        public string planValidation { get; set; }
        public string prepaidDues { get; set; }
        public string prorated_subTotal { get; set; }
        public string prorated_tax { get; set; }
        public string prorated_total { get; set; }
        public string salestax { get; set; }
        public string schedulePreTaxAmount { get; set; }
        public string scheduleTotalAmount { get; set; }
        public string strikeout_field { get; set; }
        public string totalAmount { get; set; }
        public string totalDue { get; set; }
        public DateTime? expirationDate { get; set; }
    }

    public class ReportPTPlanModel
    {
        public string monthlyRecurringCharge { get; set; }
        public string planName { get; set; }
        public string originalPlanName { get; set; }
        public string planPrice { get; set; }
        public string ptPlanNameType { get; set; }
        public string ptPlanId { get; set; }
        public string totalPrice { get; set; }
        public string validationHash { get; set; }
        public string serviceQuantity { get; set; }
        public string ProcessingFee { get; set; }
        public string PTPlanType { get; set; }
    }

    public class InitialReportAgreementModel
    {
        public PersonalInformationModel PersonalInformation { get; set; }
        public BillingAddressModel BillingInfo { get; set; }
        public ReportPlanModel PlanInitialInformation { get; set; }
        public PaymentInformationModel PTpaymentInfo { get; set; }
        public PaymentInformationModel PaymentInfo { get; set; }
        public ReportPTPlanModel PTPlanInformation { get; set; }
        public BankingDetailObj BankingDetailObj { get; set; }
        public PTBankingDetailObj PTbankingDetailObj { get; set; }
        public string signatureBody { get; set; }
        public string initialSignatureBody { get; set; }
        public bool isKiosk { get; set; }
        public string agreementType { get; set; }
        public string clubNumber { get; set; }
        public string SourceId { get; set; }
        public string sourceName { get; set; }
        public string EntrySource { get; set; }
    }
}
