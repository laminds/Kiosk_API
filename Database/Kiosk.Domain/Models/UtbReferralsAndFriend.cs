using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_Referrals_And_Friends", Schema = "PROMOVAULT")]
public partial class  UtbReferralsAndFriend
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    public int? ClubNumber { get; set; }

    [Column("form_id")]
    [StringLength(50)]
    public string FormId { get; set; }

    [Column("promo_location_id")]
    public long? PromoLocationId { get; set; }

    [Column("result_id")]
    [StringLength(50)]
    public string ResultId { get; set; }

    [Column("Referred_FirstName")]
    [StringLength(50)]
    public string ReferredFirstName { get; set; }

    [Column("Referred_LastName")]
    [StringLength(50)]
    public string ReferredLastName { get; set; }

    [Column("Referred_Email")]
    [StringLength(100)]
    public string ReferredEmail { get; set; }

    [Column("Referred_PhoneNumber")]
    [StringLength(15)]
    public string ReferredPhoneNumber { get; set; }

    [Column("Referred_Friend_FirstName")]
    [StringLength(50)]
    public string ReferredFriendFirstName { get; set; }

    [Column("Referred_Friend_lastName")]
    [StringLength(50)]
    public string ReferredFriendLastName { get; set; }

    [Column("Referred_Friend_Email")]
    [StringLength(100)]
    public string ReferredFriendEmail { get; set; }

    [Column("Referred_Friend_PhoneNumber")]
    [StringLength(15)]
    public string ReferredFriendPhoneNumber { get; set; }

    [StringLength(50)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(50)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
