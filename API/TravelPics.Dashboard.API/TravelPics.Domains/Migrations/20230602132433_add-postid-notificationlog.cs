﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    /// <inheritdoc />
    public partial class addpostidnotificationlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "NotificationLogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "NotificationLogs");
        }
    }
}
