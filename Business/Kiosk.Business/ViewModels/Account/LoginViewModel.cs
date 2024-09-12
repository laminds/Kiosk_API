using System.ComponentModel.DataAnnotations;

namespace Kiosk.Business.ViewModels.Account
{
    public partial class  LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}