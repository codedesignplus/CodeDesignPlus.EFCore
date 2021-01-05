using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.EFCore.Sample.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.EfCore.Sample.Api.SqlServer.EntityConfiguration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ConfigurationBase<long, string, Category>();

            builder.ToTable("Categories");
            builder.Property(x => x.Name).HasColumnType("varchar(64)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(254)").IsRequired();

            builder.Property(x => x.IdUserCreator).IsRequired(false);
        }
    }
}
