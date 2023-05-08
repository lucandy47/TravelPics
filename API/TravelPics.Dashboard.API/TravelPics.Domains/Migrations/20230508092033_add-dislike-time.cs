using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class adddisliketime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DislikedOn",
                table: "Likes",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikedOn",
                table: "Likes");
        }
    }
}
