using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[PrimaryKey("Key", "Value")]
[Table("Set", Schema = "HangFire")]
[Index("Key", "Score", Name = "IX_HangFire_Set_Score")]
public partial class  Set
 : BaseEntity{
    [Key]
    [StringLength(100)]
    public string Key { get; set; }

    public double Score { get; set; }

    [Key]
    [StringLength(256)]
    public string Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireAt { get; set; }
}
