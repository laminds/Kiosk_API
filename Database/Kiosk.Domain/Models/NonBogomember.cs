using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("NonBOGOMembers", Schema = "PT")]
public partial class  NonBogomember
 : BaseEntity{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string AgreementNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RecurringStatus { get; set; }

    [Column("PurchasedAnotherPT")]
    [StringLength(10)]
    [Unicode(false)]
    public string PurchasedAnotherPt { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EmailAddress { get; set; }
}
