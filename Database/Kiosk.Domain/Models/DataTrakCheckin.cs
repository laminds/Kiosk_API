using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("DataTrakCheckin")]
public partial class  DataTrakCheckin
 : BaseEntity{
    [Key]
    public int DataTrakCheckinId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutTimeStamp { get; set; }

    public int ClbNumber { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public bool IsCheckOut { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CheckOutBy { get; set; }
}
