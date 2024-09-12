using Kiosk.Business.ViewModels.CommonModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Kiosk.Business.Helpers;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Kiosk.Business.ViewModels.SPModels;

namespace Kiosk.API.Helpers
{
    public class SSOSession
    {
        private static readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public static SSOSessionResultModel SetSSOSession()
        {
            SessionModel UserSession = new SessionModel();
            SSOSessionResultModel resultModel = new SSOSessionResultModel();

            var identity = (System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            var UserName = identity.FindFirst(c => c.Type.Contains("nameidentifier"));
            var EmployeeIdClaim = identity.FindFirst(c => c.Type.Contains("privatepersonalidentifier"));
            var FirstName = identity.FindFirst(c => c.Type.Contains("CommonName"));
            var LastName = identity.FindFirst(c => c.Type.Contains("surname"));
            if (UserName != null || EmployeeIdClaim != null)
            {
                string FullName = "";
                //if (IsTwoFactorEnable(UserName) == 0)
                //{
                //    resultModel.result = 2;
                //    resultModel.userId = GetUserId(UserName);
                //    return resultModel;
                //}

                if (!string.IsNullOrEmpty(FirstName.Value) && !string.IsNullOrEmpty(LastName.Value))
                {
                    FullName = FirstName.Value + " " + LastName.Value;
                }
                else if (!string.IsNullOrEmpty(FirstName.Value) && string.IsNullOrEmpty(LastName.Value))
                {
                    FullName = FirstName.Value;
                }
                else if (string.IsNullOrEmpty(FirstName.Value) && !string.IsNullOrEmpty(LastName.Value))
                {
                    FullName = LastName.Value;
                }
                else if (string.IsNullOrEmpty(FirstName.Value) && string.IsNullOrEmpty(LastName.Value))
                {
                    FullName = UserName.Value.ToString().Replace("youfit\\", "");
                }
                UserSession.EmployeeNumber = EmployeeIdClaim.Value ?? null;
                UserSession.Name = FullName;
                UserSession.UserName = UserName.Value.ToString().ToLower().Replace("youfit\\", "");

                //var Equipmendata = IsEquipmentAccess(UserSession.UserName);
                //if (Equipmendata.Any())
                //{
                //    UserSession.IsEquipmentFrontAccess = Equipmendata.Where(x => x.Name == "EquipmentFront").Select(x => x.IsAccess).FirstOrDefault();
                //    UserSession.IsEquipmentAdminAccess = Equipmendata.Where(x => x.Name == "EquipmentAdmin").Select(x => x.IsAccess).FirstOrDefault();
                //    UserSession.IsEquipmentExternalVendor = Equipmendata.Where(x => x.Name == "EquipmentExternalVendor").Select(x => x.IsAccess).FirstOrDefault();

                //    UserSession.IsInspectionFrontAccess = Equipmendata.Where(x => x.Name == "InspectionFront").Select(x => x.IsAccess).FirstOrDefault();
                //    UserSession.IsInspectionAdminAccess = Equipmendata.Where(x => x.Name == "InspectionAdmin").Select(x => x.IsAccess).FirstOrDefault();
                //}
                //else
                //{
                //    UserSession.IsEquipmentFrontAccess = false;
                //    UserSession.IsEquipmentAdminAccess = false;
                //    UserSession.IsEquipmentExternalVendor = false;
                //    UserSession.IsInspectionFrontAccess = false;
                //    UserSession.IsInspectionAdminAccess = false;

                //}
                _httpContextAccessor.HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(UserSession));

                resultModel.result = 1;
                return resultModel;
            }
            else
            {
                resultModel.result = 0;
                return resultModel;
            }

        }

        public static int IsTwoFactorEnable(System.Security.Claims.Claim UserName)
        {
            var connectionstring = AppSettings.DefaultConnectionString;
            //string userName = UserName.Value.ToString().ToLower().Replace("youfit\\", "");
            Guid userGuid = GetUserId(UserName);
            var UserAgent = SSOSession.getUserAgent();
            var DeviceName = SSOSession.getMachineName();
            var PublicIpAddress = getPublicIpAddress();

            using (var conn = new SqlConnection(connectionstring))
            {
                var procedure = "[dbo].[CheckTwoFactorByUserName]";
                var parameters = new DynamicParameters();
                //parameters.Add("@UserName", userName, DbType.AnsiString);
                parameters.Add("@UserId", userGuid, DbType.Guid);
                parameters.Add("@UserAgent", UserAgent, DbType.AnsiString);
                parameters.Add("@PublicIpAddress", PublicIpAddress, DbType.AnsiString);
                parameters.Add("@Succeeded", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                conn.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@Succeeded");
            }

        }

        public static Guid GetUserId(System.Security.Claims.Claim UserName)
        {
            var connectionstring = AppSettings.KioskConnectionString;
            string userName = UserName.Value.ToString().ToLower().Replace("youfit\\", "");
            using (var conn = new SqlConnection(connectionstring))
            {
                return conn.Query<Guid>("Select Guid from Users where UserName=@UserName", new { @UserName = userName }).FirstOrDefault();
            }
        }

        public static List<usp_GetEquipmentModuleAccess_Result> IsEquipmentAccess(string UserName)
        {
            var connectionstring = AppSettings.DefaultConnectionString;
            using (var conn = new SqlConnection(connectionstring))
            {
                return conn.Query<usp_GetEquipmentModuleAccess_Result>("EXEC [OPS].[usp_GetEquipmentModuleAccess]", new { UserName = UserName },
        commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static string getLocalIpAddress()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var LocalIpAddress = request.HttpContext.Connection.LocalIpAddress.ToString();
            return LocalIpAddress;
        }

        public static string getPublicIpAddress()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
            return remoteIpAddress;
        }
        public static string getUserAgent()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var ddssdss = request.Headers["User-Agent"];
            var UserAgent = request.Headers["User-Agent"].ToString();
            return UserAgent;
        }
        public static string getMachineName()
        {
            return System.Environment.MachineName;
        }

    }
}
