using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_Inventory", Schema = "PICKLE")]
public partial class  UtbInventory
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ClubNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string InventoryName { get; set; }

    [Unicode(false)]
    public string InventoryDescription { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Price { get; set; }

    public bool IsActive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
