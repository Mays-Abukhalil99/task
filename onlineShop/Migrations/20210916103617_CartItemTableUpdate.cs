using Microsoft.EntityFrameworkCore.Migrations;

namespace onlineShop.Migrations
{
    public partial class CartItemTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
