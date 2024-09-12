using Kiosk.Business.Model.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.ABCResponseModels
{
    public class ABCMemberSignUpModel
    {
    }
    public class ABCPTPlansResponseModel
    {
        public List<RecurringServicePlan> AllPTPlanList { get; set; }
        public List<RecurringServicePlan> PTPlanTabOrderList { get; set; }
        public string FlashSalePlanList { get; set; }
        public bool IsPTMember { get; set; }
        public List<string> PTTabsList { get; set; }
        public List<PTPlanTabOrderAndName> PTPlanTabOrderAndName { get; set; }
        public bool IsSmallGroupPlan { get; set; }
    }
    public class RecurringServicePlan : Plan
    {
        public string validationHash { get; set; }
        public Billing billing { get; set; }
        public List<AdditionalCatalogItems> additionalCatalogItems { get; set; }
        public PurchaseToday purchaseToday { get; set; }
        public int totalValue { get; set; }
        public string totalInvoicePricisionValue { get; set; }
        public string totalInvoiceScaleValue { get; set; }
        public string unitPrice { get; set; }
        public string ProcessingFee { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public bool ViewPlanDetails { get; set; }
        public int RowNo { get; set; }
        public string PlanDetailsJson { get; set; }
    }
    public class AdditionalCatalogItems
    {
        public string additionalCatalogItemId { get; set; }
        public string additionalCatalogItemName { get; set; }
        public string additionalCatalogItemAmount { get; set; }
    }

    public class Billing
    {
        public string unitPrice { get; set; }
        public string unitPricisionValue { get; set; }
        public string unitScaleValue { get; set; }
        public string invoiceTotal { get; set; }
        public string totalInvoicePricisionValue { get; set; }
        public string totalInvoiceScaleValue { get; set; }
    }

    public class PurchaseToday
    {
        public string unitPrice { get; set; }
        public string serviceQuantity { get; set; }
        public string totalInvoice { get; set; }
        public string unitPricisionValue { get; set; }
        public string unitScaleValue { get; set; }
        public string totalInvoicePricisionValue { get; set; }
        public string totalInvoiceScaleValue { get; set; }
        public string catalogItemsTotal { get; set; }
        public string totalServiceQuantity { get; set; }
    }
    public class Plan
    {
        public string recurringServicePlanId { get; set; }
        public string recurringServicePlanName { get; set; }
        public string originalRecurringServicePlanName { get; set; }
        public string PlainRecurringServicePlanName { get; set; }
    }
    
    public class PTPlanInitialModel
    {
        public List<PTPlanTabOrder> PTPlanTabOrder { get; set; }
        public List<PTPlanTabOrder> SmallGroupPlanTabOrder { get; set; }
        public List<PTPlanTabOrderAndName> PTPlanTabOrderAndName { get; set; }
        public List<PTPlanTabOrderAndName> SmallGroupPlanTabOrderAndName { get; set; }
        public List<RecurringServicePlan> AllPTPlans { get; set; }
    }
    public class PTPlanMemberType
    {
        public string MembershipType { get; set; }
    }
    public class PTPlanTabOrder
    {
        public string DataTrakPlanName { get; set; }
        public string MarketingPlanName { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public int OrderNumber { get; set; }
        public int RowNo { get; set; }
        public bool ViewPlanDetails { get; set; }
    }
    public class PTPlanTabOrderAndName
    {
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public Int64 RowNo { get; set; }
        public bool ViewPlanDetails { get; set; }
    }
    public class ABCCredentialModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class ABCClubPlanResponseExceptionModel
    {
        public List<RecurringServicePlan> Response { get; set; }
        public bool? StopRequest { get; set; }
        public bool? Reattempt { get; set; }
    }

    public class ABCClubPlansResponseModel
    {
        public List<RecurringServicePlan> recurringServicePlans { get; set; }
    }
    public class ABCClubPlansDetailsResponseModel
    {
        public RecurringServicePlan recurringServicePlanDetail { get; set; }
    }

   

}
