using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "test",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 5, 21, 18, 1, 27, 383, DateTimeKind.Local).AddTicks(2914), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 5, 21, 18, 1, 27, 383, DateTimeKind.Local).AddTicks(2985), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 5, 21, 18, 1, 27, 383, DateTimeKind.Local).AddTicks(2990), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 5, 21, 18, 1, 27, 383, DateTimeKind.Local).AddTicks(2995), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 5, 21, 18, 1, 27, 383, DateTimeKind.Local).AddTicks(2999), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 21, 17, 55, 4, 840, DateTimeKind.Local).AddTicks(9837));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 21, 17, 55, 4, 840, DateTimeKind.Local).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 21, 17, 55, 4, 840, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 21, 17, 55, 4, 840, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 21, 17, 55, 4, 840, DateTimeKind.Local).AddTicks(9896));
        }
    }
}
