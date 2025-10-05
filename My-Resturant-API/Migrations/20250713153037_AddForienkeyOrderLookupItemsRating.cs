using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class AddForienkeyOrderLookupItemsRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_rating",
                table: "Orders",
                column: "rating");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders",
                column: "orderStatus",
                principalTable: "LoolupItems",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoolupItems_rating",
                table: "Orders",
                column: "rating",
                principalTable: "LoolupItems",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoolupItems_rating",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_rating",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders",
                column: "orderStatus",
                principalTable: "LoolupItems",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
