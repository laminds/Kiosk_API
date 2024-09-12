using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.ManageMembership
{
    public class PaymentInfoModel
    {
        public PersonalInfoModel billingInfo { get; set; }
        public PaymentInfoDetails paymentInformation { get; set; }
        public int clubNumber { get; set; }
        public string memberId { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }

    }

    public class PaymentInfoDetails
    {
        public string creditCardCVV { get; set; }
        //public string creditCardExpMonth { get; set; }

        public CreditCardexdate creditCardExpMonth { get; set; }
        public string creditCardExpYear { get; set; }
        public string creditCardFirstName { get; set; }
        public string creditCardLastName { get; set; }
        public string creditCardNumber { get; set; }
        public string creditCardType { get; set; }
        public string creditCardZipCode { get; set; }
    }
    #region CardOnFileModel

    public class CreditCardexdate
    {
        public string  month { get; set; }
        public string  monthName { get; set; }
    }
    public class CreditCard
    {
        public string creditCardFirstName { get; set; }
        public string creditCardLastName { get; set; }
        public string creditCardType { get; set; }
        public string creditCardAccountNumber { get; set; }
        public string creditCardExpMonth { get; set; }
        public string creditCardExpYear { get; set; }
    }

    public class Request
    {
        public string clubNumber { get; set; }
        public string memberId { get; set; }
        public RequestBody requestBody { get; set; }
    }

    public class RequestBody
    {
        public CreditCard creditCard { get; set; }
        public string sendEmail { get; set; }
    }

    public class Result
    {
        public string accountInfoId { get; set; }
    }

    public class UpdateMemberPaymentInfoResponse
    {
        public Status status { get; set; }
        public Result result { get; set; }
        public Request request { get; set; }
    }

    public class Status
    {
        public string message { get; set; }
        public string count { get; set; }
        public string messageCode { get; set; }
        public string missingMemberIds { get; set; }
    }

    public class CardOnFileModelRequestModel
    {
        public CreditCard creditCard { get; set; }
        public string sendEmail { get; set; }
    }

    #endregion


}
