﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("RecurringMemberSignatures", Schema = "FOD")]
public partial class  RecurringMemberSignature
 : BaseEntity{
    [Key]
    public long RecurringMemberSignatureId { get; set; }

    [Required]
    public byte[] SignatureBody { get; set; }

    [Column("SignatureURL")]
    [Unicode(false)]
    public string SignatureUrl { get; set; }
}