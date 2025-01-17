﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[PrimaryKey("Queue", "Id")]
[Table("JobQueue", Schema = "HangFire")]
public partial class  JobQueue
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    public long JobId { get; set; }

    [Key]
    [StringLength(50)]
    public string Queue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FetchedAt { get; set; }
}
