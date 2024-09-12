using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("NewPTMember")]
public partial class  NewPtmember
 : BaseEntity{
    [Key]
    [Column("NewPTMemberId")]
    public long NewPtmemberId { get; set; }

    public long NewMemberId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Gender { get; set; }

    public int ClubNumber { get; set; }

    public bool IsKeepMeUpdate { get; set; }

    public bool IsReceiveTextMessages { get; set; }

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

    [Column("SFId")]
    [Unicode(false)]
    public string Sfid { get; set; }

    public int? SourceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SourceName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AgreementNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RecurringServiceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PlanName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PromotionCode { get; set; }

    public int? TotalSessions { get; set; }
    public bool IsTodayBillingSameAsDraft { get; set; }
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

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? InitiationFee { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FirstMonthDues { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? LastMonthDues { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? MonthlyPayment { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("ABCStatusMessage")]
    public string AbcstatusMessage { get; set; }

    [Column("ABCPTStatusMessage")]
    public string AbcptstatusMessage { get; set; }

    [Column("ABCSGTStatusMessage")]
    public string ABCSGTStatusMessage { get; set; }

    [Column("SFStatusMessage")]
    public string SfstatusMessage { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public bool IsNewLead { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SalesPersonId { get; set; }

    [StringLength(200)]
    public string SalesPersonName { get; set; }

    public int? TotalValidateCount { get; set; }

    public byte[] SignatureBody { get; set; }

    public byte[] InitialSignatureBody { get; set; }

    [Column("AgreementURL")]
    public string AgreementUrl { get; set; }


    [Column("SGTAgreementURL")]
    public string SGTAgreementURL { get; set; }

    public bool IsContractUpdated { get; set; }

    [Column("Agreement_ModifiedOn", TypeName = "datetime")]
    public DateTime? AgreementModifiedOn { get; set; }

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

    public bool? IsPrepaidCard { get; set; }

    public bool? IsUserChangedPrepaidCard { get; set; }
    public Nullable<decimal> PTMonthlyRecurringCharge { get; set; }
    public Nullable<decimal> ProcessingFee { get; set; }
    public string PaymentType { get; set; }
    public Nullable<int> SGTTotalSessions { get; set; }
    public string SGTPlanId { get; set; }
    public string SGTPlanName { get; set; }
    public Nullable<decimal> SGTInitiationFee { get; set; }
    public Nullable<decimal> SGTFirstMonthDues { get; set; }
    public Nullable<decimal> SGTMonthlyPayment { get; set; }
    public Nullable<decimal> SGTTotalAmount { get; set; }
    public Nullable<decimal> SGTMonthlyRecurringCharge { get; set; }
    public string SGTRecurringServiceId { get; set; }
    public string SecondaryCCFirstName { get; set; }
    public string SecondaryCCLastName { get; set; }
    public string SecondaryCCNumber { get; set; }
    public string SecondaryCCType { get; set; }
    public string SecondaryCCExpirationDate { get; set; }
    public string SecondaryCCZipCode { get; set; }
    public string RecurringType { get; set; }
    public string hs_object_id { get; set; }
}
