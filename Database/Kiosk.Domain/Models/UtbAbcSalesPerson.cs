using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_ABC_SalesPerson")]
[Index("ClubNumber", "EmployeeId", Name = "IX_ABCSalesPerson_EmployeeId_ClubNo")]
public partial class  UtbAbcSalesPerson
 : BaseEntity{
    [Key]
    [Column("ABCSalesPersonId")]
    public int AbcsalesPersonId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ClubNumber { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string EmployeeId { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }
}
