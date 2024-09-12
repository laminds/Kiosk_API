using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Plans
{
    public class MemberPlanModel
    {
        public int ClubNumber { get; set; }
        public string DataTrakPlans { get; set; }
        public string MarketingPlans { get; set; }
        public string Strikeout_field { get; set; }
        public string BannerText { get; set; }
        public string OriginalPlanTypeName { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public int OrderNumber { get; set; }
    }

    public class InitialPlanModel
    {
        public List<MemberPlanModel> PlanDetails { get; set; }
        public List<memberShipTabOrderDetail> MemberShipTabOrder { get; set; }
        public List<memberSignUpPlanModel> AllPlans { get; set; }

    }
    public class AmenitiesResponseModel
    {
        public int AmenitiesId { get; set; }
        public string AmenitiesName { get; set; }
        public string OrderNo { get; set; }
        public string PlanType { get; set; }
    }
}
