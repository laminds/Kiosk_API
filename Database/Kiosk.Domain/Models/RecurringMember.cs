using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("RecurringMembers", Schema = "ENR")]
public partial class  RecurringMember
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    public int? ClubNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanName { get; set; }

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

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Gender { get; set; }

    public bool IsReceiveMsgOrCalls { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("PublicIPAddress")]
    [StringLength(50)]
    [Unicode(false)]
    public string PublicIpaddress { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BrowserName { get; set; }

    [Unicode(false)]
    public string UserAgent { get; set; }
}
