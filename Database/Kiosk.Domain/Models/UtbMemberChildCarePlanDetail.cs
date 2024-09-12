using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_Member_ChildCarePlanDetails")]
public partial class  UtbMemberChildCarePlanDetail
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(50)]
    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string Gender { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    [StringLength(256)]
    public string PlanName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PlanPrice { get; set; }

    public int? ClubNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(256)]
    public string MemberId { get; set; }

    public int? SourceId { get; set; }

    [StringLength(50)]
    public string MemberType { get; set; }

    [StringLength(256)]
    public string SalesPersonEmployeeNumber { get; set; }

    [StringLength(256)]
    public string SalesPersonName { get; set; }

    [StringLength(256)]
    public string GuestType { get; set; }
}
