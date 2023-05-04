using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class addprofilepictodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfileImageId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Location",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,10)",
                oldPrecision: 12,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Location",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,10)",
                oldPrecision: 12,
                oldScale: 10);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileImage",
                table: "Documents",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Documents_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Documents_ProfileImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsProfileImage",
                table: "Documents");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Location",
                type: "decimal(12,10)",
                precision: 12,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Location",
                type: "decimal(12,10)",
                precision: 12,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6);
        }
    }
}
