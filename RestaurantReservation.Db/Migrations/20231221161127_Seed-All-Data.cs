using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    openingHours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_employees_restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    menuItemId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.menuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    tableId = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.tableId);
                    table.ForeignKey(
                        name: "FK_tables_restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    reservationId = table.Column<int>(type: "int", nullable: false),
                    reservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partySize = table.Column<int>(type: "int", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false),
                    tableId = table.Column<int>(type: "int", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.reservationId);
                    table.ForeignKey(
                        name: "FK_reservations_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "customerId");
                    table.ForeignKey(
                        name: "FK_reservations_restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK_reservations_tables_tableId",
                        column: x => x.tableId,
                        principalTable: "tables",
                        principalColumn: "tableId");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reservationId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_orders_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_orders_reservations_reservationId",
                        column: x => x.reservationId,
                        principalTable: "reservations",
                        principalColumn: "reservationId");
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    orderItemId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    menuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.orderItemId);
                    table.ForeignKey(
                        name: "FK_orderItems_MenuItems_menuItemId",
                        column: x => x.menuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "menuItemId");
                    table.ForeignKey(
                        name: "FK_orderItems_orders_orderId",
                        column: x => x.orderId,
                        principalTable: "orders",
                        principalColumn: "orderId");
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "customerId", "email", "firstName", "lastName", "phoneNumber" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John", "Doe", "123-456-7890" },
                    { 2, "jane@example.com", "Jane", "Smith", "987-654-3210" },
                    { 3, "michael@example.com", "Michael", "Johnson", "555-555-5555" },
                    { 4, "emily@example.com", "Emily", "Williams", "111-222-3333" },
                    { 5, "william@example.com", "William", "Brown", "444-444-4444" }
                });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "RestaurantId", "address", "name", "openingHours", "phoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St.", "Mr Italian", "9:00 AM - 10:00 PM", "555-123-4567" },
                    { 2, "456 Manara St.", "Meat Haven", "10:00 AM - 9:00 PM", "555-987-6543" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "menuItemId", "description", "name", "price", "restaurantId" },
                values: new object[,]
                {
                    { 1, "Classic Italian pasta dish", "Spaghetti Bolognese", 12.99m, 1 },
                    { 2, "Freshly grilled salmon with lemon butter sauce", "Grilled Salmon", 17.99m, 2 },
                    { 3, "Traditional Italian pizza with tomatoes and fresh mozzarella", "Margherita Pizza", 10.99m, 1 },
                    { 4, "Juicy beef steak cooked to perfection", "Beef Steak", 19.99m, 2 },
                    { 5, "Crisp romaine lettuce, croutons, and Caesar dressing", "Caesar Salad", 8.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "employeeId", "firstName", "lastName", "position", "restaurantId" },
                values: new object[,]
                {
                    { 1, "Alice", "Johnson", "Manager", 1 },
                    { 2, "Bob", "Smith", "Manager", 2 },
                    { 3, "Charlie", "Williams", "Waiter", 1 },
                    { 4, "David", "Brown", "Chef", 2 },
                    { 5, "Eva", "Davis", "Chef", 1 },
                    { 6, "John", "Davis", "Waiter", 2 }
                });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "tableId", "capacity", "restaurantId" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 2 },
                    { 3, 6, 1 },
                    { 4, 4, 2 },
                    { 5, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "reservations",
                columns: new[] { "reservationId", "customerId", "partySize", "reservationDate", "restaurantId", "tableId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2023, 12, 21, 19, 11, 26, 742, DateTimeKind.Local).AddTicks(1945), 1, 1 },
                    { 2, 2, 2, new DateTime(2023, 12, 21, 19, 11, 26, 742, DateTimeKind.Local).AddTicks(1959), 2, 2 },
                    { 3, 3, 6, new DateTime(2023, 12, 21, 21, 11, 26, 742, DateTimeKind.Local).AddTicks(1962), 1, 3 },
                    { 4, 1, 1, new DateTime(2023, 12, 21, 22, 11, 26, 742, DateTimeKind.Local).AddTicks(1965), 2, 2 },
                    { 5, 2, 4, new DateTime(2023, 12, 21, 20, 11, 26, 742, DateTimeKind.Local).AddTicks(1967), 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "orderId", "employeeId", "orderDate", "reservationId" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8002), 1 },
                    { 2, 5, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8053), 1 },
                    { 3, 4, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8063), 2 },
                    { 4, 4, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8066), 2 },
                    { 5, 5, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8068), 3 },
                    { 6, 4, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8071), 4 },
                    { 7, 5, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8073), 5 },
                    { 8, 5, new DateTime(2023, 12, 21, 18, 11, 26, 740, DateTimeKind.Local).AddTicks(8075), 5 }
                });

            migrationBuilder.InsertData(
                table: "orderItems",
                columns: new[] { "orderItemId", "menuItemId", "orderId", "quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 3, 1, 1 },
                    { 3, 5, 2, 3 },
                    { 4, 1, 2, 1 },
                    { 5, 2, 3, 1 },
                    { 6, 4, 3, 1 },
                    { 7, 2, 4, 1 },
                    { 8, 1, 5, 1 },
                    { 9, 2, 6, 2 },
                    { 10, 5, 7, 1 },
                    { 11, 5, 8, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_restaurantId",
                table: "employees",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_restaurantId",
                table: "MenuItems",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_menuItemId",
                table: "orderItems",
                column: "menuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_orderId",
                table: "orderItems",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_employeeId",
                table: "orders",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_reservationId",
                table: "orders",
                column: "reservationId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_customerId",
                table: "reservations",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_restaurantId",
                table: "reservations",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_tableId",
                table: "reservations",
                column: "tableId");

            migrationBuilder.CreateIndex(
                name: "IX_tables_restaurantId",
                table: "tables",
                column: "restaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "restaurants");
        }
    }
}
