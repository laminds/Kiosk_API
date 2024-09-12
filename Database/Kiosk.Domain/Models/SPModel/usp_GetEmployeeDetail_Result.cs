using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Domain.Models.SPModel
{
    public class usp_GetEmployeeDetail_Result
    {
        public int employee_id { get; set; }
        public string paychex_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string FullName { get; set; }
        public string employee_phone { get; set; }
        public string employee_email { get; set; }
        public string employee_status { get; set; }
        public string EpFullName { get; set; }
        public string Branch_Code { get; set; }
        public Nullable<int> clubNumber { get; set; }
        public string SPEmployeeId { get; set; }
        public int SalesPersonMissing { get; set; }
        public string BarCode { get; set; }
    }
}
