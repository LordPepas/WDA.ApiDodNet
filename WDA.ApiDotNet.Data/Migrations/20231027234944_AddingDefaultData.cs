using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WDA.ApiDodNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "City", "Name" },
                values: new object[,]
                {
                    { 1, "São Paulo", "Companhia das Letras" },
                    { 2, "Rio de Janeiro", "Aleph" },
                    { 3, "Rio De Janeiro", "Editora Intrínseca" },
                    { 4, "Rio de Janeiro", "Editora Rocco" },
                    { 5, "Porto Alegre", "Darkside" },
                    { 6, "Nova Iorque", "Harper Collins" },
                    { 7, "Rio de Janeiro", "Editora Arqueiro" },
                    { 8, "Lisboa", "Leya" },
                    { 9, "São Paulo", "Saraiva" },
                    { 10, "Porto Alegre", "Sextante" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "City", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Rua A", "Fortaleza", "lauro@yahoo.com", "Lauro" },
                    { 2, "Rua B", "Crato", "roberto@gmail.com", "Roberto" },
                    { 3, "Rua C", "Caucaia", "ronaldo@hotmail.com", "Ronaldo" },
                    { 4, "Rua D", "Recife", "rodrigo@gmail.com", "Rodrigo" },
                    { 5, "Rua E", "Rio de Janeiro", "alexandre@yahoo.com", "Alexandre" },
                    { 6, "Rua F", "Fortaleza", "isabela@gmail.com", "Isabela" },
                    { 7, "Rua G", "São Paulo", "pedro@hotmail.com", "Pedro" },
                    { 8, "Rua H", "Rio de Janeiro", "mariana@yahoo.com", "Mariana" },
                    { 9, "Rua I", "Belo Horizonte", "lucas@gmail.com", "Lucas" },
                    { 10, "Rua J", "Recife", "amanda@hotmail.com", "Amanda" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "PublisherId", "Quantity", "Release", "Rented" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", "O Senhor dos Anéis", 1, 10, 1954, 0 },
                    { 2, "J.K. Rowling", "Harry Potter e a Pedra Filosofal", 2, 1, 1997, 0 },
                    { 3, "Miguel de Cervantes", "Dom Quixote", 3, 1, 1605, 0 },
                    { 4, "Gabriel García Márquez", "Cem Anos de Solidão", 4, 12, 1954, 0 },
                    { 5, "George Orwell", "1984", 5, 7, 1954, 0 },
                    { 6, "George Orwell", "A Revolução dos Bichos", 1, 5, 1954, 0 },
                    { 7, "Fiódor Dostoiévski", "Crime e Castigo", 2, 3, 1954, 0 },
                    { 8, "Antoine de Saint-Exupéry", "O Pequeno Príncipe", 3, 8, 1954, 0 },
                    { 9, "Machado de Assis", "Memórias Póstumas de Brás Cubas", 4, 6, 1954, 0 },
                    { 10, "Franz Kafka", "A Metamorfose", 5, 4, 1954, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
