using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    openingHours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    menuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.menuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    tableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.tableId);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partySize = table.Column<int>(type: "int", nullable: false),
                    restaurantId = table.Column<int>(type: "int", nullable: false),
                    tableId = table.Column<int>(type: "int", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "customerId");
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_restaurantId",
                        column: x => x.restaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_tableId",
                        column: x => x.tableId,
                        principalTable: "Tables",
                        principalColumn: "tableId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reservationId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId");
                    table.ForeignKey(
                        name: "FK_Orders_Reservations_reservationId",
                        column: x => x.reservationId,
                        principalTable: "Reservations",
                        principalColumn: "reservationId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    menuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.orderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_menuItemId",
                        column: x => x.menuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "menuItemId");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "orderId");
                });

            migrationBuilder.InsertData(
                table: "Customers",
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
                table: "Restaurants",
                columns: new[] { "RestaurantId", "address", "name", "openingHours", "phoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St.", "Mr Italian", "9:00 AM - 10:00 PM", "555-123-4567" },
                    { 2, "456 Manara St.", "Meat Haven", "10:00 AM - 9:00 PM", "555-987-6543" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
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
                table: "Tables",
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
                table: "Reservations",
                columns: new[] { "reservationId", "customerId", "partySize", "reservationDate", "restaurantId", "tableId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2023, 12, 22, 20, 25, 51, 688, DateTimeKind.Local).AddTicks(7699), 1, 1 },
                    { 2, 2, 2, new DateTime(2023, 12, 22, 20, 25, 51, 688, DateTimeKind.Local).AddTicks(7715), 2, 2 },
                    { 3, 3, 6, new DateTime(2023, 12, 22, 22, 25, 51, 688, DateTimeKind.Local).AddTicks(7718), 1, 3 },
                    { 4, 1, 1, new DateTime(2023, 12, 22, 23, 25, 51, 688, DateTimeKind.Local).AddTicks(7720), 2, 2 },
                    { 5, 2, 4, new DateTime(2023, 12, 22, 21, 25, 51, 688, DateTimeKind.Local).AddTicks(7722), 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "orderId", "employeeId", "orderDate", "reservationId" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3925), 1 },
                    { 2, 5, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3976), 1 },
                    { 3, 4, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3979), 2 },
                    { 4, 4, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3981), 2 },
                    { 5, 5, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3983), 3 },
                    { 6, 4, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3987), 4 },
                    { 7, 5, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3989), 5 },
                    { 8, 5, new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3991), 5 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
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
                name: "IX_Employees_restaurantId",
                table: "Employees",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_restaurantId",
                table: "MenuItems",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_menuItemId",
                table: "OrderItems",
                column: "menuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_orderId",
                table: "OrderItems",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_employeeId",
                table: "Orders",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_reservationId",
                table: "Orders",
                column: "reservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_customerId",
                table: "Reservations",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_restaurantId",
                table: "Reservations",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_tableId",
                table: "Reservations",
                column: "tableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_restaurantId",
                table: "Tables",
                column: "restaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
