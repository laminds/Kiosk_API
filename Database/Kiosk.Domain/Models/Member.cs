using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kiosk.Domain.Models;

public partial class   Member
 : BaseEntity{
    [Key]
    public int Id { get; set; }

    [Column("memberId")]
    [StringLength(50)]
    [Unicode(false)]
    public string MemberId { get; set; }

    [Column("firstName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Column("lastName")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column("homeClub")]
    public int HomeClub { get; set; }

    [Column("email")]
    [StringLength(256)]
    [Unicode(false)]
    public string Email { get; set; }

    [Column("addressLine1")]
    [StringLength(200)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [Column("addressLine2")]
    [StringLength(200)]
    [Unicode(false)]
    public string AddressLine2 { get; set; }

    [Column("city")]
    [StringLength(100)]
    [Unicode(false)]
    public string City { get; set; }

    [Column("state")]
    [StringLength(10)]
    [Unicode(false)]
    public string State { get; set; }

    [Column("postalCode")]
    [StringLength(10)]
    [Unicode(false)]
    public string PostalCode { get; set; }

    [Column("countryCode")]
    [StringLength(10)]
    [Unicode(false)]
    public string CountryCode { get; set; }

    [Column("primaryPhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string PrimaryPhone { get; set; }

    [Column("mobilePhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string MobilePhone { get; set; }

    [Column("workPhone")]
    [StringLength(20)]
    [Unicode(false)]
    public string WorkPhone { get; set; }

    [Column("workPhoneExt")]
    [StringLength(10)]
    [Unicode(false)]
    public string WorkPhoneExt { get; set; }

    [Column("emergencyFirstName")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmergencyFirstName { get; set; }

    [Column("emergencyLastName")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmergencyLastName { get; set; }

    [Column("emergencyPhone")]
    [StringLength(15)]
    [Unicode(false)]
    public string EmergencyPhone { get; set; }

    [Column("emergencyExt")]
    [StringLength(10)]
    [Unicode(false)]
    public string EmergencyExt { get; set; }

    [Column("creditcardFirstName")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditcardFirstName { get; set; }

    [Column("creditcardLastName")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditcardLastName { get; set; }

    [Column("creditcardType")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditcardType { get; set; }

    [Column("created_by")]
    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_datetime")]
    [StringLength(50)]
    [Unicode(false)]
    public string ModifiedDatetime { get; set; }

    [Column("modified_by", TypeName = "datetime")]
    public DateTime? ModifiedBy { get; set; }

    [Column("MemberInfoABCResponse")]
    public int MemberInfoAbcresponse { get; set; }

    [Column("MemberInfoABCResponseDateOn", TypeName = "datetime")]
    public DateTime? MemberInfoAbcresponseDateOn { get; set; }

    [Column("MemberInfoABCStatus")]
    [StringLength(8000)]
    [Unicode(false)]
    public string MemberInfoAbcstatus { get; set; }

    [Column("creditCardAccountNumberLastFour")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardAccountNumberLastFour { get; set; }

    [Column("creditCardExpMonth")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardExpMonth { get; set; }

    [Column("creditCardExpYear")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardExpYear { get; set; }

    [Column("creditCardPostalCode")]
    [StringLength(500)]
    [Unicode(false)]
    public string CreditCardPostalCode { get; set; }

    [Column("draftAccountFirstName")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountFirstName { get; set; }

    [Column("draftAccountLastName")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountLastName { get; set; }

    [Column("draftAccountRoutingNumber")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountRoutingNumber { get; set; }

    [Column("draftAccountNumberLastFour")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountNumberLastFour { get; set; }

    [Column("draftAccountType")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountType { get; set; }

    [Column("draftAccountBankName")]
    [StringLength(500)]
    [Unicode(false)]
    public string DraftAccountBankName { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string PaymentMethodType { get; set; }

    [Column("BillingInfoABCResponse")]
    public int BillingInfoAbcresponse { get; set; }

    [Column("BillingInfoABCResponseDateOn", TypeName = "datetime")]
    public DateTime? BillingInfoAbcresponseDateOn { get; set; }

    [Column("BillingInfoABCStatus")]
    [StringLength(8000)]
    [Unicode(false)]
    public string BillingInfoAbcstatus { get; set; }
}
