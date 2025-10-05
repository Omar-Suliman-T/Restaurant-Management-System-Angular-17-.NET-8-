using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class makeModificationDateNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "People",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "OrderMealDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "OrderItemDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Meals",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "MealDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "LoolupItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "LookupTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Items",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Ingrediants",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "IngrediantItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Categories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 2,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 3,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 4,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 5,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 2,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 3,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 4,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 5,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 6,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 7,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 8,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 9,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 10,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 11,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 12,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 13,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 14,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 15,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 16,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 17,
                column: "modificationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "OrderMealDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "OrderItemDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Meals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "MealDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "LoolupItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "LookupTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Ingrediants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "IngrediantItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 2,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 3,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 4,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LookupTypes",
                keyColumn: "id",
                keyValue: 5,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 2,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 3,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 4,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 5,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 6,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 7,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 8,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 9,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 10,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 11,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 12,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 13,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 14,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 15,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 16,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LoolupItems",
                keyColumn: "id",
                keyValue: 17,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "id",
                keyValue: 1,
                column: "modificationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
