using bhs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhs.Data
{
    public class BhsDbContext : DbContext
    {
        public BhsDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seller = new Seller
            {
                Code = 1,
                Name = "João",
                Cpf = "77266059879",
                Email = "joao@bhs.com.br"
            };

            var vehicles = new List<Vehicle>()
            {
                new Vehicle
                {
                    Code = 1,
                    Brand = "Fiat",
                    Model = "Fiat Uno 2011",
                    YearOfManufacture = 2011
                },
                new Vehicle
                {
                    Code = 2,
                    Brand = "Ford",
                    Model = "Ford Ka 2021",
                    YearOfManufacture = 2021
                }
            };

            var sales = new Sale
            {
                Id = 1,
                SellerCode = seller.Code,
                Status = SaleStatus.WaitingPayment,
            };

            modelBuilder.Entity<Seller>().HasData(seller);
            modelBuilder.Entity<Vehicle>().HasData(vehicles);
            modelBuilder.Entity<Sale>().HasData(sales);

            modelBuilder.Entity<Sale>()
                .HasMany(sale => sale.Vehicles)
                .WithMany(vehicle => vehicle.Sales)
                .UsingEntity(entity => {
                    entity.HasData(new { SalesId = 1, VehiclesCode = 1 });
                    entity.HasData(new { SalesId = 1, VehiclesCode = 2 });
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
