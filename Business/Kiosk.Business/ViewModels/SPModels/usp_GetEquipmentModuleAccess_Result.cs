using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.SPModels
{
    public class usp_GetEquipmentModuleAccess_Result
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAccess { get; set; }
    }
}
