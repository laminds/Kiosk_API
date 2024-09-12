using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Checkout
{
    public class InitialCheckoutPlanModel
    {
        public string ClubNumber { get; set; }
        public string PlanId { get; set; }
        public string SourceName { get; set; }
        public string clubName { get; set; }
        public string PlanName { get; set; }
        public string PTPlanName { get; set; }
        public string PlanPrice { get; set; }
        public int SourceId { get; set; }
        public string PTPlanId { get; set; }
        public string PtPlanNameType { get; set; }
        public string SGTPlanNameType { get; set; }
        public string PTPlanType { get; set; }
        public string SmallGroupPlanId { get; set; }
        public string SmallGroupValidationHash { get; set; }
        public string PlanType { get; set; }
        public string PlanBiweeklyType { get; set; }
        public string PTValidationHash { get; set; }
        public string NewEntrySource { get; set; }
        public bool IsSameAsAbove { get; set; }
        public string RecurringServiceId { get; set; }
        public string SignatureBody { get; set; }
        public string RecurringPaymentMethod { get; set; }
        public string InitialSignatureBody { get; set; }
        public string AgreementNumber { get; set; }
        public string PlanTypeDetail { get; set; }
        public bool IsRecurringPaymentFlag { get; set; }
        public string MemberPlanDetailsJson { get; set; }
        public string PTPlanDetailsJson { get; set; }
        public string SGTPlanDetailsJson { get; set; }

        //public bool IsAgreementContractChecked { get; set; }
        //public bool IsAuthorizationTermsChecked { get; set; }
        //public bool IsMembershipAgreementContractChecked { get; set; }
        //public bool IsNotesofthisagreementChecked { get; set; }
        //public bool IsPTAgreementContractChecked { get; set; }
        //public bool isTermsConditionsChecked { get; set; }
        //public bool isThirtyDayNoticeChecked { get; set; }
    }
}
