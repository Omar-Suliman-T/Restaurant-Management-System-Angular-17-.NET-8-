using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class Reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberOfPeople = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    specialRequests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_LoolupItems_status",
                        column: x => x.status,
                        principalTable: "LoolupItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reservations_People_customerId",
                        column: x => x.customerId,
                        principalTable: "People",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Status");

            migrationBuilder.InsertData(
                table: "LoolupItems",
                columns: new[] { "id", "lookupTypeID", "modificationDate", "name" },
                values: new object[] { 18, 2, null, "Completed" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_customerId",
                table: "Reservations",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_status",
                table: "Reservations",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DeleteData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "OrderStatus");
        }
    }
}
