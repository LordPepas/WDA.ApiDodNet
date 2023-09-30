using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDA.ApiDodNet.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEFKsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Books_Book_id",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Users_User_id",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Rentals",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Return_date",
                table: "Rentals",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "Rental_date",
                table: "Rentals",
                newName: "RentalDate");

            migrationBuilder.RenameColumn(
                name: "Prevision_date",
                table: "Rentals",
                newName: "PrevisionDate");

            migrationBuilder.RenameColumn(
                name: "Book_id",
                table: "Rentals",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_User_id",
                table: "Rentals",
                newName: "IX_Rentals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_Book_id",
                table: "Rentals",
                newName: "IX_Rentals_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Books_BookId",
                table: "Rentals",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Users_UserId",
                table: "Rentals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Books_BookId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Users_UserId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rentals",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Rentals",
                newName: "Return_date");

            migrationBuilder.RenameColumn(
                name: "RentalDate",
                table: "Rentals",
                newName: "Rental_date");

            migrationBuilder.RenameColumn(
                name: "PrevisionDate",
                table: "Rentals",
                newName: "Prevision_date");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Rentals",
                newName: "Book_id");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                newName: "IX_Rentals_User_id");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_BookId",
                table: "Rentals",
                newName: "IX_Rentals_Book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Books_Book_id",
                table: "Rentals",
                column: "Book_id",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Users_User_id",
                table: "Rentals",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
