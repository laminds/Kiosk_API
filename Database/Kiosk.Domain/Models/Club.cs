using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Spatial;

namespace Kiosk.Domain.Models;

[Table("Club")]
public partial class  Club
 : BaseEntity{
    [Key]
    public int ClubId { get; set; }

    public int Code { get; set; }

    public string Name { get; set; }

    public string Address1 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Email { get; set; }

    public bool Active { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    //public Geography Geolocation { get; set; }

    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
}
