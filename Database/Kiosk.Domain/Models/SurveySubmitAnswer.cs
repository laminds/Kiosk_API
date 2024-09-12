using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  SurveySubmitAnswer
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string QuestionId { get; set; }

    public int? OptionId { get; set; }

    public string Text { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string ClubNumber { get; set; }

    [Column("SFId")]
    public string Sfid { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public int? TeamMember { get; set; }
}
