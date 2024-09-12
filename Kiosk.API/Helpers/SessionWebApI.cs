using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Kiosk.Business.Helpers;

namespace Kiosk.API.Helpers
{
    public class SessionWebApI : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionWebApI(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string UserSessionExist = _httpContextAccessor.HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(UserSessionExist))
            {
                var IsClaim = SSOSession.SetSSOSession();
                if (IsClaim.result == 0)
                {
                    return;
                }
                else if (IsClaim.result == 2)
                {
                    string redirectUrl = AppSettings.TwoFactorAuthUrl;

                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary
                    {
                        { "action", "TwoFactorAuth" },
                        { "controller", "Home" },
                        { "area", null },
                        { "RedirectAuthUrl", redirectUrl + "&u=" + IsClaim.userId }
                    };
                    context.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    return;
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
