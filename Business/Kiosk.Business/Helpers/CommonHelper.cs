using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System;
using System.Net.Mail;
using UAParser;

namespace Kiosk.Business.Helpers
{
    public static class  CommonHelper
    {
        private static readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [Obsolete]
        public static string getBrowser()
        {
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c.UserAgent.Family;
        }
        public static string getPublicIpAddress()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        public static string getUserAgent()
        {
            return _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
        }
        public static string getLocalIpAddress()
        {
            //return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            string localIpAddr = string.Empty;
            string HostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                localIpAddr = ip4.ToString();
            }
            return localIpAddr;
        }
    }
}