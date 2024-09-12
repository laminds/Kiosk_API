using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.SaveWorkFlow
{
    public class SaveWorkFlow
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string PageName { get; set; }
        public string ClubNumber {get; set; }
        public string PageUrl { get; set; }
        public string ActionType { get; set; }
        public string SessionId { get; set; }
    }
}
