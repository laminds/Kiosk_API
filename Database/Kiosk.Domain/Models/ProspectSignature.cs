using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  ProspectSignature
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string OwnerId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ProspectId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ClubDate { get; set; }

    public string ClubTimeZone { get; set; }

    public bool IsKeepMeUpdate { get; set; }

    public bool IsReceiveTextMessages { get; set; }

    public bool ShowKeepMeUpdate { get; set; }

    public bool ShowReceiveTextMessages { get; set; }

    public bool IsMember { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<InitialSignatureImage> InitialSignatureImages { get; } = new List<InitialSignatureImage>();

    [InverseProperty("Parent")]
    public virtual ICollection<SignatureImage> SignatureImages { get; } = new List<SignatureImage>();
}
