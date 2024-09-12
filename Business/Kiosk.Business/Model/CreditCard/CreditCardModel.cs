using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.CreditCard
{
    public class CreditCardModel
    {
        public string bin { get; set; }
        public string brand { get; set; }
        public string bank { get; set; }
        public string type { get; set; }
        public string level { get; set; }
        public string country { get; set; }
        public string info { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string www { get; set; }
        public bool? prepaid { get; set; }
        public bool? reloadable { get; set; }
        public bool? business { get; set; }
        public List<object> multi_data { get; set; }
        public CCErrors errors { get; set; }
    }
    public class CCErrors
    {
    }
    public class CreditCardRoot
    {
        public CreditCardModel result { get; set; }
    }
    public class PrepaidCreditCardFlag
    {
        public bool IsPrepaidCreditCard { get; set; }
        public bool IsUserChangedPrepaidCard { get; set; }
        public string MemberId { get; set; }
        public string PlanType { get; set; }
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
}
