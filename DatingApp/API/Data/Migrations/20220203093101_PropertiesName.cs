using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class PropertiesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LestActive",
                table: "Users",
                newName: "LastActive");

            migrationBuilder.RenameColumn(
                name: "KnowAs",
                table: "Users",
                newName: "KnownAs");

            migrationBuilder.RenameColumn(
                name: "Introdution",
                table: "Users",
                newName: "Introduction");

            migrationBuilder.RenameColumn(
                name: "Interested",
                table: "Users",
                newName: "Interests");

            migrationBuilder.RenameColumn(
                name: "BirtheDay",
                table: "Users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Photos",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastActive",
                table: "Users",
                newName: "LestActive");

            migrationBuilder.RenameColumn(
                name: "KnownAs",
                table: "Users",
                newName: "KnowAs");

            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "Users",
                newName: "Introdution");

            migrationBuilder.RenameColumn(
                name: "Interests",
                table: "Users",
                newName: "Interested");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "BirtheDay");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "URL");
        }
    }
}
