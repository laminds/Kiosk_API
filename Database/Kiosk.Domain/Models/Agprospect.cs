using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("AGProspects")]
public partial class  Agprospect
 : BaseEntity{
    [Key]
    [Column("AGProspectsId")]
    public long AgprospectsId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int ClubNumber { get; set; }

    [Column("SFId")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sfid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [Column("ReferralSFId")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReferralSfid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferralMemberId { get; set; }

    public long? LeadId { get; set; }
}
