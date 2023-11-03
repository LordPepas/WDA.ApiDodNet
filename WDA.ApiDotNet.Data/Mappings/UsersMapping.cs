using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDotNet.Business.Models;

namespace WDA.ApiDotNet.Data.Maps
{
    public class UsersMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {


            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                  .IsRequired()
                .HasColumnType("text");
            builder.Property(c => c.City)
                  .IsRequired()
                .HasColumnType("text");
            builder.Property(c => c.Address)
                 .IsRequired()
                .HasColumnType("text");
            builder.Property(c => c.Email)
                  .IsRequired()
                .HasColumnType("text");

            builder.ToTable("Users");
        }
    }
}
