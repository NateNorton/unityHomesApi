using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace unityHomesApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuths",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    NumberOfBedrooms = table.Column<int>(type: "integer", nullable: false),
                    HasGarden = table.Column<bool>(type: "boolean", nullable: false),
                    SquareMeeterage = table.Column<int>(type: "integer", nullable: false),
                    MonthlyRent = table.Column<int>(type: "integer", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    PropertyNumber = table.Column<int>(type: "integer", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    PropertyTypeId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "House" },
                    { 2, "Apartment" },
                    { 3, "Cottage" },
                    { 4, "Flat" },
                    { 5, "Mansion" },
                    { 6, "Studio" },
                    { 7, "Villa" },
                    { 8, "Bungalow" },
                    { 9, "Penthouse" },
                    { 10, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "City", "DateAdded", "DateUpdated", "Description", "HasGarden", "IsAvailable", "MonthlyRent", "NumberOfBedrooms", "Postcode", "PropertyNumber", "PropertyTypeId", "SquareMeeterage", "Street", "Title" },
                values: new object[,]
                {
                    { 1L, "City 1", new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3650), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3650), new TimeSpan(0, 0, 0, 0, 0)), "A nice little cottage in the countryside.", true, true, 300, 3, "AB12 3CD", 1, 4, 100, "Road 1", "Cozy Cottage" },
                    { 2L, "City 3", new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3650), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3650), new TimeSpan(0, 0, 0, 0, 0)), "Modern apartment in the city center.", false, true, 200, 1, "BC34 5DE", 2, 2, 50, "Road 2", "Urban Apartment" },
                    { 3L, "City 3", new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3650), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 7, 8, 57, 38, 376, DateTimeKind.Unspecified).AddTicks(3660), new TimeSpan(0, 0, 0, 0, 0)), "A nice little cottage in the countryside.", true, false, 600, 4, "XY98 7ZW", 3, 1, 185, "Road 3", "Country House" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "UserAuths");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PropertyTypes");
        }
    }
}
