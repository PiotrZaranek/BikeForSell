using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BikeForSell.Domain.Models;

namespace BikeForSell.Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<Drive> Drives { get; set; }
        public DbSet<Brake> Brakes { get; set; }
        public DbSet<Wheel> Wheels { get; set; }
        public DbSet<DetailInformation> DetailInformations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 1:1 Frame To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.Frame)
                .WithOne(b => b.Bike)
                .HasForeignKey<Frame>(k => k.BikeRef);

            // 1:1 Drive To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.Drive)
                .WithOne(b => b.Bike)
                .HasForeignKey<Drive>(k => k.BikeRef);

            // 1:1 Brake To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.Brake)
                .WithOne(b => b.Bike)
                .HasForeignKey<Brake>(k => k.BikeRef);

            // 1:1 Wheel To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.Wheel)
                .WithOne(b => b.Bike)
                .HasForeignKey<Wheel>(k => k.BikeRef);

            // 1:1 DetailInfromation To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.DetailInformation)
                .WithOne(b => b.Bike)
                .HasForeignKey<DetailInformation>(k => k.BikeRef);

            // 1:1 Transaction To Bike
            builder.Entity<Bike>()
                .HasOne(a => a.Transaction)
                .WithOne(b => b.Bike)
                .HasForeignKey<Transaction>(k => k.BikeRef);

            // 1:1 DetalInfromation To ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.DetailInformation)
                .WithOne(b => b.User)
                .HasForeignKey<DetailInformation>(k => k.UserRef);
        }
    }
}