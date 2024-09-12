using Kiosk.Business.Model.JwtObj;
using Kiosk.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Newtonsoft.Json;
using NLog;
using System.Threading.Tasks;

namespace Kiosk.API.Controllers
{
    //[Authorize]
    [EnableCors("AllowMyOrigin")]
    public class ClubController : BaseApiController
    {
        private readonly IHtmlLocalizer<ClubController> _localizer;
        private readonly IClubService _clubService;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubService clubService, IHtmlLocalizer<ClubController> localizer, IHttpContextAccessor httpContextAccessor)
        {
            _clubService = clubService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetClubList")]
        public async Task<object> GetClubInfo()
        {
            var User = _httpContextAccessor.HttpContext.Session.GetString("UserSession");
            var UserObj = JsonConvert.DeserializeObject<JwtAuthModel>(User);
        
            return await GetDataWithMessage(async () =>
            {
                var result = await _clubService.GetClubInfo(null, false, UserObj.EmployeeNumber);
                return Response(result, string.Empty);
            });
        }

        [HttpGet]
        [Route("GetClubStationsByClub")]
        public async Task<object> GetClubStationsByClub(int clubNumber)
        {
            return await GetDataWithMessage(async () =>
            {
                var result = await _clubService.GetClubStationsByClub(clubNumber);
                return Response(result, string.Empty);
            });
        }

    }
}
