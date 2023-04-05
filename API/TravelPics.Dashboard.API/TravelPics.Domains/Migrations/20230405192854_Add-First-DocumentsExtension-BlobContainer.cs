using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstDocumentsExtensionBlobContainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DocumentExtensions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DocumentBlobContainers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);


            var sql = @"
                BEGIN
                    INSERT INTO DocumentExtensions(Extension,Description,ContentType)
                VALUES ('jpg','JPG Image','image/jpeg'),('jpeg','JPEG Image','image/jpeg'),('png','PNG image','image/png')

                    INSERT INTO DocumentBlobContainers(Name,ContainerName)
	                VALUES('travelpicsimages','travelpicsimages')
                END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
