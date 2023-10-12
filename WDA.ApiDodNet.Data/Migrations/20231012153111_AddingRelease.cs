using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDA.ApiDodNet.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Launch",
                table: "Books",
                newName: "Release");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Release",
                table: "Books",
                newName: "Launch");
        }
    }
}
