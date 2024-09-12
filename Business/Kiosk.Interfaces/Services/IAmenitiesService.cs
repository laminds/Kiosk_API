using Kiosk.Business.Model.Amenities;
using Kiosk.Business.Model.Search;
using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Services
{
    public interface IAmenitiesService : IBaseService<Club>
    {
        Task<TotspotpickelballFlag> GetTotspotandPickleballflag(int clubNumber);

        #region Pickleball
        Task<List<EquipmentModel>> GetEquipmentDetails(EquipmentRequestModel PostData);
        Task<int> InsertPickleballLead(ContactModel PostData);
        #endregion Pickleball

        #region BabySitting
        Task<object> ChildCarePlanCheckOut(ChildCarePlanDetails postdata);
        #endregion
    }
}
