using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class addAddApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 1,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6635));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 2,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6678));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 3,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 4,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 5,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 6,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6688));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 7,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 8,
                column: "orderDate",
                value: new DateTime(2024, 2, 2, 19, 15, 39, 419, DateTimeKind.Local).AddTicks(6692));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 1,
                column: "reservationDate",
                value: new DateTime(2024, 2, 2, 20, 15, 39, 420, DateTimeKind.Local).AddTicks(8928));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 2,
                column: "reservationDate",
                value: new DateTime(2024, 2, 2, 20, 15, 39, 420, DateTimeKind.Local).AddTicks(8943));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 3,
                column: "reservationDate",
                value: new DateTime(2024, 2, 2, 22, 15, 39, 420, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 4,
                column: "reservationDate",
                value: new DateTime(2024, 2, 2, 23, 15, 39, 420, DateTimeKind.Local).AddTicks(8948));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 5,
                column: "reservationDate",
                value: new DateTime(2024, 2, 2, 21, 15, 39, 420, DateTimeKind.Local).AddTicks(8950));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 1,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 2,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 3,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3979));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 4,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3981));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 5,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3983));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 6,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3987));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 7,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 8,
                column: "orderDate",
                value: new DateTime(2023, 12, 22, 19, 25, 51, 687, DateTimeKind.Local).AddTicks(3991));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 1,
                column: "reservationDate",
                value: new DateTime(2023, 12, 22, 20, 25, 51, 688, DateTimeKind.Local).AddTicks(7699));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 2,
                column: "reservationDate",
                value: new DateTime(2023, 12, 22, 20, 25, 51, 688, DateTimeKind.Local).AddTicks(7715));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 3,
                column: "reservationDate",
                value: new DateTime(2023, 12, 22, 22, 25, 51, 688, DateTimeKind.Local).AddTicks(7718));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 4,
                column: "reservationDate",
                value: new DateTime(2023, 12, 22, 23, 25, 51, 688, DateTimeKind.Local).AddTicks(7720));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "reservationId",
                keyValue: 5,
                column: "reservationDate",
                value: new DateTime(2023, 12, 22, 21, 25, 51, 688, DateTimeKind.Local).AddTicks(7722));
        }
    }
}
