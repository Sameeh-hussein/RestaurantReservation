﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    [DbContext(typeof(RestaurantReservationDbContext))]
    [Migration("20240202171539_add-AddApplicationUser")]
    partial class addAddApplicationUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customerId"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            customerId = 1,
                            email = "john@example.com",
                            firstName = "John",
                            lastName = "Doe",
                            phoneNumber = "123-456-7890"
                        },
                        new
                        {
                            customerId = 2,
                            email = "jane@example.com",
                            firstName = "Jane",
                            lastName = "Smith",
                            phoneNumber = "987-654-3210"
                        },
                        new
                        {
                            customerId = 3,
                            email = "michael@example.com",
                            firstName = "Michael",
                            lastName = "Johnson",
                            phoneNumber = "555-555-5555"
                        },
                        new
                        {
                            customerId = 4,
                            email = "emily@example.com",
                            firstName = "Emily",
                            lastName = "Williams",
                            phoneNumber = "111-222-3333"
                        },
                        new
                        {
                            customerId = 5,
                            email = "william@example.com",
                            firstName = "William",
                            lastName = "Brown",
                            phoneNumber = "444-444-4444"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeId"));

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("restaurantId")
                        .HasColumnType("int");

                    b.HasKey("employeeId");

                    b.HasIndex("restaurantId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            employeeId = 1,
                            firstName = "Alice",
                            lastName = "Johnson",
                            position = "Manager",
                            restaurantId = 1
                        },
                        new
                        {
                            employeeId = 2,
                            firstName = "Bob",
                            lastName = "Smith",
                            position = "Manager",
                            restaurantId = 2
                        },
                        new
                        {
                            employeeId = 3,
                            firstName = "Charlie",
                            lastName = "Williams",
                            position = "Waiter",
                            restaurantId = 1
                        },
                        new
                        {
                            employeeId = 4,
                            firstName = "David",
                            lastName = "Brown",
                            position = "Chef",
                            restaurantId = 2
                        },
                        new
                        {
                            employeeId = 5,
                            firstName = "Eva",
                            lastName = "Davis",
                            position = "Chef",
                            restaurantId = 1
                        },
                        new
                        {
                            employeeId = 6,
                            firstName = "John",
                            lastName = "Davis",
                            position = "Waiter",
                            restaurantId = 2
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItems", b =>
                {
                    b.Property<int>("menuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("menuItemId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("restaurantId")
                        .HasColumnType("int");

                    b.HasKey("menuItemId");

                    b.HasIndex("restaurantId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            menuItemId = 1,
                            description = "Classic Italian pasta dish",
                            name = "Spaghetti Bolognese",
                            price = 12.99m,
                            restaurantId = 1
                        },
                        new
                        {
                            menuItemId = 2,
                            description = "Freshly grilled salmon with lemon butter sauce",
                            name = "Grilled Salmon",
                            price = 17.99m,
                            restaurantId = 2
                        },
                        new
                        {
                            menuItemId = 3,
                            description = "Traditional Italian pizza with tomatoes and fresh mozzarella",
                            name = "Margherita Pizza",
                            price = 10.99m,
                            restaurantId = 1
                        },
                        new
                        {
                            menuItemId = 4,
                            description = "Juicy beef steak cooked to perfection",
                            name = "Beef Steak",
                            price = 19.99m,
                            restaurantId = 2
                        },
                        new
                        {
                            menuItemId = 5,
                            description = "Crisp romaine lettuce, croutons, and Caesar dressing",
                            name = "Caesar Salad",
                            price = 8.99m,
                            restaurantId = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"));

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("reservationId")
                        .HasColumnType("int");

                    b.HasKey("orderId");

                    b.HasIndex("employeeId");

                    b.HasIndex("reservationId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            orderId = 1,
                            employeeId = 5,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6635),
                            reservationId = 1
                        },
                        new
                        {
                            orderId = 2,
                            employeeId = 5,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6678),
                            reservationId = 1
                        },
                        new
                        {
                            orderId = 3,
                            employeeId = 4,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6681),
                            reservationId = 2
                        },
                        new
                        {
                            orderId = 4,
                            employeeId = 4,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6683),
                            reservationId = 2
                        },
                        new
                        {
                            orderId = 5,
                            employeeId = 5,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6685),
                            reservationId = 3
                        },
                        new
                        {
                            orderId = 6,
                            employeeId = 4,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6688),
                            reservationId = 4
                        },
                        new
                        {
                            orderId = 7,
                            employeeId = 5,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6690),
                            reservationId = 5
                        },
                        new
                        {
                            orderId = 8,
                            employeeId = 5,
                            orderDate = new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6692),
                            reservationId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItems", b =>
                {
                    b.Property<int>("orderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderItemId"));

                    b.Property<int>("menuItemId")
                        .HasColumnType("int");

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("orderItemId");

                    b.HasIndex("menuItemId");

                    b.HasIndex("orderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            orderItemId = 1,
                            menuItemId = 1,
                            orderId = 1,
                            quantity = 2
                        },
                        new
                        {
                            orderItemId = 2,
                            menuItemId = 3,
                            orderId = 1,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 3,
                            menuItemId = 5,
                            orderId = 2,
                            quantity = 3
                        },
                        new
                        {
                            orderItemId = 4,
                            menuItemId = 1,
                            orderId = 2,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 5,
                            menuItemId = 2,
                            orderId = 3,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 6,
                            menuItemId = 4,
                            orderId = 3,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 7,
                            menuItemId = 2,
                            orderId = 4,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 8,
                            menuItemId = 1,
                            orderId = 5,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 9,
                            menuItemId = 2,
                            orderId = 6,
                            quantity = 2
                        },
                        new
                        {
                            orderItemId = 10,
                            menuItemId = 5,
                            orderId = 7,
                            quantity = 1
                        },
                        new
                        {
                            orderItemId = 11,
                            menuItemId = 5,
                            orderId = 8,
                            quantity = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Property<int>("reservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reservationId"));

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.Property<int>("partySize")
                        .HasColumnType("int");

                    b.Property<DateTime>("reservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("restaurantId")
                        .HasColumnType("int");

                    b.Property<int>("tableId")
                        .HasColumnType("int");

                    b.HasKey("reservationId");

                    b.HasIndex("customerId");

                    b.HasIndex("restaurantId");

                    b.HasIndex("tableId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            reservationId = 1,
                            customerId = 1,
                            partySize = 4,
                            reservationDate = new DateTime(2024, 2, 2, 20, 15, 39, 420, DateTimeKind.Local).AddTicks(8928),
                            restaurantId = 1,
                            tableId = 1
                        },
                        new
                        {
                            reservationId = 2,
                            customerId = 2,
                            partySize = 2,
                            reservationDate = new DateTime(2024, 2, 2, 20, 15, 39, 420, DateTimeKind.Local).AddTicks(8943),
                            restaurantId = 2,
                            tableId = 2
                        },
                        new
                        {
                            reservationId = 3,
                            customerId = 3,
                            partySize = 6,
                            reservationDate = new DateTime(2024, 2, 2, 22, 15, 39, 420, DateTimeKind.Local).AddTicks(8946),
                            restaurantId = 1,
                            tableId = 3
                        },
                        new
                        {
                            reservationId = 4,
                            customerId = 1,
                            partySize = 1,
                            reservationDate = new DateTime(2024, 2, 2, 23, 15, 39, 420, DateTimeKind.Local).AddTicks(8948),
                            restaurantId = 2,
                            tableId = 2
                        },
                        new
                        {
                            reservationId = 5,
                            customerId = 2,
                            partySize = 4,
                            reservationDate = new DateTime(2024, 2, 2, 21, 15, 39, 420, DateTimeKind.Local).AddTicks(8950),
                            restaurantId = 1,
                            tableId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantId"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("openingHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            address = "123 Main St.",
                            name = "Mr Italian",
                            openingHours = "9:00 AM - 10:00 PM",
                            phoneNumber = "555-123-4567"
                        },
                        new
                        {
                            RestaurantId = 2,
                            address = "456 Manara St.",
                            name = "Meat Haven",
                            openingHours = "10:00 AM - 9:00 PM",
                            phoneNumber = "555-987-6543"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Property<int>("tableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tableId"));

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<int>("restaurantId")
                        .HasColumnType("int");

                    b.HasKey("tableId");

                    b.HasIndex("restaurantId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            tableId = 1,
                            capacity = 4,
                            restaurantId = 1
                        },
                        new
                        {
                            tableId = 2,
                            capacity = 2,
                            restaurantId = 2
                        },
                        new
                        {
                            tableId = 3,
                            capacity = 6,
                            restaurantId = 1
                        },
                        new
                        {
                            tableId = 4,
                            capacity = 4,
                            restaurantId = 2
                        },
                        new
                        {
                            tableId = 5,
                            capacity = 5,
                            restaurantId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("employees")
                        .HasForeignKey("restaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItems", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("menuItems")
                        .HasForeignKey("restaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Employee", "employee")
                        .WithMany("orders")
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Reservation", "reservation")
                        .WithMany("orders")
                        .HasForeignKey("reservationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("employee");

                    b.Navigation("reservation");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItems", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.MenuItems", "menuItems")
                        .WithMany("OrderItems")
                        .HasForeignKey("menuItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("menuItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Customer", "customer")
                        .WithMany("reservations")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "restaurant")
                        .WithMany("reservations")
                        .HasForeignKey("restaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Table", "table")
                        .WithMany("reservations")
                        .HasForeignKey("tableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("restaurant");

                    b.Navigation("table");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("tables")
                        .HasForeignKey("restaurantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Navigation("reservations");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItems", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Restaurant", b =>
                {
                    b.Navigation("employees");

                    b.Navigation("menuItems");

                    b.Navigation("reservations");

                    b.Navigation("tables");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Navigation("reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
