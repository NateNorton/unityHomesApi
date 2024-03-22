using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unityHomesApi.Migrations
{
    /// <inheritdoc />
    public partial class articlesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Messages",
                newName: "Edited");

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Properties",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Articles",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateAdded", "DateUpdated", "FullDescription", "UserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), "Default full description", 0L });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateAdded", "DateUpdated", "FullDescription", "UserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), "Default full description", 0L });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateAdded", "DateUpdated", "FullDescription", "UserId" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 22, 14, 21, 18, 907, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), "Default full description", 0L });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "Edited",
                table: "Messages",
                newName: "IsRead");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DateAdded", "DateUpdated" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(240), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 5, 20, 14, 17, 372, DateTimeKind.Unspecified).AddTicks(240), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
