using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Plans
{
    public class ABCMemberPlanInitialModel
    {
        public ABCMemberPlanResponseModel ABCPlans { get; set; }
        public bool? StopRequest { get; set; }
        public bool? Reattempt { get; set; }
    }
    public class ABCMemberPlanResponseModel
    {
        public List<memberSignUpPlanModel> plans { get; set; }
        public List<memberSignUpPlanModel> InitialPlanList { get; set; }
        public List<memberShipTabOrderDetail> PlanOrderTab { get; set; }
    }

    public class memberSignUpPlanModel
    {
        public string planName { get; set; }
        public string marketingPlanName { get; set; }
        public string planId { get; set; }
        public string agreementDescription { get; set; }
        public string agreementTerms { get; set; }
        public string agreementNote { get; set; }
        public string planValidation { get; set; }
        public string clubFeeTotalAmount { get; set; }
        public string initiationFee { get; set; }
        public string FirstMonthDues_tax { get; set; }
        public string InitiationFee_tax { get; set; }
        public string scheduleTotalAmount { get; set; }
        public string firstMonthDues { get; set; }
        public string downPaymentTotalAmount { get; set; }
        public int planFeesPricisionValue { get; set; }
        public string planFeesScaleValue { get; set; }
        public string enrollFeePricisionValue { get; set; }
        public List<PlanFeatureModel> features { get; set; }
        public bool? activePresale { get; set; }
        public string promoCode { get; set; }
        public string promoName { get; set; }
        public string annualFeeDueDays { get; set; }
        public string membershipType { get; set; }
        public string Strikeout_field { get; set; }
        public string Salestax { get; set; }
        public string AnnualDues { get; set; }
        public string PrepaidDues { get; set; }
        public string TotalDue { get; set; }
        public string PaidToday { get; set; }
        public string TotalAmount { get; set; }
        public string schedulePreTaxAmount { get; set; }
        public DateTime? firstDueDate { get; set; }
        public DateTime? expirationDate { get; set; }

        public string PlanTypeDetail { get; set; }
        public string PlanSubType { get; set; }
        public string Prorated_subTotal { get; set; }
        public string Prorated_tax { get; set; }
        public string Prorated_total { get; set; }

        public string TabName { get; set; }
        public string OriginalPlanTypeName { get; set; }
        public string BannerText { get; set; }
        public int TabOrder { get; set; }
        public int OrderNumber { get; set; }
        public string clubFeeName { get; set; }
        public string PlanDetailsJson { get; set; }
    }

    public class PlanFeatureModel
    {
        public string feature { get; set; }
        public bool isSelected { get; set; }

    }
    public class memberShipTabOrderDetail
    {
        public string TabName { get; set; }
        public string OriginalPlanTypeName { get; set; }
        public int TabOrder { get; set; }
        public Int64 RowNo { get; set; }
    }

    public class ABCMemberPlanDetailModel
    {
        public memberSignUpPlanDetailModel paymentPlan { get; set; }
    
    }

    public class memberSignUpPlanDetailModel : memberSignUpPlanModel
    {
        public string scheduleFrequency { get; set; }
        public DateTime? firstDueDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public List<memberSignUpPlanDetailDownPaymentsModel> downPayments { get; set; }
        public List<memberSignUpPlanDetailschedulesModel> schedules { get; set; }
        public List<memberSignUpPlanDetailclubFeesModel> clubFees { get; set; }
    }

    public class memberSignUpPlanDetailDownPaymentsModel
    {
        public string name { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public string tax { get; set; }

    }
    public class memberSignUpPlanDetailschedulesModel
    {
        public string profitCenter { get; set; }
        public string schedulePreTaxAmount { get; set; }
        public string scheduleAmount { get; set; }
    }
    public class memberSignUpPlanDetailclubFeesModel
    {
        public string feeName { get; set; }
        public string feeAmount { get; set; }
    }
}
