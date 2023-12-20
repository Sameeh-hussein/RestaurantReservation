using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext: DbContext
    {
        public DbSet<Restaurant> restaurants {  get; set; }
        public DbSet<Reservation> reservations { get; set;}
        public DbSet<Customer> customers { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Table> tables { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSetting.json")
                .Build();

            var connectionString = configuration.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Ignore(o => o.totalAmount)
                .HasKey(x => x.orderId);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.reservation)
                .WithMany(r => r.orders)
                .HasForeignKey(t => t.reservationId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.employee)
                .WithMany(e => e.orders)
                .HasForeignKey(o => o.employeeId);

            modelBuilder.Entity<MenuItems>()
                .HasKey(x => x.menuItemId);

            modelBuilder.Entity<MenuItems>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.menuItems)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<Customer>()
                .HasKey(x => x.customerId);

            modelBuilder.Entity<Employee>()
                .HasKey(x => x.employeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.employees)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<OrderItems>()
                .HasKey(x => x.orderItemId);

            modelBuilder.Entity<OrderItems>()
                .HasOne(t => t.Order)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(t => t.orderId);

            modelBuilder.Entity<OrderItems>()
                .HasOne(t => t.menuItems)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(t => t.menuItemId);

            modelBuilder.Entity<Reservation>()
                .HasKey(x => x.reservationId);

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.restaurant)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.customer)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.customerID);

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.table)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.tableId);

            modelBuilder.Entity<Restaurant>()
                .HasKey(x => x.RestaurantId);

            modelBuilder.Entity<Table>()
                .HasKey(x => x.tableId);

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Restaurant)           
                .WithMany(r => r.tables)             
                .HasForeignKey(t => t.restaurantId);

            var cascadeDeleteFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var FK in cascadeDeleteFKs)
                FK.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}
