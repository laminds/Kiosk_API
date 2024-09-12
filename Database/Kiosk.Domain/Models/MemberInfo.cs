using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("MemberInfo", Schema = "YouFitJoin")]
public partial class   MemberInfo
 : BaseEntity{
    [Key]
    public long MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Gender { get; set; }

    public int? ClubNumber { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string State { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Column("Terms_Condition")]
    public bool? TermsCondition { get; set; }

    public bool? IsKeepMeUpdated { get; set; }

    [Column("Communication_Consent")]
    public bool? CommunicationConsent { get; set; }

    [Column("SFId")]
    [Unicode(false)]
    public string Sfid { get; set; }

    [Column("SFStatusMessage")]
    public string SfstatusMessage { get; set; }

    public int? SourceId { get; set; }

    [Column("ABCMemberId")]
    [StringLength(50)]
    [Unicode(false)]
    public string AbcmemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AgreementNumber { get; set; }

    [Column("ABCStatusMessage")]
    public string AbcstatusMessage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RecurringServiceId { get; set; }

    [Column("ABCPTStatusMessage")]
    [Unicode(false)]
    public string AbcptstatusMessage { get; set; }

    [Column("DWH_Flag")]
    public bool? DwhFlag { get; set; }

    public byte[] SignatureBody { get; set; }

    public byte[] InitialSignatureBody { get; set; }

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
