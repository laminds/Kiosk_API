using Kiosk.Business.ViewModels.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kiosk.Domain.Models;

namespace Kiosk.Domain
{
    public partial class KioskContext : IdentityDbContext<ApplicationUser>
    {
        public KioskContext(DbContextOptions<KioskContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }

        public DbSet<NewMember>NewMember{ get; set; }


    }
}