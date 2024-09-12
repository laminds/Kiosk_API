using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("Totspot", Schema = "YouFitJoin")]
public partial class  Totspot
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool? IsDeleted { get; set; }

    [StringLength(256)]
    public string Createdby { get; set; }

    [StringLength(256)]
    public string Modifiedby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}
