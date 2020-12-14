using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryControlSystem.Migrations
{
    public partial class Changed_Order_Properties_Fulfilled_Ordered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullfilled",
                table: "Orders");

            migrationBuilder.AlterColumn<bool>(
                name: "Ordered",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Fulfilled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fulfilled",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Ordered",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Fullfilled",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
