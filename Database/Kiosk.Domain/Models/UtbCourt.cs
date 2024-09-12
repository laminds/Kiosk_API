using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_Court", Schema = "PICKLE")]
public partial class  UtbCourt
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ClubNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CourtName { get; set; }

    [Unicode(false)]
    public string CourtDescription { get; set; }

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
