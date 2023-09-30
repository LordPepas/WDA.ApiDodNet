using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDodNet.Data.Models;

namespace WDA.ApiDotNet6.Infra.Data.Maps
{
    public class RentalsMapping : IEntityTypeConfiguration<Rentals>
    {
        public void Configure(EntityTypeBuilder<Rentals> builder)
        {
            builder.ToTable("Rentals");
            builder.HasKey(x => x.Id); 

            builder.Property(x => x.BookId) 
                .IsRequired();
            builder.HasOne(e => e.Book);

            builder.Property(x => x.UserId)
                .IsRequired();
            builder.HasOne(e => e.User);

            builder.Property(x => x.RentalDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.PrevisionDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.ReturnDate)
                .IsRequired(false)
                .HasColumnType("date");

        }
    }
}
