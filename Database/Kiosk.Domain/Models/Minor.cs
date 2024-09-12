using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Keyless]
[Table("Minor")]
public partial class  Minor
 : BaseEntity{
    public long Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ClubNumber { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    public byte[] Signature { get; set; }

    [Column("SFID")]
    [Unicode(false)]
    public string Sfid { get; set; }

    [Column("SignatureSfID")]
    [Unicode(false)]
    public string SignatureSfId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Createdby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }
}
