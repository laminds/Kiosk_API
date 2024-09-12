using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("RenewMember")]
public partial class  RenewMember
 : BaseEntity{
    [Key]
    public int RenewMemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

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

    public int ClbNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [Column("ABCCheckIn")]
    public bool AbccheckIn { get; set; }

    public string CheckInStatus { get; set; }

    public string CheckInMessage { get; set; }

    [Column("ABCCheckInsStatus")]
    public string AbccheckInsStatus { get; set; }

    public bool HasAppointment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentStart { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentEnd { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AppointmentSubject { get; set; }

    public bool AppointmentShown { get; set; }

    public bool IsRenewMember { get; set; }

    public string Message { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
