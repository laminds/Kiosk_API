using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  ProspectBarcode
 : BaseEntity{
    [Key]
    public int ProspectBarcodeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    public string DriverLicenseEncode { get; set; }
}
