using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("URL")]
public partial class  Url
 : BaseEntity{
    [Key]
    [Column("URLId")]
    public int Urlid { get; set; }

    public int ClubNumber { get; set; }

    [Unicode(false)]
    public string ClubName { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string ShortCode { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string ControllerName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ActionName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [Unicode(false)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    public string DriverLicenseEncode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string State { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Column("SFId")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sfid { get; set; }

    public int? LeadId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ClubStationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Barcode { get; set; }

    public bool? IsActiveMember { get; set; }
}
