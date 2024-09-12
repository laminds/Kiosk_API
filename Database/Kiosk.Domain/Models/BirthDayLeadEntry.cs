using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("BirthDayLeadEntry")]
public partial class  BirthDayLeadEntry
 : BaseEntity{
    [Key]
    public long BirthDayEntryId { get; set; }

    public int ClubNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Gender { get; set; }

    public bool IsKeepMeUpdate { get; set; }

    [Column("SFId")]
    [Unicode(false)]
    public string Sfid { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BarCode { get; set; }

    [Column("ABCStatusMessage")]
    public string AbcstatusMessage { get; set; }

    [Column("SFStatusMessage")]
    public string SfstatusMessage { get; set; }

    [Column("ABCCheckIn")]
    public bool AbccheckIn { get; set; }

    [StringLength(500)]
    public string CheckInStatus { get; set; }

    [StringLength(500)]
    public string CheckInMessage { get; set; }

    [Column("ABCCheckInsStatus")]
    public string AbccheckInsStatus { get; set; }

    [Column("PublicIPAddress")]
    [StringLength(100)]
    [Unicode(false)]
    public string PublicIpaddress { get; set; }

    [Column("LocalIPAddress")]
    [StringLength(100)]
    [Unicode(false)]
    public string LocalIpaddress { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BrowserName { get; set; }

    [Unicode(false)]
    public string UserAgent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public bool IsCheckOut { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutTimeStamp { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CheckOutBy { get; set; }
}
