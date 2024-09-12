using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Schema", Schema = "HangFire")]
public partial class  Schema
 : BaseEntity{
    [Key]
    public int Version { get; set; }
}
