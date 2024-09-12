using Kiosk.Business.Helpers;
using Kiosk.Domain.Models.SPModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Domain.Data
{
    public partial class PrdPlanContext : DbContext
    {
        public PrdPlanContext()
        {
        }

        public PrdPlanContext(DbContextOptions<PrdPlanContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(AppSettings.PrdPlanConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
