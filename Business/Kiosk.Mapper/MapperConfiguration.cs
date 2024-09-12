using AutoMapper;
using Kiosk.Business.Model.Search;
using Kiosk.Business.Model.SilverSneaker;
using Kiosk.Business.ViewModels;
using Kiosk.Business.ViewModels.General;
using Kiosk.Domain;
using Kiosk.Domain.Models;

namespace Kiosk.Business.Helpers
{
    public partial class  MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<PersonModel, Person>();
            CreateMap<Person, PersonModel>();
            CreateMap<ContactModel, HubSpotResponseModel>();
            CreateMap<SilverSneaker, SilverSneakerDetail>();

        }
    }
}