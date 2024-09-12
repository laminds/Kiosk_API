using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("utb_ABCEmployees")]
[Index("ClubNumber", "EmployeeId", Name = "IX_ABCEmployees_EmployeeId_ClubNo")]
public partial class  UtbAbcemployee
 : BaseEntity{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("club_number")]
    public int ClubNumber { get; set; }

    [Column("employee_Id")]
    [StringLength(200)]
    [Unicode(false)]
    public string EmployeeId { get; set; }

    [Column("barcode")]
    [StringLength(200)]
    [Unicode(false)]
    public string Barcode { get; set; }

    [Column("first_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Column("middle_Initial")]
    [StringLength(200)]
    [Unicode(false)]
    public string MiddleInitial { get; set; }

    [Column("last_Name")]
    [StringLength(200)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column("birth_date", TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [Column("profile")]
    [StringLength(200)]
    [Unicode(false)]
    public string Profile { get; set; }

    [Column("address")]
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("city")]
    [StringLength(200)]
    [Unicode(false)]
    public string City { get; set; }

    [Column("state")]
    [StringLength(200)]
    [Unicode(false)]
    public string State { get; set; }

    [Column("postal_code")]
    [StringLength(200)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    [Column("country_code")]
    [StringLength(200)]
    [Unicode(false)]
    public string CountryCode { get; set; }

    [Column("email")]
    [StringLength(200)]
    [Unicode(false)]
    public string Email { get; set; }

    [Column("home_phone")]
    [StringLength(200)]
    [Unicode(false)]
    public string HomePhone { get; set; }

    [Column("cell_phone")]
    [StringLength(200)]
    [Unicode(false)]
    public string CellPhone { get; set; }

    [Column("work_phone")]
    [StringLength(200)]
    [Unicode(false)]
    public string WorkPhone { get; set; }

    [Column("work_phone_extension")]
    [StringLength(200)]
    [Unicode(false)]
    public string WorkPhoneExtension { get; set; }

    [Column("emergency_phone")]
    [StringLength(200)]
    [Unicode(false)]
    public string EmergencyPhone { get; set; }

    [Column("emergency_phone_extension")]
    [StringLength(200)]
    [Unicode(false)]
    public string EmergencyPhoneExtension { get; set; }

    [Column("wage", TypeName = "decimal(18, 2)")]
    public decimal? Wage { get; set; }

    [Column("employee_status")]
    [StringLength(200)]
    [Unicode(false)]
    public string EmployeeStatus { get; set; }

    [Column("commission_level")]
    [StringLength(200)]
    [Unicode(false)]
    public string CommissionLevel { get; set; }

    [Column("start_date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("termination_date", TypeName = "datetime")]
    public DateTime? TerminationDate { get; set; }

    [Column("training_level")]
    [StringLength(200)]
    [Unicode(false)]
    public string TrainingLevel { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("created_by")]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modified_by")]
    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("deleted_by")]
    [StringLength(50)]
    [Unicode(false)]
    public string DeletedBy { get; set; }
}
