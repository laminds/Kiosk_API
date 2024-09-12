using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Contact
{
    public class RFCDetailsModel
    {
    }
    public class MemberReturnForCollectionModel
    {
        public string MemberId { get; set; }
        public string Email { get; set; }
        public string PrimaryPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RFCDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberId { get; set; }
        public string CreatedBy { get; set; }
        public int? ClbNumber { get; set; }
    }
}
