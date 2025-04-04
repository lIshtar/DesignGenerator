using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IllusrtationPath",
                table: "Illustrations",
                newName: "IllustrationPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IllustrationPath",
                table: "Illustrations",
                newName: "IllusrtationPath");
        }
    }
}
