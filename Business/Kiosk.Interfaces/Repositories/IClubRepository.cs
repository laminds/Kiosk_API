using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Repositories
{
    public interface IClubRepository : IBaseRepository<Club>
    {
        Task<List<usp_GetLocationsByEmail_Result>> GetClubList(string email, bool isAdminUser, string employeeNumber);
        Task<usp_GetClubStationsByClub_Result> GetClubStationsByClub(int ClubNumber);
        Task<List<ClubUserModel>> GetClubByUserList(string email, bool isAdminUser, string employeeNumber);
        Task<List<ClubUserModel>> GetClubByUser(string username);
    }
}
