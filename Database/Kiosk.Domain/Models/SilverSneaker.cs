using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("SilverSneaker")]
public partial class  SilverSneaker
 : BaseEntity{
    [Key]
    public long SilverSneakerId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ShortCode { get; set; }

    public int ClubNumber { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string SearchId { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string SearchMethodType { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column("PublicIPAddress")]
    [StringLength(100)]
    [Unicode(false)]
    public string PublicIpaddress { get; set; }

    [Column("LocalIPAddress")]
    [StringLength(100)]
    [Unicode(false)]
    public string LocalIpaddress { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BrowserName { get; set; }

    [Unicode(false)]
    public string UserAgent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhoneKisokShortCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Source { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [Column("SFId")]
    [StringLength(50)]
    [Unicode(false)]
    public string Sfid { get; set; }
}

