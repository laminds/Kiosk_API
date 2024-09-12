using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  CheckIn
 : BaseEntity{
    [Key]
    public int CheckInId { get; set; }

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

    public int ClubNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferralMemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GuestType { get; set; }

    [Column("ABCCheckIn")]
    public bool AbccheckIn { get; set; }

    public string CheckInStatus { get; set; }

    public string CheckInMessage { get; set; }

    [Column("ABCProspectStatus")]
    public string AbcprospectStatus { get; set; }

    [Column("ABCCheckInsStatus")]
    public string AbccheckInsStatus { get; set; }
}
