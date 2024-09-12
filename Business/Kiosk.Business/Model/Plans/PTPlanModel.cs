using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Plans
{
    internal class PTPlanModel
    {
    }
    public class PTPlanResponseModel
    {
        public string ClubNumber { get; set; }
        public bool? IsFlipPlan { get; set; }
        public string PlanType { get; set; }
        public string MemberId { get; set; }
        public bool IsSGTFlag { get; set; }
        public string Source { get; set; }
    }
}
