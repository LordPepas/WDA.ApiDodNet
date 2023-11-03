using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDotNet.Business.Models;

namespace WDA.ApiDotNet6.Data.Maps
{
    public class PublishersMapping : IEntityTypeConfiguration<Publishers>
    {
        public void Configure(EntityTypeBuilder<Publishers> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("text");
            builder.Property(c => c.City)
                  .IsRequired()
                .HasColumnType("text");

            builder.ToTable("Publishers");
        }
    }
}
