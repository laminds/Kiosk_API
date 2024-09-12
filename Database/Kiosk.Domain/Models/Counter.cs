using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Keyless]
[Table("Counter", Schema = "HangFire")]
public partial class  Counter
 : BaseEntity{
    [Required]
    [StringLength(100)]
    public string Key { get; set; }

    public int Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireAt { get; set; }
}
