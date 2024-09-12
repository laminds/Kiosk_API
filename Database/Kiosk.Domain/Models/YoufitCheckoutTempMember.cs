using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class  YoufitCheckoutTempMember
 : BaseEntity{
    [Key]
    public int TempMemberId { get; set; }

    [Required]
    [StringLength(200)]
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

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Gender { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AgreementNumber { get; set; }

    public int ClubNumber { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string State { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string PlanId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string PlanName { get; set; }

    [Column("PTPlanPrice")]
    [StringLength(50)]
    [Unicode(false)]
    public string PtplanPrice { get; set; }

    [Column("PTPlanServiceQuantity")]
    public int? PtplanServiceQuantity { get; set; }

    [Column("PTPlanTotalPrice")]
    [StringLength(50)]
    [Unicode(false)]
    public string PtplanTotalPrice { get; set; }

    [Column("PTMonthlyRecurringCharge")]
    [StringLength(50)]
    [Unicode(false)]
    public string PtmonthlyRecurringCharge { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string InitiationFee { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstMonthDues { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastMonthDues { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MonthlyPayment { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TotalAmount { get; set; }

    [Column(TypeName = "date")]
    public DateTime? SaleDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FirstBillingDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractFirstName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractLastName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractCreditCardNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractBankAccountNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractRoutingNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractExpirationDate { get; set; }

    public byte[] SignatureBody { get; set; }

    public byte[] InitialSignatureBody { get; set; }

    public bool IsMemberShipAgreement { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AnnualFee { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TotSpotAmount { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string BankOwnerFirstName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string BankOwnerLastName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string BankAccountType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardAccountType { get; set; }
}
