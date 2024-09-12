using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Index("ParentId", Name = "IX_ParentId")]
public partial class  SignatureImage
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

    public int ParentId { get; set; }

    [StringLength(120)]
    [Unicode(false)]
    public string ContentType { get; set; }

    public int BodyLength { get; set; }

    public byte[] Body { get; set; }

    [StringLength(120)]
    [Unicode(false)]
    public string ImagePath { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("SignatureImages")]
    public virtual ProspectSignature Parent { get; set; }
}
