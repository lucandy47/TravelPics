using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class updatenotifstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_NotificationStatus_StatusId",
                table: "InAppNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationStatus",
                table: "NotificationStatus");

            migrationBuilder.RenameTable(
                name: "NotificationStatus",
                newName: "NotificationStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationStatuses",
                table: "NotificationStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_NotificationStatuses_StatusId",
                table: "InAppNotifications",
                column: "StatusId",
                principalTable: "NotificationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_NotificationStatuses_StatusId",
                table: "InAppNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationStatuses",
                table: "NotificationStatuses");

            migrationBuilder.RenameTable(
                name: "NotificationStatuses",
                newName: "NotificationStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationStatus",
                table: "NotificationStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_NotificationStatus_StatusId",
                table: "InAppNotifications",
                column: "StatusId",
                principalTable: "NotificationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
