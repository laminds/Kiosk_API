using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.ABCResponseModels
{
    public class ABCPTRecurringServiceResonseModel
    {
        public ABCRecurringStatusModel status { get; set; }
        public ABCRecurringServiceResult result { get; set; }
        public ABCRecurringServiceRequest request { get; set; }
    }
    public class ABCRecurringStatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
        public string messageCode { get; set; }
    }
    public class ABCRecurringServiceResult
    {
        public string recurringServiceId { get; set; }
    }

    public class ABCRecurringServiceRequest
    {
        public string clubNumber { get; set; }
        public string memberId { get; set; }
        public string recurringServicePlanId { get; set; }
        public string serviceEmployeeId { get; set; }
        public string commissionsEmployeeId { get; set; }
        public string validationHash { get; set; }
        public string campaignId { get; set; }
        public ABCRecurringServiceCreditCardModel creditCard { get; set; }
    }
    public class ABCRecurringServiceCreditCardModel
    {
        public string creditCardFirstName { get; set; }
        public string creditCardLastName { get; set; }
        public string creditCardType { get; set; }
        public string creditCardAccountNumber { get; set; }
        public string creditCardExpMonth { get; set; }
        public string creditCardExpYear { get; set; }
        public string creditCardPostalCode { get; set; }
        public string creditCardCvvCode { get; set; }
    }
    public class ABCRecurringPlanCheckOutCustomResponseModel
    {
        public string PTMessage { get; set; }
        public string SGTMessage { get; set; }
        public ABCPTRecurringServiceResonseModel ABCPTRecurringServiceResonse { get; set; }
        public ABCPTRecurringServiceResonseModel ABCSGTRecurringServiceResonse { get; set; }
    }


    public class ABCPTUpdateClubAccountResponseModel
    {
        public ABCRecurringStatusModel status { get; set; }
        public ABCPTUpdateClubAccountResultModel result { get; set; }
        public ABCPTUpdateClubAccountRequestModel request { get; set; }
    }

    public class ABCPTUpdateClubAccountResultModel
    {
        public string accountInfoId { get; set; }
    }
    public class ABCPTUpdateClubAccountRequestModel
    {
        public string memberId { get; set; }
        public ABCPTClubAccountRootCreditCardAndDraftAccountModel requestBody { get; set; }
        public string clubNumber { get; set; }
    }
    public class ABCPTCommonClubAccountCreditCardModel
    {
        public string creditCardFirstName { get; set; }
        public string creditCardLastName { get; set; }
        public string creditCardType { get; set; }
        public string creditCardExpMonth { get; set; }
        public string creditCardExpYear { get; set; }
    }
    public class ABCPTCommonClubAccountBankAccountModel
    {
        public string draftAccountFirstName { get; set; }
        public string draftAccountLastName { get; set; }
        public string draftAccountRoutingNumber { get; set; }
        public string draftAccountType { get; set; }
    }
    public class ABCPTClubAccountRootCreditCardAndDraftAccountModel
    {
        public ABCPTClubAccountCreditCardRequestModel creditCard { get; set; }
        public ABCPTClubAccountDraftAccountModel draftAccount { get; set; }
    }
    public class ABCPTClubAccountCreditCardRequestModel : ABCPTCommonClubAccountCreditCardModel
    {
        public string creditCardAccountNumber { get; set; }
        public string creditCardPostalCode { get; set; }
    }
    public class ABCPTClubAccountDraftAccountModel : ABCPTCommonClubAccountBankAccountModel
    {
        public string draftAccountNumber { get; set; }
    }


 
}
