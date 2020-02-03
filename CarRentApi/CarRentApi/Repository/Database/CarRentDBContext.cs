using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CarRentApi.Repositories.Database
{
    public class CarRentDBContext:DbContext
    {
        public CarRentDBContext(DbContextOptions<CarRentDBContext> options) : base(options)
        {

        }

        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne<CarClass>().WithMany();
                c.HasMany<Reservation>().WithOne().HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.Cascade);
                c.HasOne<CarType>().WithMany();
                c.HasOne<CarBrand>().WithMany();
            });
            builder.Entity<Customer>(cu =>
            {
                cu.HasKey(y => y.Id);
                cu.HasMany<Reservation>().WithOne().HasForeignKey(rcu => rcu.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Reservation>(con =>
            {
                con.HasKey(z => z.Id);
                con.HasMany<Contract>().WithOne().HasForeignKey(cx => cx.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
