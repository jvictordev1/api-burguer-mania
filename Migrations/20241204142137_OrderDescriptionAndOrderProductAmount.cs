using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_burguer_mania.Migrations
{
    /// <inheritdoc />
    public partial class OrderDescriptionAndOrderProductAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "OrdersProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");
        }
    }
}
