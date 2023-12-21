using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext: DbContext
    {
        public DbSet<Restaurant> Restaurants {  get; set; }
        public DbSet<Reservation> Reservations { get; set;}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

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
                .Property(x => x.orderId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
                .HasOne(t => t.reservation)
                .WithMany(r => r.orders)
                .HasForeignKey(t => t.reservationId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.employee)
                .WithMany(e => e.orders)
                .HasForeignKey(o => o.employeeId);

            modelBuilder.Entity<Order>()
                .HasData(SeedOrdersData());



            modelBuilder.Entity<Customer>()
                .HasKey(x => x.customerId);

            modelBuilder.Entity<Customer>()
                .Property(x => x.customerId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Customer>()
                .HasData(SeedCustomersData());



            modelBuilder.Entity<MenuItems>()
                .HasKey(x => x.menuItemId);

            modelBuilder.Entity<MenuItems>()
                .Property(x => x.menuItemId)
                .ValueGeneratedNever();

            modelBuilder.Entity<MenuItems>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.menuItems)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<MenuItems>()
                .HasData(SeedMenuItemsData());



            modelBuilder.Entity<Employee>()
                .HasKey(x => x.employeeId);

            modelBuilder.Entity<Employee>()
                .Property(x => x.employeeId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.employees)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<Employee>()
                .HasData(SeedEmployeesData());



            modelBuilder.Entity<OrderItems>()
                .HasKey(x => x.orderItemId);

            modelBuilder.Entity<OrderItems>()
                .Property(x => x.orderItemId)
                .ValueGeneratedNever();

            modelBuilder.Entity<OrderItems>()
                .HasOne(t => t.Order)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(t => t.orderId);

            modelBuilder.Entity<OrderItems>()
                .HasOne(t => t.menuItems)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(t => t.menuItemId);

            modelBuilder.Entity<OrderItems>()
                .HasData(SeedOrderItemsData());



            modelBuilder.Entity<Reservation>()
                .HasKey(x => x.reservationId);

            modelBuilder.Entity<Reservation>()
                .Property(x => x.reservationId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.restaurant)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.customer)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.customerId);

            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.table)
                .WithMany(r => r.reservations)
                .HasForeignKey(t => t.tableId);

            modelBuilder.Entity<Reservation>()
                .HasData(SeedReservationsData());



            modelBuilder.Entity<Restaurant>()
                .HasKey(x => x.RestaurantId);

            modelBuilder.Entity<Restaurant>()
                .Property(x => x.RestaurantId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Restaurant>()
                .HasData(SeedRestaurabtsData());



            modelBuilder.Entity<Table>()
                .HasKey(x => x.tableId);

            modelBuilder.Entity<Table>()
                .Property(x => x.tableId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Restaurant)           
                .WithMany(r => r.tables)             
                .HasForeignKey(t => t.restaurantId);

            modelBuilder.Entity<Table>()
                .HasData(SeedTablesData());



            var cascadeDeleteFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var FK in cascadeDeleteFKs)
                FK.DeleteBehavior = DeleteBehavior.NoAction;
        }

        private List<Table> SeedTablesData()
        {
            return new List<Table>()
            {
                new Table { tableId = 1, restaurantId = 1, capacity = 4 },
                new Table { tableId = 2, restaurantId = 2, capacity = 2 },
                new Table { tableId = 3, restaurantId = 1, capacity = 6 },
                new Table { tableId = 4, restaurantId = 2, capacity = 4 },
                new Table { tableId = 5, restaurantId = 1, capacity = 5 }
            };
        }

        private List<Restaurant> SeedRestaurabtsData()
        {
            return new List<Restaurant>()
            {
                new Restaurant { RestaurantId = 1, name = "Mr Italian", address = "123 Main St.", phoneNumber = "555-123-4567", openingHours = "9:00 AM - 10:00 PM" },
                new Restaurant { RestaurantId = 2, name = "Meat Haven", address = "456 Manara St.", phoneNumber = "555-987-6543", openingHours = "10:00 AM - 9:00 PM" }
            };
        }

        private List<OrderItems> SeedOrderItemsData()
        {
            return new List<OrderItems>()
            {
                new OrderItems { orderItemId = 1, orderId = 1, menuItemId = 1, quantity = 2 },
                new OrderItems { orderItemId = 2, orderId = 1, menuItemId = 3, quantity = 1 },
                new OrderItems { orderItemId = 3, orderId = 2, menuItemId = 5, quantity = 3 },
                new OrderItems { orderItemId = 4, orderId = 2, menuItemId = 1, quantity = 1 },
                new OrderItems { orderItemId = 5, orderId = 3, menuItemId = 2, quantity = 1 },
                new OrderItems { orderItemId = 6, orderId = 3, menuItemId = 4, quantity = 1 },
                new OrderItems { orderItemId = 7, orderId = 4, menuItemId = 2, quantity = 1 },
                new OrderItems { orderItemId = 8, orderId = 5, menuItemId = 1, quantity = 1 },
                new OrderItems { orderItemId = 9, orderId = 6, menuItemId = 2, quantity = 2 },
                new OrderItems { orderItemId = 10, orderId = 7, menuItemId = 5, quantity = 1 },
                new OrderItems { orderItemId = 11, orderId = 8, menuItemId = 5, quantity = 1 }
            };
        }

        private List<Reservation> SeedReservationsData()
        {
            return new List<Reservation>()
            {
                new Reservation { reservationId = 1, customerId = 1, restaurantId = 1, tableId = 1, reservationDate = DateTime.Now.AddHours(1), partySize = 4 },
                new Reservation { reservationId = 2, customerId = 2, restaurantId = 2, tableId = 2, reservationDate = DateTime.Now.AddHours(1), partySize = 2 },
                new Reservation { reservationId = 3, customerId = 3, restaurantId = 1, tableId = 3, reservationDate = DateTime.Now.AddHours(3), partySize = 6 },
                new Reservation { reservationId = 4, customerId = 1, restaurantId = 2, tableId = 2, reservationDate = DateTime.Now.AddHours(4), partySize = 1 },
                new Reservation { reservationId = 5, customerId = 2, restaurantId = 1, tableId = 5, reservationDate = DateTime.Now.AddHours(2), partySize = 4 }
            };
        }

        private List<Employee> SeedEmployeesData()
        {
            return new List<Employee>()
            {
                new Employee { employeeId = 1, restaurantId = 1, firstName = "Alice", lastName = "Johnson", position = "Manager" },
                new Employee { employeeId = 2, restaurantId = 2, firstName = "Bob", lastName = "Smith", position = "Manager" },
                new Employee { employeeId = 3, restaurantId = 1, firstName = "Charlie", lastName = "Williams", position = "Waiter" },
                new Employee { employeeId = 4, restaurantId = 2, firstName = "David", lastName = "Brown", position = "Chef" },
                new Employee { employeeId = 5, restaurantId = 1, firstName = "Eva", lastName = "Davis", position = "Chef" },
                new Employee { employeeId = 6, restaurantId = 2, firstName = "John", lastName = "Davis", position = "Waiter" }
            };
        }

        private List<MenuItems> SeedMenuItemsData()
        {
            return new List<MenuItems>()
            {
                new MenuItems { menuItemId = 1, restaurantId = 1, name = "Spaghetti Bolognese", description = "Classic Italian pasta dish", price = 12.99m },
                new MenuItems { menuItemId = 2, restaurantId = 2, name = "Grilled Salmon", description = "Freshly grilled salmon with lemon butter sauce", price = 17.99m },
                new MenuItems { menuItemId = 3, restaurantId = 1, name = "Margherita Pizza", description = "Traditional Italian pizza with tomatoes and fresh mozzarella", price = 10.99m },
                new MenuItems { menuItemId = 4, restaurantId = 2, name = "Beef Steak", description = "Juicy beef steak cooked to perfection", price = 19.99m },
                new MenuItems { menuItemId = 5, restaurantId = 1, name = "Caesar Salad", description = "Crisp romaine lettuce, croutons, and Caesar dressing", price = 8.99m }
            }; 
        }

        private List<Customer> SeedCustomersData()
        {
            return new List<Customer>()
            {
                new Customer { customerId = 1, firstName = "John", lastName = "Doe", email = "john@example.com", phoneNumber = "123-456-7890" },
                new Customer { customerId = 2, firstName = "Jane", lastName = "Smith", email = "jane@example.com", phoneNumber = "987-654-3210" },
                new Customer { customerId = 3, firstName = "Michael", lastName = "Johnson", email = "michael@example.com", phoneNumber = "555-555-5555" },
                new Customer { customerId = 4, firstName = "Emily", lastName = "Williams", email = "emily@example.com", phoneNumber = "111-222-3333" },
                new Customer { customerId = 5, firstName = "William", lastName = "Brown", email = "william@example.com", phoneNumber = "444-444-4444" }
            };
        }

        private List<Order> SeedOrdersData()
        {
            return new List<Order>()
            {
                new Order { orderId = 1, reservationId = 1, employeeId = 5, orderDate = DateTime.Now, totalAmount = 36.97m  },
                new Order { orderId = 2, reservationId = 1, employeeId = 5, orderDate = DateTime.Now, totalAmount = 39.93m },
                new Order { orderId = 3, reservationId = 2, employeeId = 4, orderDate = DateTime.Now, totalAmount = 37.98m },
                new Order { orderId = 4, reservationId = 2, employeeId = 4, orderDate = DateTime.Now, totalAmount = 17.99m  },
                new Order { orderId = 5, reservationId = 3, employeeId = 5, orderDate = DateTime.Now, totalAmount = 12.99m  },
                new Order { orderId = 6, reservationId = 4, employeeId = 4, orderDate = DateTime.Now, totalAmount = 35.98m },
                new Order { orderId = 7, reservationId = 5, employeeId = 5, orderDate = DateTime.Now, totalAmount = 8.99m },
                new Order { orderId = 8, reservationId = 5, employeeId = 5, orderDate = DateTime.Now, totalAmount = 8.99m }
            };
        }

    }
}
