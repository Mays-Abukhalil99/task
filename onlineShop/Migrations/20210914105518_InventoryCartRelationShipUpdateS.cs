using Microsoft.EntityFrameworkCore.Migrations;

namespace onlineShop.Migrations
{
    public partial class InventoryCartRelationShipUpdateS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Inventories");
        }
    }
}
