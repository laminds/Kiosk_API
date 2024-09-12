using Kiosk.Business.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.General
{
    public class Properties
    {
        public string abcid { get; set; }
        public string agreement_number { get; set; }
        public DateTime? birthdate { get; set; }
        public string campaign_details { get; set; }
        public DateTime? createdate { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string gender { get; set; }
        public DateTime? guest_pass_activation_date { get; set; }
        public DateTime? guest_pass_download_date { get; set; }
        public string guest_pass_duration { get; set; }
        public DateTime? guest_pass_expiration_date { get; set; }
        public string homeclub { get; set; }
        public string hs_object_id { get; set; }
        public bool? isactive { get; set; }
        public DateTime? lastmodifieddate { get; set; }
        public string lastmodifiedtimestamp { get; set; }
        public string lastname { get; set; }
        public string memberstatusreason { get; set; }
        public string phone { get; set; }
        public string state { get; set; }
        public string mobilephone { get; set; }
        public bool? hubspot_phone_optout { get; set; }
        public DateTime? phone_opt_in { get; set; }
        public bool? hubspot_sms_optout { get; set; }
        public DateTime? sms_opt_in { get; set; }
        public DateTime? email_opt_in { get; set; }
        public DateTime? dgr_recent_sign_date { get; set; }

        public static implicit operator Properties(ContactModel v)
        {
            throw new NotImplementedException();
        }
    }
    public class HubSpotResponseModel
    {
        public int total { get; set; }
        public List<SearchContactResult> results { get; set; }
    }

    public class SearchContactResult
    {
        public string id { get; set; }
        public Properties properties { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool archived { get; set; }
    }
}
