using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unityHomesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyNumber",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "City", "DateAdded", "DateUpdated", "Postcode", "PropertyNumber", "Street" },
                values: new object[] { "City 1", new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2250), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2250), new TimeSpan(0, 0, 0, 0, 0)), "AB12 3CD", 1, "Road 1" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "City", "DateAdded", "DateUpdated", "Postcode", "PropertyNumber", "Street" },
                values: new object[] { "City 3", new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2250), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2250), new TimeSpan(0, 0, 0, 0, 0)), "BC34 5DE", 2, "Road 2" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "City", "DateAdded", "DateUpdated", "Postcode", "PropertyNumber", "Street" },
                values: new object[] { "City 3", new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2260), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 9, 13, 4, 21, 594, DateTimeKind.Unspecified).AddTicks(2260), new TimeSpan(0, 0, 0, 0, 0)), "XY98 7ZW", 3, "Road 3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyNumber",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4790), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4790), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4800), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4800), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4800), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 8, 9, 18, 52, 32, DateTimeKind.Unspecified).AddTicks(4800), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
