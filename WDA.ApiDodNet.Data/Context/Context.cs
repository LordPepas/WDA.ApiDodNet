using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Infra.Data.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Rentals> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rentals>()
                .Property(r => r.RentalDate)
                .HasColumnType("date");

            modelBuilder.Entity<Rentals>()
                .Property(r => r.PrevisionDate)
                .HasColumnType("date");

            modelBuilder.Entity<Rentals>()
                .Property(r => r.ReturnDate)
                .HasColumnType("date");

            modelBuilder.Entity<Users>()
                .HasData(new List<Users>{
                    new Users(1, "Lauro", "Fortaleza", "Rua A", "lauro@yahoo.com"),
                    new Users(2, "Roberto", "Crato", "Rua B", "roberto@gmail.com"),
                    new Users(3, "Ronaldo", "Caucaia", "Rua C", "ronaldo@hotmail.com"),
                    new Users(4, "Rodrigo", "Recife", "Rua D", "rodrigo@gmail.com"),
                    new Users(5, "Alexandre", "Rio de Janeiro", "Rua E", "alexandre@yahoo.com"),
                    new Users(6, "Isabela", "Fortaleza", "Rua F", "isabela@gmail.com"),
                    new Users(7, "Pedro", "São Paulo", "Rua G", "pedro@hotmail.com"),
                    new Users(8, "Mariana", "Rio de Janeiro", "Rua H", "mariana@yahoo.com"),
                    new Users(9, "Lucas", "Belo Horizonte", "Rua I", "lucas@gmail.com"),
                    new Users(10, "Amanda", "Recife", "Rua J", "amanda@hotmail.com"),
                });

            modelBuilder.Entity<Books>()
                .HasData(new List<Books>{
                    new Books(1 ,"O Senhor dos Anéis", 1, "J.R.R. Tolkien", 1954, 10,0),
                    new Books(2, "Harry Potter e a Pedra Filosofal", 2, "J.K. Rowling", 1997, 1,0),
                    new Books(3, "Dom Quixote", 3, "Miguel de Cervantes", 1605, 1, 0),
                    new Books(4, "Cem Anos de Solidão",4,  "Gabriel García Márquez", 1954, 12,0),
                    new Books(5, "1984", 5, "George Orwell", 1954, 7,0),
                    new Books(6, "A Revolução dos Bichos", 1, "George Orwell", 1954, 5, 0),
                    new Books(7, "Crime e Castigo",  2,"Fiódor Dostoiévski", 1954, 3, 0),
                    new Books(8, "O Pequeno Príncipe", 3, "Antoine de Saint-Exupéry", 1954, 8, 0),
                    new Books(9, "Memórias Póstumas de Brás Cubas", 4, "Machado de Assis", 1954, 6, 0),
                    new Books(10, "A Metamorfose", 5, "Franz Kafka", 1954, 4, 0),
                });

            modelBuilder.Entity<Publishers>()
                .HasData(new List<Publishers>{
                    new Publishers(1, "Companhia das Letras", "São Paulo"),
                    new Publishers(2, "Aleph", "Rio de Janeiro"),
                    new Publishers(3, "Editora Intrínseca", "Rio De Janeiro"),
                    new Publishers(4, "Editora Rocco", "Rio de Janeiro"),
                    new Publishers(5, "Darkside", "Porto Alegre"),
                    new Publishers(6, "Harper Collins", "Nova Iorque"),
                    new Publishers(7, "Editora Arqueiro", "Rio de Janeiro"),
                    new Publishers(8, "Leya", "Lisboa"),
                    new Publishers(9, "Saraiva", "São Paulo"),
                    new Publishers(10, "Sextante", "Porto Alegre"),
                });
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
