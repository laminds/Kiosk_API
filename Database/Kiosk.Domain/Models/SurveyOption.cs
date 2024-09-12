using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("SurveyOption")]
public partial class  SurveyOption
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    [Required]
    [Unicode(false)]
    public string QuestionorderId { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    [StringLength(50)]
    public string QuestionsTypeId { get; set; }

    [StringLength(256)]
    public string Createdby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }
}
