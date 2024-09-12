using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

[Table("MemberPayment", Schema = "YouFitJoin")]
public partial class   MemberPayment
 : BaseEntity{
    [Key]
    public long Id { get; set; }

    public long? MemberId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardFirstName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardLastName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardAccountNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardExpMonth { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardExpYear { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardCvvCode { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardBillingZip { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardType { get; set; }

    public bool IsTodayBillingSameAsDraft { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurringPaymentType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardFirstName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardLastName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardExpMonth { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardExpYear { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardCvvCode { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardZipCode { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrCreditCardType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrDraftAccountFirstName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrDraftAccountLastName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrDraftAccountAccountType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrDraftAccountNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string RecurrDraftAccountRoutingNumber { get; set; }

    public string CreditCardTransactionResponse { get; set; }

    public string CreditCardTransactionResult { get; set; }

    public string RecurrCreditCardTransactionResponse { get; set; }

    public string RecurrCreditCardTransactionResult { get; set; }

    public string RecurrDraftAccountTransactionResponse { get; set; }

    public string RecurrDraftAccountTransactionResult { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string TodaysCreditCardTransactionId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string RecurringTransactionId { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? TotalAmount { get; set; }

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
    public string ContractRoutingNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractBankAccountNumber { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ContractExpirationDate { get; set; }

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
}
