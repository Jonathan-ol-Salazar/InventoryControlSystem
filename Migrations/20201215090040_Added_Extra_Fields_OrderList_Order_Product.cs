using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryControlSystem.Migrations
{
    public partial class Added_Extra_Fields_OrderList_Order_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderLists");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OrderLists",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OrderLists",
                newName: "Business");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumUnits",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderListID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "OrderLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "OrderLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "OrderLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderListID",
                table: "Products",
                column: "OrderListID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLists_SupplierID",
                table: "OrderLists",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLists_Suppliers_SupplierID",
                table: "OrderLists",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderLists_OrderListID",
                table: "Products",
                column: "OrderListID",
                principalTable: "OrderLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierID",
                table: "Products",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLists_Suppliers_SupplierID",
                table: "OrderLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderLists_OrderListID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderListID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderLists_SupplierID",
                table: "OrderLists");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NumUnits",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderListID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "OrderLists");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "OrderLists");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderLists");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "OrderLists");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "OrderLists",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Business",
                table: "OrderLists",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
