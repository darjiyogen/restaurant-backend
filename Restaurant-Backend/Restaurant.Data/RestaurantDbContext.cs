using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
        {
        }
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(e =>
            {
                e.HasKey(x => x.CustomerId);
            });

            builder.Entity<RestaurantTable>(e =>
            {
                e.HasKey(x => x.TableId);
            });

            builder.Entity<Reservation>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne<Customer>(s => s.Customer)
                  .WithMany(g => g.Reservations)
                  .HasForeignKey(s => s.CustomerId);

                e.HasOne<RestaurantTable>(s => s.Table)
                  .WithMany(g => g.Reservations)
                  .HasForeignKey(s => s.TableId);

            });

        }
    }
}
