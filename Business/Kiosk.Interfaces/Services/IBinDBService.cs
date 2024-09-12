using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Services
{
    public interface IBinDBService
    {
        Task<JObject> LookupBin(string apiKey);
    }
}
