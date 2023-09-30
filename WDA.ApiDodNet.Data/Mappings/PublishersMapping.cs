using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WDA.ApiDodNet.Data.Models;

//Mapeamento da tabela publishers presente no banco de dados
namespace WDA.ApiDotNet6.Infra.Data.Maps
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
