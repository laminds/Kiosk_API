using Kiosk.Business.Enums.General;

namespace Kiosk.Business.ViewModels.General
{
    public partial class  DropMessageModel
    {
        public string Message { get; set; }

        public string Description { get; set; }

        public DropMessageType DropMessageType { get; set; }
    }
}