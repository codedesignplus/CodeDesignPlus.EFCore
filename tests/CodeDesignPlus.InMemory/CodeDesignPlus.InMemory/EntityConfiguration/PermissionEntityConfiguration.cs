using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.InMemory.EntityConfiguration
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        /// <summary>
        /// Control property for unit tests
        /// </summary>
        public static bool IsInvoked;

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            IsInvoked = true;

            builder.ConfigurationBase<long, int, Permission>();

            builder.Property(x => x.Name).HasColumnType("varchar(64)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(512)").IsRequired();
            builder.Property(x => x.Controller).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.Action).HasColumnType("varchar(32)").IsRequired();
        }
    }
}
