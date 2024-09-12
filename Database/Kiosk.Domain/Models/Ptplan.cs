using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("PTPlan", Schema = "YouFitJoin")]
public partial class  Ptplan
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    [StringLength(256)]
    public string ClubNumber { get; set; }

    [StringLength(256)]
    public string PlanId { get; set; }

    public string PlanLookupName { get; set; }

    public string PlanDisplayName { get; set; }

    public string PlanDescription { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActiveFromDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActiveToDate { get; set; }

    [StringLength(256)]
    public string Createdby { get; set; }

    [StringLength(256)]
    public string Modifiedby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
