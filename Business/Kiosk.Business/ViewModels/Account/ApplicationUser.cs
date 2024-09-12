using Microsoft.AspNetCore.Identity;

namespace Kiosk.Business.ViewModels.Account
{
    public partial class  ApplicationUser : IdentityUser
    {
        public System.Guid? PasswordReset { get; set; }
        public System.DateTime? PasswordResetDate { get; set; }
    }
}