using Kiosk.Business.Model.Amenities;
using Kiosk.Domain.Data;
using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosk.Business.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data;
using Kiosk.Business.Model.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Kiosk.Repositories
{
    public partial class AmenitiesRepository : BaseRepository<Club>, IAmenitiesRepository
    {
        protected KioskContext _context;
        protected Logger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AmenitiesRepository(KioskContext context) : base(context)
        {
            _context = context;
        }
        public Task<TotspotpickelballFlag> GetTotspotandPickleballflag(int clubnumber)
        {
            TotspotpickelballFlag flags = new TotspotpickelballFlag();
            try
            {
                flags = _context.GetAmenitiesFlag
                       .FromSql($"[Kiosk].[usp_GetPickleball_Totspot_ClubDetails] {clubnumber}")
                       .AsEnumerable().FirstOrDefault();

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetTotspotandPickleballflag Method from AmenitiesRepository" + ex.ToString());
            }
            return Task.FromResult<TotspotpickelballFlag>(flags);
        }

        #region Pickleball
        public async Task<List<EquipmentModel>> GetEquipmentDetails(EquipmentRequestModel PostData)
        {
            List<EquipmentModel> equipmentList = new List<EquipmentModel>();

            try
            {
                equipmentList = Context.Equipment.Where(x => x.MemberType == PostData.MemberType).Select(e => new EquipmentModel
                {
                    EquipmentId = e.EquipmentId,
                    Name = e.Name,
                    Price = e.Price,
                    FullName = e.Name,
                }).ToList();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log("GetEquipmentDetails Method from AmenitiesRepository" + ex.ToString());
            }
            return await Task.FromResult<List<EquipmentModel>>(equipmentList);
        }

        public async Task<int> InsertPickleballLead(ContactModel PostData)
        {
            int LeadId = 0;
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "[Kiosk].[usp_InsertPickleballLead]";
            command.CommandType = CommandType.StoredProcedure;

            var Data = command.CreateParameter(); Data.ParameterName = "@Username"; Data.Value = ""; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@FirstName"; Data.Value = PostData.FirstName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@LastName"; Data.Value = PostData.LastName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@Email"; Data.Value = PostData.Email; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@PhoneNumber"; Data.Value = PostData.PhoneNumber; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@Gender"; Data.Value = PostData.Gender; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@DOB"; Data.Value = PostData.DOB; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@EquipmentId"; Data.Value = PostData.EquipmentId; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@ClubNumber"; Data.Value = PostData.ClubNumber; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@SourceName"; Data.Value = PostData.SourceName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@MemberType"; Data.Value = PostData.MemberType; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@MemberId"; Data.Value = PostData.MemberId; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@GuestType"; Data.Value = PostData.GuestType; command.Parameters.Add(Data);

            var reader = await command.ExecuteReaderAsync();
            try
            {
                while (reader.Read())
                {
                    LeadId = !DBNull.Value.Equals(reader["LeadId"]) ? Convert.ToInt32(reader["LeadId"]) : 0;
                }
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await connection.CloseAsync();
            }
            return LeadId;
        }


        #endregion Pickleball

        #region BabySitting
        public async Task<string> ChildCarePlanCheckOut(ChildCarePlanDetails PostData)
        {
            string Message = string.Empty;
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "[Kiosk].[usp_InsertMemberChildCarePlanDetails]";
            command.CommandType = CommandType.StoredProcedure;

            var Data = command.CreateParameter(); Data.ParameterName = "@Username"; Data.Value = "" + "(kiosk)"; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@FirstName"; Data.Value = PostData.FirstName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@LastName"; Data.Value = PostData.LastName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@PhoneNumber"; Data.Value = PostData.PhoneNumber; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@Email"; Data.Value = PostData.Email; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@Gender"; Data.Value = PostData.Gender; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@DOB"; Data.Value = PostData.DOB; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@PlanName"; Data.Value = PostData.PlanName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@PlanPrice"; Data.Value = PostData.PlanPrice; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@ClubNumber"; Data.Value = PostData.ClubNumber; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@MemberId"; Data.Value = PostData.MemberId; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@SourceName"; Data.Value = PostData.SourceName; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@MemberType"; Data.Value = PostData.MemberType; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@SalesPersonEmployeeNumber"; Data.Value = PostData.salesPersonObj == null ? null : PostData.salesPersonObj.paychex_id; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@SalesPersonName"; Data.Value = PostData.salesPersonObj == null ? null : PostData.salesPersonObj.first_name + ' ' + PostData.salesPersonObj.last_name; command.Parameters.Add(Data);
            Data = command.CreateParameter(); Data.ParameterName = "@GuestType"; Data.Value = PostData.GuestType; command.Parameters.Add(Data);

            var reader = await command.ExecuteReaderAsync();
            try
            {
                Message = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await reader.CloseAsync();
            }

            return Message;
        }
        #endregion
    }
}
