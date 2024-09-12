using Kiosk.Business.Enums.General;

namespace Kiosk.Business.ViewModels.General
{
    public partial class  ResponseDetail<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }

        public Error Error { get; set; }

        public DropMessageType MessageType { get; set; }
    }

    public partial class  Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public partial class  ErrorCode
    {
        public static string UNAUTHORIZED;
        public static string BAD_REQUEST;
        public static string NO_RESPONSE;
        public static string VALIDATION_FAILED;
        public static string SERVICE_EXECUTION_FAILED;
    }
}