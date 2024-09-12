using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.HubSpot
{
    public class ContactEmailModel
    {
    }
    public class CheckContactRequestModel
    {
        public string Email { get; set; }
    }
    public class CheckContactEmailHSRequestModel
    {
        public int limit { get; set; }
        public List<EmailFilterGroup> filterGroups { get; set; }
        public List<string> properties { get; set; }
        public List<EmailSort> sorts { get; set; }
    }
    public class EmailFilter
    {
        public string propertyName { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }

    public class EmailFilterGroup
    {
        public List<EmailFilter> filters { get; set; }
    }

    public class EmailSort
    {
        public string propertyName { get; set; }
        public string direction { get; set; }
    }
    public class EmailNext
    {
        public string after { get; set; }
    }

    public class EmailPaging
    {
        public EmailNext next { get; set; }
    }

    public class Properties
    {
        public string abcid { get; set; }
        public string address { get; set; }
        public string agreement_number { get; set; }
        public Nullable<DateTime> birthdate { get; set; }
        public string campaign_details { get; set; }
        public string city { get; set; }
        public Nullable<DateTime> createdate { get; set; }
        public string dgr_new_contact_record { get; set; }
        public string email { get; set; }
        public Nullable<DateTime> email_opt_in { get; set; }
        public string firstname { get; set; }
        public string gender { get; set; }
        public Nullable<DateTime> guest_pass_activation_date { get; set; }
        public Nullable<DateTime> guest_pass_download_date { get; set; }
        public string guest_pass_duration { get; set; }
        public Nullable<DateTime> guest_pass_expiration_date { get; set; }
        public string homeclub { get; set; }
        public string hs_object_id { get; set; }
        public Nullable<bool> hubspot_phone_optout { get; set; }
        public Nullable<bool> hubspot_sms_optout { get; set; }
        public Nullable<bool> isactive { get; set; }
        public DateTime lastmodifieddate { get; set; }
        public string lastmodifiedtimestamp { get; set; }
        public string lastname { get; set; }
        public string memberstatusreason { get; set; }
        public string mobilephone { get; set; }
        public string phone { get; set; }
        public Nullable<DateTime> phone_opt_in { get; set; }
        public string referring_member_first { get; set; }
        public string referring_member_id { get; set; }
        public string sales_person_paycom_id { get; set; }
        public string sfprospectid { get; set; }
        public Nullable<DateTime> sms_opt_in { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string sales_person_id { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public Properties properties { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool archived { get; set; }
    }

    public class ContactEmailResponseModel
    {
        public int total { get; set; }
        public List<Result> results { get; set; }
        public bool? isActive { get; set; }
        public EmailPaging paging { get; set; }
    }

    public class UpdateEmail
    {
        public string HsId { get; set; }
        public string email { get; set; }

    }
}
