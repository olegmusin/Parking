using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingApp.Data.Domain.Entities;
using ParkingApp.Data.Domain.Identity;

namespace ParkingApp.Data.Domain
{
    public sealed class ParkingDbContext : IdentityDbContext<AppUser>
    {

        private readonly IConfigurationRoot _config;

        public ParkingDbContext(DbContextOptions<ParkingDbContext> options, 
                                IConfigurationRoot config)
            : base(options)
        {
            _config = config;
            Database.EnsureCreatedAsync().Wait();
        }


        public DbSet<Owner> Owners { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<ParkingSession> ParkingSessions { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["database:connection"]);
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }



    }
}
