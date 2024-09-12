using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.ABCResponseModels
{
    public class ABCMemberLog
    {
    }
    public class NewMemberABCLog
    {
        public int MemberLogId { get; set; }
        public string Source { get; set; }
        public string AgreementType { get; set; }
        public string Club { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MemberId { get; set; }
        public string PlanName { get; set; }
        public string Message { get; set; }
        public string JsonRequest { get; set; }
        public long? NewMemberId { get; set; }
        public string JsonResponse { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MethodName { get; set; }
        public string APIUrl { get; set; }
    }
}
