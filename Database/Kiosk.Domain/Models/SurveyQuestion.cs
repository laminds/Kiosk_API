using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("SurveyQuestion")]
public partial class  SurveyQuestion
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string FormInputName { get; set; }

    [Required]
    [StringLength(256)]
    public string QuestionOrderId { get; set; }

    [StringLength(256)]
    public string QuestionsTypeOrderId { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(256)]
    public string ParentId { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(256)]
    public string QuestionDisplayName { get; set; }
}
