using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Pizza",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderDetail",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderDetail",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 6.0m);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 5.0m);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 6.5m);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 7.0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Pizza",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 6.0);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 6.5);

            migrationBuilder.UpdateData(
                table: "Pizza",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 7.0);
        }
    }
}
