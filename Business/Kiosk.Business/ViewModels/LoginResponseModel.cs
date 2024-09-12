using Kiosk.Business.ViewModels.Account;
using System.ComponentModel.DataAnnotations;

namespace Kiosk.Business.ViewModels
{
    public partial class  LoginResponseModel
    {        
        public ApplicationUser ApplicationUser { get; set; }

        public string Token { get; set; }        
    }
}