using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HomeDevices.Net.Server.Web.Data.Migrations.ApplicationDb
{
    public partial class RuuviTagUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RuuviTags",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RuuviTags",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "RuuviTags",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RuuviTags");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RuuviTags");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "RuuviTags");
        }
    }
}
