using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.ABC
{
    public class ABCAgreementResponseModel
    {
        public ABCStatusModel status { get; set; }
        public ABCAgreementResultModel result { get; set; }
        public int MemberlogId { get; set; }
    }
    public class ABCStatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
        public string messageCode { get; set; }
    }

    public class ABCAgreementResultModel
    {
        public string clubNumber { get; set; }
        public string agreementNumber { get; set; }
        public string memberId { get; set; }
    }
    public class ABCResponsePaymentPlan
    {
        public StatusViewModel status { get; set; }
        public Request request { get; set; }
        public PaymentPlan paymentPlan { get; set; }
        public string response { get; set; }
    }
    public class StatusViewModel
    {
        public string message { get; set; }
        public string count { get; set; }
        public string messageCode { get; set; }
    }
    public class Request
    {
        public string clubNumber { get; set; }
    }
    public class PaymentPlan
    {
        public string planName { get; set; }
        public string planId { get; set; }
        public string membershipType { get; set; }
        public string agreementTerm { get; set; }
        public string scheduleFrequency { get; set; }
        public int termInMonths { get; set; }
        public string dueDay { get; set; }
        public string firstDueDate { get; set; }
        public string expirationDate { get; set; }
        public bool activePresale { get; set; }
        public string onlineSignupAllowedPaymentMethods { get; set; }
        public string preferredPaymentMethod { get; set; }
        public decimal totalContractValue { get; set; }
        public string downPaymentName { get; set; }
        public decimal downPaymentTotalAmount { get; set; }
        public List<DownPayment> downPayments { get; set; }
        public decimal scheduleTotalAmount { get; set; }
        public List<Schedules> schedules { get; set; }
        public List<ClubFees> clubFees { get; set; }
        public string agreementTerms { get; set; }
        public string agreementNote { get; set; }
        public decimal clubFeeTotalAmount { get; set; }
        public string planValidation { get; set; }
        public bool active { get; set; }
    }
    public class DownPayment
    {
        public string name { get; set; }
        public decimal subTotal { get; set; }
        public decimal tax { get; set; }
        public decimal total { get; set; }
    }
    public class Schedules
    {
        public decimal schedulePreTaxAmount { get; set; }
        public string profitCenter { get; set; }
        public string scheduleDueDate { get; set; }
        public decimal scheduleAmount { get; set; }
        public string numberOfPayments { get; set; }
        public bool recurring { get; set; }
        public bool addon { get; set; }
        public bool defaultChecked { get; set; }
    }
    public class ClubFees
    {
        public string feeName { get; set; }
        public string feeDueDate { get; set; }

    }
}
