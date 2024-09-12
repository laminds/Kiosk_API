using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Business.Model.HubSpot
{
    public class AppointmentModel
    {
        public string appointment_type { get; set; }
        public string appt_date { get; set; }
        public string appt_status { get; set; }
    }

    public class AppointmentRequest
    {
        public List<AppointmentRequestFilterGroup> filterGroups { get; set; }
        public List<string> properties { get; set; }
    }
    public class AppointmentRequestFilterGroup
    {
        public List<AppointmentRequestFilter> filters { get; set; }
    }

    public class AppointmentRequestFilter
    {
        public string propertyName { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }

    public class AppointmentIdResult
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class AppointmentIdResponse
    {
        public List<AppointmentIdResult> results { get; set; }
    }
    public class AppointmentDetailResponse
    {
        public int total { get; set; }
        public List<AppointmentDetailResponseResult> results { get; set; }
    }
    public class AppointmentDetailResponseResult
    {
        public string id { get; set; }
        public AppointmentDetailResponseProperties properties { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool archived { get; set; }
    }

    public class AppointmentDetailResponseProperties
    {
        public string appointment_date { get; set; }
        public string appointment_status { get; set; }
        public string appointment_subject { get; set; }
        public string appointment_time { get; set; }
        public string appointment_tour_with { get; set; }
        public DateTime hs_createdate { get; set; }
        public DateTime hs_lastmodifieddate { get; set; }
        public string hs_object_id { get; set; }
    }
    public class UpdateApptStatusRequest
    {
        public UpdateApptStatusRequestProperties properties { get; set; }
    }
    public class UpdateApptStatusRequestProperties
    {
        public string appointment_status { get; set; }
    }
    public class UpdateApptStatusResponse
    {
        public string id { get; set; }
        public UpdateApptStatusResponseProperties properties { get; set; }
        public Nullable<DateTime> createdAt { get; set; }
        public Nullable<DateTime> updatedAt { get; set; }
        public bool archived { get; set; }
    }
    public class UpdateApptStatusResponseProperties
    {
        public string appointment_status { get; set; }
        public string hs_created_by_user_id { get; set; }
        public DateTime hs_createdate { get; set; }
        public DateTime hs_lastmodifieddate { get; set; }
        public string hs_object_id { get; set; }
        public string hs_updated_by_user_id { get; set; }
    }
}
