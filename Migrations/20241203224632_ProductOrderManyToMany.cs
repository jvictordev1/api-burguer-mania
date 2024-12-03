using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_burguer_mania.Migrations
{
    /// <inheritdoc />
    public partial class ProductOrderManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUser_Orders_OrdersId",
                table: "OrderUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderUser_Users_UsersId",
                table: "OrderUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "OrderUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrderUser",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUser_UsersId",
                table: "OrderUser",
                newName: "IX_OrderUser_UserId");

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUser_Orders_OrderId",
                table: "OrderUser",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUser_Users_UserId",
                table: "OrderUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUser_Orders_OrderId",
                table: "OrderUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderUser_Users_UserId",
                table: "OrderUser");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderUser",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUser_UserId",
                table: "OrderUser",
                newName: "IX_OrderUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUser_Orders_OrdersId",
                table: "OrderUser",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUser_Users_UsersId",
                table: "OrderUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
