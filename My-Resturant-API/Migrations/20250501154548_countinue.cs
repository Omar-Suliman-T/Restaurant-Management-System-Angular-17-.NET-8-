using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class countinue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_LoolupItems_name",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediants_LoolupItems_unit",
                table: "Ingrediants");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_category",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_LoolupItems_LookupTypes_lookupTypeID",
                table: "LoolupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_category",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_People_LoolupItems_role",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Categories_name",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "orderStatus",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "category",
                table: "Meals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "lookupTypeID",
                table: "LoolupItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "category",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "unit",
                table: "Ingrediants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "name",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_LoolupItems_name",
                table: "Categories",
                column: "name",
                principalTable: "LoolupItems",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediants_LoolupItems_unit",
                table: "Ingrediants",
                column: "unit",
                principalTable: "LoolupItems",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_category",
                table: "Items",
                column: "category",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LoolupItems_LookupTypes_lookupTypeID",
                table: "LoolupItems",
                column: "lookupTypeID",
                principalTable: "LookupTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_category",
                table: "Meals",
                column: "category",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders",
                column: "orderStatus",
                principalTable: "LoolupItems",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_People_LoolupItems_role",
                table: "People",
                column: "role",
                principalTable: "LoolupItems",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_LoolupItems_name",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingrediants_LoolupItems_unit",
                table: "Ingrediants");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_category",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_LoolupItems_LookupTypes_lookupTypeID",
                table: "LoolupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_category",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_People_LoolupItems_role",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Categories_name",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "orderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "lookupTypeID",
                table: "LoolupItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "unit",
                table: "Ingrediants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "name",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_LoolupItems_name",
                table: "Categories",
                column: "name",
                principalTable: "LoolupItems",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrediants_LoolupItems_unit",
                table: "Ingrediants",
                column: "unit",
                principalTable: "LoolupItems",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_category",
                table: "Items",
                column: "category",
                principalTable: "Categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoolupItems_LookupTypes_lookupTypeID",
                table: "LoolupItems",
                column: "lookupTypeID",
                principalTable: "LookupTypes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_category",
                table: "Meals",
                column: "category",
                principalTable: "Categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoolupItems_orderStatus",
                table: "Orders",
                column: "orderStatus",
                principalTable: "LoolupItems",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_LoolupItems_role",
                table: "People",
                column: "role",
                principalTable: "LoolupItems",
                principalColumn: "id");
        }
    }
}
