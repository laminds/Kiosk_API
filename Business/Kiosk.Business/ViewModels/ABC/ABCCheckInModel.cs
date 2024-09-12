using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.ViewModels.ABC
{
    public class ABCCheckInModel
    {
        public string checkInId { get; set; }
        public DateTime checkInTimestamp { get; set; }
        public string stationName { get; set; }
        public string checkInMessage { get; set; }
        public string checkInStatus { get; set; }
        public CheckinMemberModel member { get; set; }
        public string Status { get; set; }
    }
    public class CheckinMemberModel
    {
        public string memberId { get; set; }
        public int homeClub { get; set; }
    }


    public class ResponseException
    {
        public ABCCheckInResponse Response { get; set; }
        public bool? StopRequest { get; set; }
        public bool? Reattempt { get; set; }
    }

    public class ABCCheckInResponse
    {
        public StatusModel status { get; set; }
        public RequestModel request { get; set; }
        public List<ABCCheckInModel> checkins { get; set; }
    }

    public class StatusModel
    {
        public string message { get; set; }
        public string count { get; set; }
    }

    public class RequestModel
    {
        public string clubNumber { get; set; }
        public string page { get; set; }
        public string size { get; set; }
        public string checkInTimestampRange { get; set; }
    }

    public class ABCCheckinsData
    {
        public string clubNumber { get; set; }
        public string memberId { get; set; }
        public Checkin[] checkins { get; set; }
    }

    public class Checkin
    {
        public CheckinAccess access { get; set; }
    }

    public class CheckinAccess
    {
        public string locationTimestamp { get; set; }
        public string allowed { get; set; }
        public string stationId { get; set; }
    }


}
