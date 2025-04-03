using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Illustrations");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Illustrations",
                newName: "IllusrtationPath");

            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "Illustrations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "Illustrations");

            migrationBuilder.RenameColumn(
                name: "IllusrtationPath",
                table: "Illustrations",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Illustrations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
