using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.EFCore.Sample.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.EfCore.Sample.Api.SqlServer.EntityConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigurationBase<long, string, Product>();

            builder.ToTable("Products");
            builder.Property(x => x.Name).HasColumnType("varchar(64)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(254)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("numeric(18,2)").IsRequired();
            builder.Property(x => x.IdUserCreator).IsRequired(false);

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.IdCategory).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
