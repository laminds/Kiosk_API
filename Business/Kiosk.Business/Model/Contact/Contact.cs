using Kiosk.Business.Model.FreshEmail;
using Kiosk.Business.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Contact
{
    public class Contact
    {
        public ContactModel RegisterObj { get; set; }
        public ContactModel LimeCardMemberObj { get; set; }
        public FreshEmailResponse EmailResponseObj { get; set; }
        public SignatureModel DisclaimerObj { get; set; }
        public SignatureModel SignatureObj { get; set; }
        public SignatureModel InitialSignatureObj { get; set; }
        public bool Isvalid { get; set; }
        public string Message { get; set; }
    }
}
