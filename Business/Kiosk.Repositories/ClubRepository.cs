using Kiosk.Business.Helpers;
using Kiosk.Domain.Data;
using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using StandardExceptionLoggerExtention.ApplicationEnums;
//using StandardExceptionLoggerExtention.Dto;
//using StandardExceptionLoggerExtention.ExternalAccess;
//using StandardExceptionLoggerExtention.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Repositories
{
    public partial class ClubRepository : BaseRepository<Club>, IClubRepository
    {
        protected KioskContext _context;
        protected Logger _logger;
        private static Random random = new Random();
        public ClubRepository(KioskContext context) : base(context)
        {
            _context = context; 
        }
        public async Task<List<usp_GetLocationsByEmail_Result>> GetClubList(string email, bool isAdminUser, string employeeNumber)
        {
            List<usp_GetLocationsByEmail_Result> clubs = new List<usp_GetLocationsByEmail_Result>();
            try
            {
                clubs = await _context.GetClubs
                       .FromSql($"[Kiosk].[usp_GetLocationsByEmail] {null}, {isAdminUser}, {employeeNumber}")
                       .ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetClubList Method from ClubRepository" + ex.Message.ToString());
            }
            return clubs;
        }

        public Task<usp_GetClubStationsByClub_Result> GetClubStationsByClub(int ClubNumber)
        {
            usp_GetClubStationsByClub_Result clubStationId = new usp_GetClubStationsByClub_Result();
            try
            {
                clubStationId = _context.ClubStations
                          .FromSql($"[Kiosk].[usp_GetClubStationsByClub] {ClubNumber}")
                          .AsEnumerable()
                          .FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetClubStationsByClub Method from ClubRepository" + ex.Message.ToString());
            }

            return Task.FromResult<usp_GetClubStationsByClub_Result>(clubStationId);
        }

        public Task<List<ClubUserModel>> GetClubByUserList(string email, bool isAdminUser, string employeeNumber)
        {
            List<ClubUserModel> clubs = new List<ClubUserModel>();
            try
            {
                clubs = _context.GetClubsByUser
                       .FromSql($"[Kiosk].[usp_GetLocationsByUser] {null}, {isAdminUser}, {employeeNumber}")
                       .ToList();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetClubByUserList Method from ClubRepository" + ex.ToString());
            }
            return Task.FromResult<List<ClubUserModel>>(clubs);
        }

        public Task<List<ClubUserModel>> GetClubByUser(string username)
        {
            List<ClubUserModel> clubs = new List<ClubUserModel>();
            try
            {
                clubs = _context.GetClubUsers
                       .FromSql($"[Kiosk].[usp_GetClubBYUserName] {username}")
                       .ToList();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetClubByUser Method from ClubRepository" + ex.ToString());
            }
            return Task.FromResult<List<ClubUserModel>>(clubs);
        }
    }
}