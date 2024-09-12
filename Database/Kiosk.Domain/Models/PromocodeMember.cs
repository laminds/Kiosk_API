using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Keyless]
public partial class  PromocodeMember
 : BaseEntity{
    public long Id { get; set; }

    public long? MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PlanName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PlanType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AgreementPlanType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PromotionCode { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? InitiationFee { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? AnnualFee { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FirstMonthDues { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? LastMonthDues { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? MonthlyPayment { get; set; }

    [Column("Agreement_Number")]
    public string AgreementNumber { get; set; }

    [Column("AgreementURL")]
    public string AgreementUrl { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
