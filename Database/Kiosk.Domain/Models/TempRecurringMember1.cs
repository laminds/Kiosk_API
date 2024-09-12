using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("TempRecurringMembers", Schema = "PT")]
public partial class  TempRecurringMember1
 : BaseEntity{
    [Key]
    public long TempRecurringMemberId { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    public int SourceId { get; set; }

    public int ClubNumber { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string PlanId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string PlanName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PlanPrice { get; set; }

    public int PlanServiceQuantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PlanTotalPrice { get; set; }

    [Column(TypeName = "date")]
    public DateTime SaleDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime FirstBillingDate { get; set; }

    public byte[] SignatureBody { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MonthlyRecurringCharge { get; set; }
}
