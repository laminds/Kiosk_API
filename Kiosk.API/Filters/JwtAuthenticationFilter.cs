using Kiosk.Business.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace AAT.API.Filters
{
    [AttributeUsage(AttributeTargets.Class )]
    public partial class JwtAuthenticationFilter : Attribute, IAuthorizationFilter, IActionFilter
    {
        public static PersonModel ApplicationUserApiRequest { get; set; }

        internal static class AsyncHelper
        {
            private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None,
                                                                              TaskContinuationOptions.None, TaskScheduler.Default);

            public static void RunSync(Func<Task> func)
            {
                TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
            }

            public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            {
                return TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                //const string issuer = "http://identity.youfit.com/adfs/services/trust";
                //const string audience = "microsoft:identityserver:758ac6c5-d1d8-4f12-ae82-463f5c3ffad8";
                //const string adfsWellKnown = "https://identity.youfit.com/adfs/.well-known/openid-configuration";

                //const string testToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Inlja1ZVUlhDS09HZnBoZG9WSWR2Ym1QbmJsWSJ9.eyJhdWQiOiJtaWNyb3NvZnQ6aWRlbnRpdHlzZXJ2ZXI6NzU4YWM2YzUtZDFkOC00ZjEyLWFlODItNDYzZjVjM2ZmYWQ4IiwiaXNzIjoiaHR0cDovL2lkZW50aXR5LnlvdWZpdC5jb20vYWRmcy9zZXJ2aWNlcy90cnVzdCIsImlhdCI6MTcwOTg5NzA4MSwiZXhwIjoxNzA5OTAwNjgxLCJlbWFpbCI6Imhnb3BhbmlAeW91Zml0LmNvbSIsImdpdmVuX25hbWUiOiJoaXRlc2giLCJjb21tb25uYW1lIjoiSGl0ZXNoIiwicHBpZCI6IjEwMDk5OSIsImZhbWlseV9uYW1lIjoiR29wYW5pIiwic3ViIjoiWU9VRklUXFxoaXRlc2giLCJhcHB0eXBlIjoiUHVibGljIiwiYXBwaWQiOiI3NThhYzZjNS1kMWQ4LTRmMTItYWU4Mi00NjNmNWMzZmZhZDgiLCJhdXRobWV0aG9kIjoidXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFjOmNsYXNzZXM6UGFzc3dvcmRQcm90ZWN0ZWRUcmFuc3BvcnQiLCJhdXRoX3RpbWUiOiIyMDI0LTAzLTA4VDA4OjU0OjU5LjQwNloiLCJ2ZXIiOiIxLjAiLCJzY3AiOiJwcm9maWxlIG9wZW5pZCJ9.fktQfmsdkbgXoST3OdZxWsUDoqWcZOAzbKsPKaH7t6Txehdfnq0kM8svlHQZm0UEFxzh1SuTNRvrSg1I9rFY4SuOmHfYXLzPsWTDM0O-7LJwAlzZ7UiZf3LyvPLORxi9sl4j0dPYdFmuODzlgAXhMFF-H0H_N3vbtOhTpjMWZfzElqbu6KXjz4JbVrUmy-Obln8m_yYcliuXqifvuTTsCRzAb2xiaJnTRN-yKkbWpRN3GMgaKS3B2ZOU92Jd60xrB4T0Y4N2J75mOC5ebCEs2DQxqTzJn_LJYQopoirnhDLqLMD7GEkohdiKMhO2ZaJdbNlj07ViS5qx8UXCih3aUg";

                //IdentityModelEventSource.ShowPII = true;

                //try
                //{
                //    // Download the OIDC configuration which contains the JWKS
                //    // NB!!: Downloading this takes time, so do not do it very time you need to validate a token, Try and do it only once in the lifetime
                //    //     of your application!!

                //    IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>
                //        (adfsWellKnown, new OpenIdConnectConfigurationRetriever());
                //    OpenIdConnectConfiguration openIdConfig = AsyncHelper.RunSync(async () => await configurationManager.GetConfigurationAsync(CancellationToken.None));

                //    // Configure the TokenValidationParameters. Assign the SigningKeys which were downloaded from ADFS. 
                //    // Also set the Issuer and Audience to validate

                //    TokenValidationParameters validationParameters =
                //        new TokenValidationParameters
                //        {
                //            ValidIssuer = issuer,
                //            ValidAudience = audience,
                //            IssuerSigningKeys = openIdConfig.SigningKeys
                //        };

                //    // Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method

                //    SecurityToken validatedToken;
                //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                //    var user = handler.ValidateToken(testToken, validationParameters, out validatedToken);

                //    Console.WriteLine($"Token is validated.\n");

                //    // The ValidateToken method above will return a ClaimsPrincipal. 
                //    // Enumerate the ClaimsPrincipal

                //    if (null != user)
                //    {
                //        foreach (Claim claim in user.Claims)
                //        {
                //            Console.WriteLine(claim.Type + "  " + claim.Value);
                //        }
                //    }

                //    // Display the JSON

                //    Console.WriteLine("\n" + validatedToken.ToString());

                //}

                //catch (Exception e)
                //{
                //    Console.WriteLine($"Error occurred while validating token: {e.Message}");
                //}
                var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
                ApplicationUserApiRequest = null;
                if (IsAuthenticated)
                {
                    var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
                    ApplicationUserApiRequest = new PersonModel
                    {
                        Id = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value),
                        Name = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Name")?.Value
                    };
                }
            }
            catch (Exception)
            {
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (ApplicationUserApiRequest != null)
                {
                    dynamic valuesCntlr = context.Controller as dynamic;
                    valuesCntlr.ApplicationUserApiRequest = ApplicationUserApiRequest;
                }
            }
            catch (Exception)
            {
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
