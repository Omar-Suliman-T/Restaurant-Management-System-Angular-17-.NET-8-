using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class secoundone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngrediantItem_Ingrediants_Ingrediantid",
                table: "IngrediantItem");

            migrationBuilder.DropColumn(
                name: "ingredientId",
                table: "IngrediantItem");

            migrationBuilder.RenameColumn(
                name: "Ingrediantid",
                table: "IngrediantItem",
                newName: "ingrediantId");

            migrationBuilder.RenameIndex(
                name: "IX_IngrediantItem_Ingrediantid",
                table: "IngrediantItem",
                newName: "IX_IngrediantItem_ingrediantId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngrediantItem_Ingrediants_ingrediantId",
                table: "IngrediantItem",
                column: "ingrediantId",
                principalTable: "Ingrediants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngrediantItem_Ingrediants_ingrediantId",
                table: "IngrediantItem");

            migrationBuilder.RenameColumn(
                name: "ingrediantId",
                table: "IngrediantItem",
                newName: "Ingrediantid");

            migrationBuilder.RenameIndex(
                name: "IX_IngrediantItem_ingrediantId",
                table: "IngrediantItem",
                newName: "IX_IngrediantItem_Ingrediantid");

            migrationBuilder.AddColumn<int>(
                name: "ingredientId",
                table: "IngrediantItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_IngrediantItem_Ingrediants_Ingrediantid",
                table: "IngrediantItem",
                column: "Ingrediantid",
                principalTable: "Ingrediants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
