using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("NonActiveMember")]
public partial class  NonActiveMember
 : BaseEntity{
    [Key]
    public int NonActiveMemberId { get; set; }

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

    public int ClbNumber { get; set; }

    public int SourceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GuestType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Barcode { get; set; }
}
