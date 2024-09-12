using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  Product
 : BaseEntity{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ProductName { get; set; }

    [Column(TypeName = "money")]
    public decimal? Rate { get; set; }
}
