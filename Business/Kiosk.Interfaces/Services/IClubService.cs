using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Services
{
    public interface IClubService : IBaseService<Club>
    {
        Task<List<usp_GetLocationsByEmail_Result>> GetClubInfo(string email, bool IsAdminUser, string employeeNumber);
        Task<usp_GetClubStationsByClub_Result> GetClubStationsByClub(int clubNumber);
    }
}