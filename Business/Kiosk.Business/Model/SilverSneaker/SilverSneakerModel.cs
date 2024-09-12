using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.SilverSneaker
{
    public class SilverSneakerModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ClubNumber { get; set; }
        public string Source { get; set; }
        public string MemberId { get; set; }
        public string MethodType { get; set; }
        public string HSId { get; set; }

    }

    public class SilverSneakerSearchResultModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
    public class SilverSneakerQRCodeResponseModel
    {
        public SilverSneakerDetail UserDetail { get; set; }
        public string ShortCode { get; set; }
    }
    public partial class SilverSneakerDetail
    {
        public long SilverSneakerId { get; set; }
        public string ShortCode { get; set; }
        public int ClubNumber { get; set; }
        public string SearchId { get; set; }
        public string SearchMethodType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PublicIPAddress { get; set; }
        public string LocalIPAddress { get; set; }
        public string BrowserName { get; set; }
        public string UserAgent { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string PhoneKisokShortCode { get; set; }
        public string Source { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SFId { get; set; }
        public string MemberId { get; set; }
    }

    public class SilverPlanSearchModel
    {
        public int ClubNumber { get; set; }
        public string PlanId { get; set; }
        public string PromoCode { get; set; }
    }
}
