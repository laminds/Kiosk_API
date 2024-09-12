using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_ABC_Employee_Departments")]
public partial class  UtbAbcEmployeeDepartment
 : BaseEntity{
    [Key]
    public int DepartmentId { get; set; }

    [StringLength(200)]
    public string EmployeeId { get; set; }

    [StringLength(200)]
    public string Department { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ClubNumber { get; set; }

    [StringLength(200)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(200)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }
}
