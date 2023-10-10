using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Models;

//configurar as entidades (tabelas) do banco de dados com os modelos de dados definidos
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

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
