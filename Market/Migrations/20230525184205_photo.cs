using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar4",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Avatar5",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 25, 21, 42, 5, 42, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 25, 21, 42, 5, 42, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 25, 21, 42, 5, 42, DateTimeKind.Local).AddTicks(5304));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 25, 21, 42, 5, 42, DateTimeKind.Local).AddTicks(5308));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DateCreate",
                value: new DateTime(2023, 5, 25, 21, 42, 5, 42, DateTimeKind.Local).AddTicks(5311));

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar2",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar3",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar4",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar5",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Avatar", "Avatar2", "Avatar3", "Avatar4", "Avatar5", "DateCreate" },
                values: new object[] { null, null, null, null, null, new DateTime(2023, 5, 25, 19, 10, 14, 387, DateTimeKind.Local).AddTicks(3734) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Avatar", "Avatar2", "Avatar3", "Avatar4", "Avatar5", "DateCreate" },
                values: new object[] { null, null, null, null, null, new DateTime(2023, 5, 25, 19, 10, 14, 387, DateTimeKind.Local).AddTicks(3811) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Avatar", "Avatar2", "Avatar3", "Avatar4", "Avatar5", "DateCreate" },
                values: new object[] { null, null, null, null, null, new DateTime(2023, 5, 25, 19, 10, 14, 387, DateTimeKind.Local).AddTicks(3817) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Avatar", "Avatar2", "Avatar3", "Avatar4", "Avatar5", "DateCreate" },
                values: new object[] { null, null, null, null, null, new DateTime(2023, 5, 25, 19, 10, 14, 387, DateTimeKind.Local).AddTicks(3822) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Avatar", "Avatar2", "Avatar3", "Avatar4", "Avatar5", "DateCreate" },
                values: new object[] { null, null, null, null, null, new DateTime(2023, 5, 25, 19, 10, 14, 387, DateTimeKind.Local).AddTicks(3827) });
        }
    }
}
