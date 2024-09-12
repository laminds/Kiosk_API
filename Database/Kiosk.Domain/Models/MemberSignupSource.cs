using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("MemberSignupSource")]
public partial class  MemberSignupSource
 : BaseEntity{
    [Key]
    public int MemberSignupSourceId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string SourceName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }
}
