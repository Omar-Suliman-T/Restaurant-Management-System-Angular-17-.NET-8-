using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My_Resturant.Migrations
{
    /// <inheritdoc />
    public partial class firstone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookupTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoolupItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lookupTypeID = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoolupItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_LoolupItems_LookupTypes_lookupTypeID",
                        column: x => x.lookupTypeID,
                        principalTable: "LookupTypes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_Categories_LoolupItems_name",
                        column: x => x.name,
                        principalTable: "LoolupItems",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Ingrediants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ingrediants_LoolupItems_unit",
                        column: x => x.unit,
                        principalTable: "LoolupItems",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    password = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.id);
                    table.CheckConstraint("CH_password_Length", "LEN(password) >= 5");
                    table.ForeignKey(
                        name: "FK_People_LoolupItems_role",
                        column: x => x.role,
                        principalTable: "LoolupItems",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ingrediants = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_category",
                        column: x => x.category,
                        principalTable: "Categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Meals_Categories_category",
                        column: x => x.category,
                        principalTable: "Categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    costumerId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    orderStatus = table.Column<int>(type: "int", nullable: false),
                    deliveryAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    costumerNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    netPrice = table.Column<double>(type: "float", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_LoolupItems_orderStatus",
                        column: x => x.orderStatus,
                        principalTable: "LoolupItems",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_People_costumerId",
                        column: x => x.costumerId,
                        principalTable: "People",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "IngrediantItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    ingredientId = table.Column<int>(type: "int", nullable: false),
                    Ingrediantid = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngrediantItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_IngrediantItem_Ingrediants_Ingrediantid",
                        column: x => x.Ingrediantid,
                        principalTable: "Ingrediants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngrediantItem_Items_itemId",
                        column: x => x.itemId,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    mealID = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_MealDetails_Items_itemID",
                        column: x => x.itemID,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealDetails_Meals_mealID",
                        column: x => x.mealID,
                        principalTable: "Meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItemDetails_Items_itemId",
                        column: x => x.itemId,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemDetails_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMealDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    modificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMealDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderMealDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMealDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "id", "modificationDate", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CategoryName" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OrderStatus" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unit" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rateing" }
                });

            migrationBuilder.InsertData(
                table: "LoolupItems",
                columns: new[] { "id", "lookupTypeID", "modificationDate", "name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fast Food" },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italian" },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Healty" },
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New" },
                    { 5, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Confirmed" },
                    { 6, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cancelled" },
                    { 7, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delivered" },
                    { 8, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gm" },
                    { 9, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ml" },
                    { 10, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pieces" },
                    { 11, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { 12, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer" },
                    { 13, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very bad" },
                    { 14, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bad" },
                    { 15, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good" },
                    { 16, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very Good" },
                    { 17, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exillent" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "id", "creationDate", "email", "firstName", "isActive", "lastName", "modificationDate", "password", "phone", "role" },
                values: new object[] { 1, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "6D7C381F8597F4DFB9D47ABBEE793CCC5DBAFCD5ECB7B4A02C436C2AB27D57B0DAD78FE14BBC0AAC76C1CBB124728E51", "Omar", true, "Suliman", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A48646AF11C3BC78393F42E2247557917DCC5BD3062E2A3F63C02AC26056AF3C493DFA67FD585EFC59D0D13FF3DD95DD", "079428423", 11 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngrediantItem_Ingrediantid",
                table: "IngrediantItem",
                column: "Ingrediantid");

            migrationBuilder.CreateIndex(
                name: "IX_IngrediantItem_itemId",
                table: "IngrediantItem",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediants_unit",
                table: "Ingrediants",
                column: "unit");

            migrationBuilder.CreateIndex(
                name: "IX_Items_category",
                table: "Items",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_Items_name",
                table: "Items",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupTypes_name",
                table: "LookupTypes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoolupItems_lookupTypeID",
                table: "LoolupItems",
                column: "lookupTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoolupItems_name",
                table: "LoolupItems",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealDetails_itemID",
                table: "MealDetails",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_MealDetails_mealID",
                table: "MealDetails",
                column: "mealID");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_category",
                table: "Meals",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDetails_itemId",
                table: "OrderItemDetails",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDetails_orderId",
                table: "OrderItemDetails",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMealDetails_MealId",
                table: "OrderMealDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMealDetails_OrderId",
                table: "OrderMealDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_costumerId",
                table: "Orders",
                column: "costumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_orderStatus",
                table: "Orders",
                column: "orderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_People_email",
                table: "People",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_password",
                table: "People",
                column: "password",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_role",
                table: "People",
                column: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngrediantItem");

            migrationBuilder.DropTable(
                name: "MealDetails");

            migrationBuilder.DropTable(
                name: "OrderItemDetails");

            migrationBuilder.DropTable(
                name: "OrderMealDetails");

            migrationBuilder.DropTable(
                name: "Ingrediants");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "LoolupItems");

            migrationBuilder.DropTable(
                name: "LookupTypes");
        }
    }
}
