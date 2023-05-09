using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class addinappnotificationentitywithstatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "InAppNotifications",
                newName: "StatusId");

            migrationBuilder.CreateTable(
                name: "NotificationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InAppNotifications_StatusId",
                table: "InAppNotifications",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_InAppNotifications_NotificationStatus_StatusId",
                table: "InAppNotifications",
                column: "StatusId",
                principalTable: "NotificationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            var sql = @"
                BEGIN
                    INSERT INTO NotificationStatus(Name)
	                VALUES('Created'),('Sent'),('Received'),('Read'),('Failed');
                END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InAppNotifications_NotificationStatus_StatusId",
                table: "InAppNotifications");

            migrationBuilder.DropTable(
                name: "NotificationStatus");

            migrationBuilder.DropIndex(
                name: "IX_InAppNotifications_StatusId",
                table: "InAppNotifications");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "InAppNotifications",
                newName: "Status");
        }
    }
}
