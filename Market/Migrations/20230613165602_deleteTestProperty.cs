using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class deleteTestProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreate",
                value: new DateTime(2023, 6, 13, 19, 56, 2, 550, DateTimeKind.Local).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreate",
                value: new DateTime(2023, 6, 13, 19, 56, 2, 550, DateTimeKind.Local).AddTicks(3514));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreate",
                value: new DateTime(2023, 6, 13, 19, 56, 2, 550, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreate",
                value: new DateTime(2023, 6, 13, 19, 56, 2, 550, DateTimeKind.Local).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreate",
                value: new DateTime(2023, 6, 13, 19, 56, 2, 550, DateTimeKind.Local).AddTicks(3575));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2023, 6, 4, 20, 26, 37, 365, DateTimeKind.Local).AddTicks(4215), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 6, 4, 20, 26, 37, 365, DateTimeKind.Local).AddTicks(4298), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 6, 4, 20, 26, 37, 365, DateTimeKind.Local).AddTicks(4302), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 6, 4, 20, 26, 37, 365, DateTimeKind.Local).AddTicks(4305), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "DateCreate", "test" },
                values: new object[] { new DateTime(2023, 6, 4, 20, 26, 37, 365, DateTimeKind.Local).AddTicks(4308), 0 });
        }
    }
}
