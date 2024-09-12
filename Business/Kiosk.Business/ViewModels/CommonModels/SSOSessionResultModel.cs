using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.CommonModels
{
    public class SSOSessionResultModel
    {
        public int result { get; set; }
        public Guid userId { get; set; }
    }
}
