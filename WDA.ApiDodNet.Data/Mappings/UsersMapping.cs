using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Infra.Data.Maps
{
    public class UsersMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");


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
        }
    }
}
