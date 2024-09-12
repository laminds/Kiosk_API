using AutoMapper;
using Kiosk.Business.Helpers;
using Kiosk.Domain;
using Kiosk.Domain.Data;
using Kiosk.Interfaces.Repositories;
using Kiosk.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace Kiosk.UoW
{
    public partial class  UnitOfWork : IUnitOfWork
    {
        private readonly KioskContext Context;
        private readonly IMapper _mapper;

        public UnitOfWork(KioskContext context, IMapper mapper)
        {
            this.Context = context;
            this._mapper = mapper;
            ClubRepository = new ClubRepository(Context);
            SearchRepository = new SearchRepository(Context);
            PlanRepository = new PlanRepository(Context);
            GuestRepository = new GuestRepository(Context);
            MemberRepository = new MemberRepository(Context);
            SilverSneakersRespository = new SilverSneakersRepository(Context);
            AmenitiesRepository = new AmenitiesRepository(Context);
            ManageMembershipRepository = new ManageMembershipRepository(Context);
            StaffRepository = new StaffRepository(Context);
            JiraTicketRepository = new JiraTicketRepository(Context);
            SaveWorkFlowRepository = new SaveWorkFlowRepository(Context);
        }
        public IClubRepository ClubRepository { get; }
        public ISearchRepository SearchRepository { get; }
        public IPlanRepository PlanRepository { get; }
        public IGuestRepository GuestRepository { get; }
        public IMemberRepository MemberRepository { get; }
        public ISilverSneakersRespository SilverSneakersRespository { get; }
        public IAmenitiesRepository AmenitiesRepository { get; }
        public IManageMembershipRepository ManageMembershipRepository { get; }
        public IStaffRepository StaffRepository { get; }
        public IJiraTicketRepository JiraTicketRepository { get; }
        public ISaveWorkFlowRepository SaveWorkFlowRepository { get; }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                Context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}