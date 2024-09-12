using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.ABC
{
    public class ProspectResponse
    {
        public ProspectStatusModel status { get; set; }
        public ProspectResultModel result { get; set; }
        public ProspectRequestModel request { get; set; }
    }

    public class ProspectStatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
    }
    public class ProspectResultModel
    {
        public string memberId { get; set; }
        public string barcode { get; set; }
    }

    public class ProspectRequestModel
    {
        public PostProspectRequestBodyModel postProspectRequestBody { get; set; }
    }

    public class PostProspectRequestBodyModel
    {
        public ProspectsModel[] prospects { get; set; }
    }


    // insert call response model
    public class ProspectRequest
    {
        public ProspectsModel[] prospects { get; set; }
    }

    public class ProspectsModel
    {
        public ProspectModel prospect { get; set; }
    }

    public class ProspectModel
    {
        public PersonalModel personal { get; set; }
        public AgreementModel agreement { get; set; }
    }

    public class PersonalModel
    {
        public string memberId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string primaryPhone { get; set; }
        public string mobilePhone { get; set; }
        public string gender { get; set; }
    }
    public class AgreementModel
    {
        public string beginDate { get; set; }
        public string referringMemberId { get; set; }
    }
}
