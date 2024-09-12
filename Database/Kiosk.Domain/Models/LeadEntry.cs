using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("LeadEntry")]
public partial class  LeadEntry
 : BaseEntity{
    [Key]
    public long LeadEntryId { get; set; }

    public int ClubNumber { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    public bool IsKeepMeUpdate { get; set; }

    public bool IsReceiveTextMessages { get; set; }

    public long ParentId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string SourceType { get; set; }

    [Column("SFId")]
    [Unicode(false)]
    public string Sfid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ReferringProspectName { get; set; }

    [Column("ReferralSFId")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReferralSfid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferralMemberId { get; set; }

    [Column("ABCStatusMessage")]
    public string AbcstatusMessage { get; set; }

    [Column("SFStatusMessage")]
    public string SfstatusMessage { get; set; }

    [Column("ABCCheckIn")]
    public bool? AbccheckIn { get; set; }

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
    public DateTime CreatedOn { get; set; }

    public int? TotalProspectValidateCount { get; set; }

    [Column("ABCProspectStatus")]
    [Unicode(false)]
    public string AbcprospectStatus { get; set; }

    [Column("FA_Finding")]
    [StringLength(50)]
    [Unicode(false)]
    public string FaFinding { get; set; }

    [Column("FA_CommentCode")]
    [StringLength(50)]
    [Unicode(false)]
    public string FaCommentCode { get; set; }

    [Column("FA_SuggestedEmail")]
    [StringLength(50)]
    [Unicode(false)]
    public string FaSuggestedEmail { get; set; }

    [Column("FA_SuggestedComments")]
    [StringLength(200)]
    [Unicode(false)]
    public string FaSuggestedComments { get; set; }

    public int TotalCheckinValidateCount { get; set; }

    [Column("TotalSFProspectValidateCount")]
    public int TotalSfprospectValidateCount { get; set; }

    [Column("IsFromPartnerQRCode")]
    public bool IsFromPartnerQrcode { get; set; }

    [Column("VFPStatus")]
    public bool Vfpstatus { get; set; }

    [Column("VFPId")]
    [StringLength(50)]
    [Unicode(false)]
    public string Vfpid { get; set; }

    [Column("VFPUserId")]
    [StringLength(50)]
    [Unicode(false)]
    public string VfpuserId { get; set; }

    [Column("VFPmessage")]
    [Unicode(false)]
    public string Vfpmessage { get; set; }

    [Unicode(false)]
    public string LeadType { get; set; }
}
