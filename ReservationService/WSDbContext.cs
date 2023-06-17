using BloodBankLibrary.Core.Accomodations;
using Microsoft.EntityFrameworkCore;

namespace Settings
{
    public class WSDbContext : DbContext
    {
        public DbSet<ReservationBE> Reservations { get; set; }

        public WSDbContext(DbContextOptions<WSDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
