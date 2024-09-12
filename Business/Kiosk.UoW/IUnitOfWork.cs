using Kiosk.Interfaces.Repositories;
using Kiosk.Interfaces.Services;
using Kiosk.Repositories;
using System;

namespace Kiosk.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IClubRepository ClubRepository { get; }
        ISearchRepository SearchRepository { get; }
        IPlanRepository PlanRepository { get; }
        IGuestRepository GuestRepository { get; }
        IMemberRepository MemberRepository { get; }
        ISilverSneakersRespository SilverSneakersRespository { get; }
        IAmenitiesRepository AmenitiesRepository { get; }
        IManageMembershipRepository ManageMembershipRepository { get; }
        IStaffRepository StaffRepository { get; }
        IJiraTicketRepository JiraTicketRepository { get; }
        ISaveWorkFlowRepository SaveWorkFlowRepository { get; }
    }
}