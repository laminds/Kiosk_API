using AutoMapper;
using Kiosk.Domain.Data;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Services;
using Kiosk.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Services
{
    public partial class ClubService : ServiceBase, IClubService
    {
        public ClubService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<List<usp_GetLocationsByEmail_Result>> GetClubInfo(string email, bool IsAdminUser, string employeeNumber)
        {
            var dataList = mapper.Map<List<usp_GetLocationsByEmail_Result>>(await unitOfWork.ClubRepository.GetClubList(email, IsAdminUser, employeeNumber));
            return dataList.ToList();
        }

        public async Task<usp_GetClubStationsByClub_Result> GetClubStationsByClub(int clubNumber)
        {
            var clubStationId = await unitOfWork.ClubRepository.GetClubStationsByClub(clubNumber);
            return clubStationId;
        }

    }
}