using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("SurveyQuestionsType")]
public partial class  SurveyQuestionsType
 : BaseEntity{
    [Key]
    public int QuestionsTypeId { get; set; }

    [StringLength(200)]
    public string QuestionsTypeName { get; set; }

    [StringLength(256)]
    public string QuestionsTypeOrderId { get; set; }
}
