using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[PrimaryKey("JobId", "Id")]
[Table("State", Schema = "HangFire")]
public partial class  State
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    [Key]
    public long JobId { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Reason { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public string Data { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("States")]
    public virtual Job Job { get; set; }
}
