using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("YouFitCheckout.PTPlan")]
public partial class  YouFitCheckoutPtplan
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
    public string ClubNumber { get; set; }

    [StringLength(50)]
    public string PlanId { get; set; }

    [StringLength(200)]
    public string PlanLookupName { get; set; }

    [StringLength(500)]
    public string PlanDisplayName { get; set; }

    [StringLength(1000)]
    public string PlanDescription { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActiveFromDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActiveToDate { get; set; }

    [StringLength(50)]
    public string Createdby { get; set; }

    [StringLength(50)]
    public string Modifiedby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
