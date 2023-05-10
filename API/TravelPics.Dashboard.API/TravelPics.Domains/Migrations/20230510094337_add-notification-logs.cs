using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class addnotificationlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM InAppNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_NotificationStatuses_StatusId",
                table: "InAppNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_Users_ReceiverId",
                table: "InAppNotifications");

            migrationBuilder.DropIndex(
                name: "IX_InAppNotifications_ReceiverId",
                table: "InAppNotifications");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "InAppNotifications");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "InAppNotifications",
                newName: "NotificationLogId");

            migrationBuilder.RenameIndex(
                name: "IX_InAppNotifications_StatusId",
                table: "InAppNotifications",
                newName: "IX_InAppNotifications_NotificationLogId");

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_NotificationStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "NotificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_NotificationTypeId",
                table: "NotificationLogs",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_ReceiverId",
                table: "NotificationLogs",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_StatusId",
                table: "NotificationLogs",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_NotificationLogs_NotificationLogId",
                table: "InAppNotifications",
                column: "NotificationLogId",
                principalTable: "NotificationLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_NotificationLogs_NotificationLogId",
                table: "InAppNotifications");

            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.RenameColumn(
                name: "NotificationLogId",
                table: "InAppNotifications",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_InAppNotifications_NotificationLogId",
                table: "InAppNotifications",
                newName: "IX_InAppNotifications_StatusId");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "InAppNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InAppNotifications_ReceiverId",
                table: "InAppNotifications",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_NotificationStatuses_StatusId",
                table: "InAppNotifications",
                column: "StatusId",
                principalTable: "NotificationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_Users_ReceiverId",
                table: "InAppNotifications",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
