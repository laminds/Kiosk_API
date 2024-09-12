using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.Staff
{
    public class SurveyModel
    {
        public class SurveyDetailsModel
        {
            public List<GetOption> SurveyOption { get; set; }
            public List<GetQuestion> SurveyQuestion { get; set; }
        }

        public class GetQuestion
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public string FormInputName { get; set; }
            public string QuestionOrderId { get; set; }
            public string QuestionsTypeOrderId { get; set; }
            public string QuestionsTypeName { get; set; }
            public string AnswerName { get; set; }
            public string ParentId { get; set; }
        }

        public class GetOption
        {
            public Int64 Id { get; set; }
            public string Name { get; set; }
            public string QuestionOrderId { get; set; }
            public string DisplayName { get; set; }

        }

        public class ClubInitialModel
        {
            public List<SurveyLocationsModel> ClubList { get; set; }
        }
        public class SurveyLocationsModel
        {
            public string location_code { get; set; }
            public string club_name { get; set; }
        }

        public class SurveyQLA
        {
            public SurveyQLADetails SurveyObjDetails { get; set; }
        }

        public class SurveyQLADetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public int ContactId { get; set; }
            public string hs_object_id { get; set; }
            public string ClubNumber { get; set; }
            public string Createdby { get; set; }
            public List<QLADetailsModel> QLAList { get; set; }
        }
        public class QLADetailsModel
        {
            public string QuestionId { get; set; }
            public string OptionId { get; set; }
            public dynamic AnswerName { get; set; }
            public string QuestionsTypeName { get; set; }
            //public List<OtherAnswerDetails> OtherAnswerName { get; set; }
            public string SalesPerson { get; set; }
        }

        public class OtherAnswerDetails
        {
            public string FriendsName { get; set; }
            public string FriendsPhoneNo { get; set; }
        }

        public class SurveyResponseModel
        {
            public SurveyResponseDetailsModel InsertSurveyModel { get; set; }
        }
        public class SurveyResponseDetailsModel
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }

    }
}
