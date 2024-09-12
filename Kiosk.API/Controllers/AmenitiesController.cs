using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using NLog;
using System.Threading.Tasks;
using Kiosk.Interfaces.Services;
using Kiosk.Business.Model.Amenities;
using Kiosk.Business.Model.Search;


namespace Kiosk.API.Controllers
{
    //[Authorize]
    [EnableCors("AllowMyOrigin")]
    public class AmenitiesController : BaseApiController
    {
        private readonly IAmenitiesService _amenitiesService;

        public AmenitiesController(IAmenitiesService amenitiesService)
        {
            _amenitiesService = amenitiesService;
        }

        [HttpGet]
        [Route("GetTotspotandPickleballflag")]
        public async Task<object> GetTotspotandPickleballflag(int clubNumber)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _amenitiesService.GetTotspotandPickleballflag(clubNumber);
                return Response(result , string.Empty);
            });
        }

        #region Pickleball
        [HttpPost]
        [Route("GetEquipmentDetails")]
        public async Task<object> GetEquipmentDetails(EquipmentRequestModel PostData)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _amenitiesService.GetEquipmentDetails(PostData);
                return Response(result, string.Empty);
            });
        }

        [HttpPost]
        [Route("InsertPickleballLead")]
        public async Task<object> InsertPickleballLead(ContactModel PostData)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _amenitiesService.InsertPickleballLead(PostData);
                return Response(result, string.Empty);
            });
        }
        #endregion Pickleball

        #region BabySitting
        [HttpPost]
        [Route("ChildCarePlanCheckOut")]
        public async Task<object> ChildCarePlanCheckOut(ChildCarePlanDetails postData)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _amenitiesService.ChildCarePlanCheckOut(postData);
                return Response(result, string.Empty);
            });
        }

        #endregion
    }
}
