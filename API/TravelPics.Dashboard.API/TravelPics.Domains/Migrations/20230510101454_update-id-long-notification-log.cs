using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class updateidlongnotificationlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InAppNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NotificationLogId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InAppNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "NotificationLogs",
               columns: table => new
               {
                   Id = table.Column<long>(type: "bigint", nullable: false)
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
            migrationBuilder.DropTable(
                name: "InAppNotifications");

            migrationBuilder.DropForeignKey(
               name: "FK_InAppNotifications_NotificationLogs_NotificationLogId",
               table: "InAppNotifications");

            migrationBuilder.DropTable(
                name: "NotificationLogs");
        }
    }
}
