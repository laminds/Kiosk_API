using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.FreshEmail
{
    public class FreshEmailResponse
    {
        public string EMAIL { get; set; }
        public string FINDING { get; set; }
        public string COMMENT { get; set; }
        public string COMMENT_CODE { get; set; }
        public string SUGG_EMAIL { get; set; }
        public string SUGG_COMMENT { get; set; }
        public string ERROR_RESPONSE { get; set; }
        public string ERROR { get; set; }
        public string UUID { get; set; }
        public bool IsValid { get; set; }
    }
    public class EmailModel
    {
        public string Summary { get; set; }
        public string Description { get; set; }
    }
    public class MailData
    {
        public string? summary { get; set; }
        public string? Discription { get; set; }
        public string? ProspectName { get; set; }
        public string? ProspectEmail { get; set; }
        public string? ProspectPhoneNumber { get; set; }
        public string? ProspectStatus { get; set; }
        public string? ScreenIndication {  get; set; }
        public string? IdentificationString { get; set; }
        public string? ClubNumber { get; set; }
        public string? PlanId { get; set; }
        public string? Planname { get; set; }
        public string? Plantype { get; set; }
        public string? PTPlanId { get; set; }
        public string? PTPlanName { get; set; }
        public string? PTPlanType { get; set; }
        public string? SGTPlanID { get; set; }
        public string? SGTPlanName { get; set; }
        public string? ImageData { get; set; }
        public string? Attachment { get; set; }
        public string? MemberStatus { get; set; }
        public string? Username { get; set; }
    }
}
