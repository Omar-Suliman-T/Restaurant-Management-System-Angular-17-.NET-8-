using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class codeCreationDateRemoveDefualtValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "discountCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "No Discount Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "discountCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "No Discount Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
