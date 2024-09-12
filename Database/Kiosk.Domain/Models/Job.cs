using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Job", Schema = "HangFire")]
public partial class  Job
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    public long? StateId { get; set; }

    [StringLength(20)]
    public string StateName { get; set; }

    [Required]
    public string InvocationData { get; set; }

    [Required]
    public string Arguments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireAt { get; set; }

    [InverseProperty("Job")]
    public virtual ICollection<JobParameter> JobParameters { get; } = new List<JobParameter>();

    [InverseProperty("Job")]
    public virtual ICollection<State> States { get; } = new List<State>();
}
