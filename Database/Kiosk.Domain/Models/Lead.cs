using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Lead")]
[Index("ClbNumber", "CreatedOn", Name = "IX_Lead_Club_CreatedOn")]
public partial class Lead
 : BaseEntity
{
    [Key]
    public long LeadId { get; set; }

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

    [Column("SFId")]
    public string Sfid { get; set; }

    public bool IsNewLead { get; set; }

    public int ClbNumber { get; set; }

    public int SourceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferralId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferralMemberId { get; set; }

    public bool ExpiredFreePass { get; set; }

    public int? TotalValidateCount { get; set; }

    [Column("ABCCheckIn")]
    public bool AbccheckIn { get; set; }

    public string CheckInStatus { get; set; }

    public string CheckInMessage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GuestType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    public bool HasAppointment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentStart { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentEnd { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AppointmentSubject { get; set; }

    public bool AppointmentShown { get; set; }

    [Column("ABCProspectStatus")]
    public string AbcprospectStatus { get; set; }

    [Column("ABCCheckInsStatus")]
    public string AbccheckInsStatus { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractUrl { get; set; }

    public bool SkipExistingProspect { get; set; }

    public int TotalAppointmentValidateCount { get; set; }

    [Column("SFAppointmentStatus")]
    public string SfappointmentStatus { get; set; }

    public int? TotalProspectValidateCount { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string State { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    public string DriverLicenseEncode { get; set; }

    public bool IsCheckOut { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutTimeStamp { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CheckOutBy { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DisclaimerUrl { get; set; }

    public string HearAbout { get; set; }

    [StringLength(50)]
    public string Gender { get; set; }

    [StringLength(200)]
    public string SalesPersonEmployeeNumber { get; set; }

    [StringLength(200)]
    public string SalesPersonName { get; set; }

    [Column("IsRFC")]
    public bool? IsRfc { get; set; }

    [Column("IsRFCViewed")]
    public bool? IsRfcviewed { get; set; }

    [Column("RFCViewedBy")]
    [StringLength(50)]
    [Unicode(false)]
    public string RfcviewedBy { get; set; }

    [Column("RFCViewedOn", TypeName = "datetime")]
    public DateTime? RfcviewedOn { get; set; }

    [Column("IsOpenHouse")]
    public bool? IsOpenHouse { get; set; }

    [Column("GuestPaidPassDate", TypeName = "datetime")]
    public DateTime? GuestPaidPassDate { get; set; }

    [Column("IsGuestPaidPass")]
    public bool? IsGuestPaidPass { get; set; }
    [StringLength(500)]
    public string referring_member_id { get; set; }
    public string SourceName { get; set; }

    [StringLength(400)]
    public string hsId { get; set; }

    public int ContactId { get; set; }

}
