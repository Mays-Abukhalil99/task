using Microsoft.EntityFrameworkCore.Migrations;

namespace onlineShop.Migrations
{
    public partial class UserCartRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserEntityId",
                table: "Carts",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserEntityId",
                table: "Carts",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserEntityId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserEntityId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Carts");
        }
    }
}
