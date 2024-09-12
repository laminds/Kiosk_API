using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Amenities
{
    public class PickleballModel : EquipmentRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public int EquipmentId { get; set; }
        public int ClubNumber { get; set; }
        public string SourceName { get; set; }
        public string MemberId { get; set; }
        public string GuestType { get; set; }
    }
    public class EquipmentRequestModel
    {
        public string MemberType { get; set; }
    }

    public class EquipmentModel
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string FullName { get; set; }
    }
}
