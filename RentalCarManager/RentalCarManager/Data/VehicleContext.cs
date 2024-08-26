using Microsoft.EntityFrameworkCore;
using RentalCarManager.Models;

namespace RentalCarManager.Data
{
    internal class VehicleContext(DbContextOptions<VehicleContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
           .HasDiscriminator<string>("VehicleType")
           .HasValue<Vehicle>(nameof(Vehicle))
           .HasValue<Truck>(nameof(Truck))
           .HasValue<Bus>(nameof(Bus))
           .HasValue<Car>(nameof(Car));
            modelBuilder.Entity<Vehicle>().OwnsOne(v => v.Customer);
            modelBuilder.Entity<Vehicle>().OwnsOne(v => v.AC);
            modelBuilder.Entity<Vehicle>().OwnsOne(v => v.Radio);
        }
    }
}
