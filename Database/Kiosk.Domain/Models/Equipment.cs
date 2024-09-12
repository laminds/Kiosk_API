using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Equipment", Schema = "Pickleball")]
public partial class  Equipment
 : BaseEntity{
    [Key]
    public int EquipmentId { get; set; }

    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [StringLength(50)]
    public string MemberType { get; set; }
}
