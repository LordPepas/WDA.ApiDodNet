using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Infra.Data.Maps
{
    public class BooksMapping : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);

            builder.Property(c => c.PublisherId)
               .IsRequired();
            builder.HasOne(e => e.Publisher);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(x => x.Author)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(x => x.Release)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(x => x.Rented)
             .IsRequired()
             .HasColumnType("integer");
        }
    }
}
