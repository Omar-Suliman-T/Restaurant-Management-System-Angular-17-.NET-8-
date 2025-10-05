using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class changeExcellent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 17,
                column: "name",
                value: "Excellent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 17,
                column: "name",
                value: "Exillent");
        }
    }
}
