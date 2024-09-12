using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.ManageMembership
{
    public class PersonalInfoModel
    {
        public int clubNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string email { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public bool isKeepMeUpdate { get; set; }
        public string memberId { get; set; }
        public bool sendEmail { get; set; }
        public string primaryPhone { get; set; }
        public string addressLine1 { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string zipCode { get; set; }
        public MarketingPreferences marketingPreferences { get; set; }
    }

    #region ::  Response From ABC API ::
    public class UpdateMemberPersonalInfoResponse
    {
        public UpdateMemberStatusModel status { get; set; }
        public UpdateMemberRequestModel request { get; set; }
        public string jsonResponse { get; set; }
    }

    public class UpdateMemberStatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
        public string messageCode { get; set; }
    }

    public class UpdateMemberRequestModel
    {
        public string clubNumber { get; set; }
        public string memberId { get; set; }
        public PersonalInfoModel requestBody { get; set; }
    }
    #endregion

    #region :: Passing Body Parameter In ABC API ::
    public class UpdateMemberPersonalInfoData
    {
        public PersonalInfoModel requestBody { get; set; }
    }
    public class MarketingPreferences
    {
        public string email { get; set; }
        public string sms { get; set; }
        public string directMail { get; set; }
        public string pushNotification { get; set; }
    }

    #endregion     
}
