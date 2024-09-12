using Kiosk.Business.Model.FreshEmail;
using Kiosk.Business.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Services
{
    public interface IFreshEmailService : IBaseService<FreshEmailResponse>
    {
        FreshEmailResponse ValidateEmail(string email);
    }
}
