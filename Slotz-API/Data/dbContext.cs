using Microsoft.EntityFrameworkCore;
using Slotz_API.Models;

namespace Slotz_API.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<User> user { get; set; }
        public DbSet<Vehicle> vehicle { get; set; }
        public DbSet<Garage> garage { get; set; }
        public DbSet<SpaceGarage> spacegarage { get; set; }
        public DbSet<AvailablePeriod> availableperiod { get; set; }
        public DbSet<ServicePeriod> serviceperiod { get; set; }
        public DbSet<Offer> offer { get; set; }
    }
}
