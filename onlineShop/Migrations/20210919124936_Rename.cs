using Microsoft.EntityFrameworkCore.Migrations;

namespace onlineShop.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartEntityId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Inventories_InventoryEntityId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartEntityId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_InventoryEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "InventoryEntityId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ItemId",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Inventories_ItemId",
                table: "CartItems",
                column: "ItemId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Inventories_ItemId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ItemId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryEntityId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartEntityId",
                table: "CartItems",
                column: "CartEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_InventoryEntityId",
                table: "CartItems",
                column: "InventoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartEntityId",
                table: "CartItems",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Inventories_InventoryEntityId",
                table: "CartItems",
                column: "InventoryEntityId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
