using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryControlSystem.Migrations
{
    public partial class Added_Extra_Fields_OrderList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderListID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderListID",
                table: "Orders",
                column: "OrderListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderLists_OrderListID",
                table: "Orders",
                column: "OrderListID",
                principalTable: "OrderLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderLists_OrderListID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderListID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderListID",
                table: "Orders");
        }
    }
}
