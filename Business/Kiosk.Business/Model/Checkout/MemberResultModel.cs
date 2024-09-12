using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Checkout
{
    public class MemberResultModel
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
