using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Contact
{
    public class SignatureModel
    {
        public string ImageSrc { get; set; }
        public bool IsKeepMeUpdate { get; set; }
        public bool IsReceiveTextMessages { get; set; }
        public DateTime ClubDate { get; set; }
        public string ClubTimeZone { get; set; }
        public bool IsKeepMeUpdate_Old { get; set; }
        public bool IsReceiveTextMessages_Old { get; set; }
    }
}
