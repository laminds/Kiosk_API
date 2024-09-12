using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.CommonModels
{
    public class SessionModel
    {
        public string Name { get; set; }
        public string EmployeeNumber { get; set; }
        public string UserName { get; set; }
        public bool IsEquipmentFrontAccess { get; set; }
        public bool IsEquipmentAdminAccess { get; set; }
        public bool IsInspectionFrontAccess { get; set; }
        public bool IsInspectionAdminAccess { get; set; }
        public bool IsEquipmentExternalVendor { get; set; }

    }
}
