using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Lead", Schema = "Pickleball")]
public partial class  Lead1
 : BaseEntity{
    [Key]
    public int LeadId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string Gender { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    public int? EquipmentId { get; set; }

    [Column("SFId")]
    public string Sfid { get; set; }

    public int? ClubNumber { get; set; }

    public int? SourceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(256)]
    public string GuestType { get; set; }
}
