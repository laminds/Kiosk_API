using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Domain.Models.SPModel
{
    public class usp_GetLocationsByEmail_Result
    {
        public int location_code { get; set; }
        public string club_name { get; set; }
        public long? promo_location_id { get; set; }
        public string club_city { get; set; }
        public string s_clubName { get; set; }
    }

    public class usp_GetClubStateBYClubNumber_Result
    {
        public string State { get; set; }
    }

    public class ClubNumberModel
    {
        public int ClubNumber { get; set; }
        public string ClubName { get; set; }
    }

    public class ClubByUserModel
    {
        public string club_name { get; set; }
        public string ClubFullName { get; set; }
        public long? promo_location_id { get; set; }
    }

    public class ClubUserModel
    {
        public int Code { get; set; }
        public string ClubFullName { get; set; } = string.Empty;
        public string club_name { get; set; }
        public long? promo_location_id { get; set; }
    }

    public class GetABCPaymentPlansResultModels
    {
        public int ABCPaymentPlansId { get; set; }
        public int clubNumber { get; set; }
        public string planId { get; set; }
        public string planName { get; set; }
        public string agreementDescription { get; set; }
        public Nullable<bool> limitedAvailability { get; set; }
        public string onlinePlanDisplayLocation { get; set; }
        public Nullable<bool> corporatePlanOnly { get; set; }
        public string membershipType { get; set; }
        public string agreementTerm { get; set; }
        public string scheduleFrequency { get; set; }
        public Nullable<int> termInMonths { get; set; }
        public string dueDay { get; set; }
        public Nullable<System.DateTime> firstDueDate { get; set; }
        public Nullable<System.DateTime> expirationDate { get; set; }
        public Nullable<bool> activePresale { get; set; }
        public string onlineSignupAllowedPaymentMethods { get; set; }
        public string preferredPaymentMethod { get; set; }
        public Nullable<decimal> totalContractValue { get; set; }
        public string downPaymentName { get; set; }
        public Nullable<decimal> downPaymentTotalAmount { get; set; }
        public Nullable<decimal> InitiationFee_subTotal { get; set; }
        public Nullable<decimal> InitiationFee_tax { get; set; }
        public Nullable<decimal> InitiationFee_total { get; set; }
        public Nullable<decimal> FirstMonthDues_subTotal { get; set; }
        public Nullable<decimal> FirstMonthDues_tax { get; set; }

        public Nullable<decimal> LastMonthDues_subTotal { get; set; }
        public Nullable<decimal> LastMonthDues_tax { get; set; }
        public Nullable<decimal> LastMonthDues_total { get; set; }
        public Nullable<decimal> scheduleTotalAmount { get; set; }
        public string agreementTerms { get; set; }
        public string agreementNote { get; set; }
        public string clubFeeName { get; set; }
        public Nullable<decimal> clubFeeTotalAmount { get; set; }
        public Nullable<decimal> schedulePreTaxAmount { get; set; }
        public Nullable<decimal> PrePaidDues { get; set; }
        public Nullable<decimal> PrePaidDues_total { get; set; }
        public Nullable<decimal> PrePaidDues_subTotal { get; set; }
        public Nullable<decimal> PrePaidDues_tax { get; set; }
        public Nullable<decimal> InitiationFee { get; set; }
        public Nullable<decimal> FirstMonthDues { get; set; }
        public string PlanDetailsJson { get; set; }
        public string planValidation { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        public string promoCode { get; set; }
        public string promoName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> Prorated_subTotal { get; set; }
        public Nullable<decimal> Prorated_tax { get; set; }
        public Nullable<decimal> Prorated_total { get; set; }
    }
}
