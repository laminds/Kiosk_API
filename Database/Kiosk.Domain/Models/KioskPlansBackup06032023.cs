using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Keyless]
[Table("KioskPlans_backup06032023")]
public partial class  KioskPlansBackup06032023
 : BaseEntity{
    public int Id { get; set; }

    public int? ClubNumber { get; set; }

    [Column("DataTrakPlan_1_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan1Monthly { get; set; }

    [Column("MarketingPlan_1_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan1Monthly { get; set; }

    [Column("Plan_1_MonthlyDate", TypeName = "datetime")]
    public DateTime? Plan1MonthlyDate { get; set; }

    [Column("PlanName_IMG_Plan_1_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan1Monthly { get; set; }

    [Column("PromoBanner_IMG_Plan_1_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan1Monthly { get; set; }

    [Column("Price_IMG_Plan_1_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan1Monthly { get; set; }

    [Column("Strikeout_field_Plan_1_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan1Monthly { get; set; }

    [Column("DataTrakPlan_2_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan2Monthly { get; set; }

    [Column("MarketingPlan_2_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan2Monthly { get; set; }

    [Column("Plan_2_MonthlyDate", TypeName = "datetime")]
    public DateTime? Plan2MonthlyDate { get; set; }

    [Column("PlanName_IMG_Plan_2_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan2Monthly { get; set; }

    [Column("PromoBanner_IMG_Plan_2_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan2Monthly { get; set; }

    [Column("Price_IMG_Plan_2_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan2Monthly { get; set; }

    [Column("Strikeout_field_Plan_2_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan2Monthly { get; set; }

    [Column("DataTrakPlan_3_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan3Monthly { get; set; }

    [Column("MarketingPlan_3_Monthly")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan3Monthly { get; set; }

    [Column("Plan_3_MonthlyDate", TypeName = "datetime")]
    public DateTime? Plan3MonthlyDate { get; set; }

    [Column("PlanName_IMG_Plan_3_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan3Monthly { get; set; }

    [Column("PromoBanner_IMG_Plan_3_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan3Monthly { get; set; }

    [Column("Price_IMG_Plan_3_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan3Monthly { get; set; }

    [Column("Strikeout_field_Plan_3_Monthly")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan3Monthly { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppliedDate { get; set; }

    [Column("DataTrakPlan_1_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan1Full { get; set; }

    [Column("MarketingPlan_1_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan1Full { get; set; }

    [Column("Plan_1_FullDate", TypeName = "datetime")]
    public DateTime? Plan1FullDate { get; set; }

    [Column("PlanName_IMG_Plan_1_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan1Full { get; set; }

    [Column("PromoBanner_IMG_Plan_1_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan1Full { get; set; }

    [Column("Price_IMG_Plan_1_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan1Full { get; set; }

    [Column("Strikeout_field_Plan_1_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan1Full { get; set; }

    [Column("DataTrakPlan_2_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan2Full { get; set; }

    [Column("MarketingPlan_2_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan2Full { get; set; }

    [Column("Plan_2_FullDate", TypeName = "datetime")]
    public DateTime? Plan2FullDate { get; set; }

    [Column("PlanName_IMG_Plan_2_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan2Full { get; set; }

    [Column("PromoBanner_IMG_Plan_2_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan2Full { get; set; }

    [Column("Price_IMG_Plan_2_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan2Full { get; set; }

    [Column("Strikeout_field_Plan_2_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan2Full { get; set; }

    [Column("DataTrakPlan_3_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string DataTrakPlan3Full { get; set; }

    [Column("MarketingPlan_3_Full")]
    [StringLength(100)]
    [Unicode(false)]
    public string MarketingPlan3Full { get; set; }

    [Column("Plan_3_FullDate", TypeName = "datetime")]
    public DateTime? Plan3FullDate { get; set; }

    [Column("PlanName_IMG_Plan_3_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PlanNameImgPlan3Full { get; set; }

    [Column("PromoBanner_IMG_Plan_3_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PromoBannerImgPlan3Full { get; set; }

    [Column("Price_IMG_Plan_3_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string PriceImgPlan3Full { get; set; }

    [Column("Strikeout_field_Plan_3_Full")]
    [StringLength(500)]
    [Unicode(false)]
    public string StrikeoutFieldPlan3Full { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string LanguageType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
