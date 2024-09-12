using Kiosk.Business.Model.Amenities;
using Kiosk.Business.Model.Search;
using Kiosk.Domain.Models;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Interfaces.Repositories
{
    public interface IAmenitiesRepository : IBaseRepository<Club>
    {
        Task<TotspotpickelballFlag> GetTotspotandPickleballflag(int clubnumber);

        #region Pickleball
        Task<List<EquipmentModel>> GetEquipmentDetails(EquipmentRequestModel PostData);
        Task<int> InsertPickleballLead(ContactModel PostData);

        #endregion Pickleball

        #region BabySitting
        Task<string> ChildCarePlanCheckOut(ChildCarePlanDetails PostData);
        #endregion
    }
}