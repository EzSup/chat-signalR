using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSignalR.DataAccess.AzureSQL.Migrations
{
    /// <inheritdoc />
    public partial class UserAndChatTablesRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messsages_Chats_ChatId",
                table: "Messsages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messsages_Users_AuthorId",
                table: "Messsages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messsages",
                table: "Messsages");

            migrationBuilder.DropIndex(
                name: "IX_Messsages_AuthorId",
                table: "Messsages");

            migrationBuilder.DropIndex(
                name: "IX_Messsages_ChatId",
                table: "Messsages");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Messsages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Messsages");

            migrationBuilder.RenameTable(
                name: "Messsages",
                newName: "Messages");

            migrationBuilder.RenameColumn(
                name: "messageContent",
                table: "Messages",
                newName: "MessageContent");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NegativeScore",
                table: "Messages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NeutralScore",
                table: "Messages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositiveScore",
                table: "Messages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NegativeScore",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NeutralScore",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PositiveScore",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Messsages");

            migrationBuilder.RenameColumn(
                name: "MessageContent",
                table: "Messsages",
                newName: "messageContent");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Messsages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "Messsages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messsages",
                table: "Messsages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messsages_AuthorId",
                table: "Messsages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messsages_ChatId",
                table: "Messsages",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messsages_Chats_ChatId",
                table: "Messsages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messsages_Users_AuthorId",
                table: "Messsages",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
