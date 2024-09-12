using AAT.API.Filters;
using Kiosk.API.Helpers;
using Kiosk.Business.Enums.General;
using Kiosk.Business.ViewModels.General;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
//using StandardExceptionLoggerExtention.ApplicationEnums;
//using StandardExceptionLoggerExtention.Dto;
//using StandardExceptionLoggerExtention.ExternalAccess;
//using StandardExceptionLoggerExtention.Repository;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
//using StandardExceptionLoggerExtention.ExternalAccess;
using Microsoft.AspNetCore.Authorization;

namespace Kiosk.API.Controllers
{
    //[JwtAuthenticationFilter]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(SessionWebApI))]
    public partial class BaseApiController : Controller
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected async Task<ResponseDetail<T>> GetDataWithMessage<T>(Func<Task<Tuple<T, string, DropMessageType>>> getDataFunc)
        {
            var output = new ResponseDetail<T>();
            try
            {
                var result = await getDataFunc();
                output.Data = result.Item1;
                output.Message = result.Item2;
                output.MessageType = result.Item3;
                return await Task.FromResult(output);
            }
            catch (Exception ex)
            {
                output.MessageType = DropMessageType.Error;
                output.Error = new Error
                {
                    Code = ErrorCode.SERVICE_EXECUTION_FAILED,
                    Message = ex.Message
                };
                output.Message = "Something went wrong!! Please Try again later";
                Logger.Error($"An error has occuerd on {Convert.ToString(ControllerContext.RouteData.Values["controller"]) + " controller &" + Convert.ToString(ControllerContext.RouteData.Values["action"]) + " Method"}Message:{ex.Message}");

                //ExternalExceptionLogger.LogException(ex, Convert.ToString(ControllerContext.RouteData.Values["controller"]), Convert.ToString(ControllerContext.RouteData.Values["action"]) + "Method", StandardExceptionLoggerExtention.ApplicationEnums.LogLevel.Error, StandardExceptionLoggerExtention.ApplicationEnums.ErrorType.Exception);

                return await Task.FromResult(output);
            }
        }
        protected new Tuple<T, string, DropMessageType> Response<T>(T data, string message, DropMessageType messageType = DropMessageType.Success)
        {
            return new Tuple<T, string, DropMessageType>(data, message, messageType);
        }
    }
}