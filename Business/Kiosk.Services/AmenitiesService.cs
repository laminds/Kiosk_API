using AutoMapper;
using Kiosk.Business.Model.Amenities;
using Kiosk.Business.Model.Search;
using Kiosk.Domain.Models.SPModel;
using Kiosk.Interfaces.Services;
using Kiosk.Services.HubSpot;
using Kiosk.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Services
{
    public  class AmenitiesService : ServiceBase, IAmenitiesService
    {

        private readonly IHubSpotService _hubSpotService;

        public AmenitiesService(IUnitOfWork unitOfWork, IMapper mapper, IHubSpotService hubSpotService) : base(unitOfWork, mapper)
        {
            _hubSpotService = hubSpotService;
        }
        public async Task<TotspotpickelballFlag> GetTotspotandPickleballflag(int clubnumber)
        {
            var dataList = mapper.Map<TotspotpickelballFlag>(await unitOfWork.AmenitiesRepository.GetTotspotandPickleballflag(clubnumber));
            return dataList;
        }

        #region Pickleball
        public async Task<List<EquipmentModel>> GetEquipmentDetails(EquipmentRequestModel PostData)
        {
            var plans = mapper.Map<List<EquipmentModel>>(await unitOfWork.AmenitiesRepository.GetEquipmentDetails(PostData));

            return plans;
        }

        public async Task<int> InsertPickleballLead(ContactModel PostData)
        {
            int ContactId = 0;
            var plans = 0;
            ContactId = await _hubSpotService.InsertUpdateContact(PostData);
            if(ContactId != 0)
            {
                plans = mapper.Map<int>(await unitOfWork.AmenitiesRepository.InsertPickleballLead(PostData));
            }
            return plans;
        }
        #endregion Pickleball

        #region BabySitting
        public async Task<object> ChildCarePlanCheckOut(ChildCarePlanDetails postdata)
        {
            var CreditCardNumber = await unitOfWork.AmenitiesRepository.ChildCarePlanCheckOut(postdata);
            return await Task.FromResult(CreditCardNumber);
        }

        #endregion
    }
}
