using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class addTwoAttributesToPeopleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passwordResetCode",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "passwordResetExpiry",
                table: "People",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "passwordResetCode", "passwordResetExpiry" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordResetCode",
                table: "People");

            migrationBuilder.DropColumn(
                name: "passwordResetExpiry",
                table: "People");
        }
    }
}
