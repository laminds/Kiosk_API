using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[PrimaryKey("Key", "Field")]
[Table("Hash", Schema = "HangFire")]
public partial class  Hash
 : BaseEntity{
    [Key]
    [StringLength(100)]
    public string Key { get; set; }

    [Key]
    [StringLength(100)]
    public string Field { get; set; }

    public string Value { get; set; }

    public DateTime? ExpireAt { get; set; }
}
