using Microsoft.EntityFrameworkCore.Migrations;

namespace onlineShop.Migrations
{
    public partial class RemoveCartInventoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityInventoryEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartEntityInventoryEntity",
                columns: table => new
                {
                    CartssId = table.Column<int>(type: "int", nullable: false),
                    InventoriessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntityInventoryEntity", x => new { x.CartssId, x.InventoriessId });
                    table.ForeignKey(
                        name: "FK_CartEntityInventoryEntity_Carts_CartssId",
                        column: x => x.CartssId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntityInventoryEntity_Inventories_InventoriessId",
                        column: x => x.InventoriessId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityInventoryEntity_InventoriessId",
                table: "CartEntityInventoryEntity",
                column: "InventoriessId");
        }
    }
}
