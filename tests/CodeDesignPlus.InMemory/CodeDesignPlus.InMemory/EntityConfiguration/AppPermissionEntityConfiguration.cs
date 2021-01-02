using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.InMemory.EntityConfiguration
{
    public class AppPermissionEntityConfiguration : IEntityTypeConfiguration<AppPermision>
    {
        /// <summary>
        /// Control property for unit tests
        /// </summary>
        public static bool IsInvoked;

        public void Configure(EntityTypeBuilder<AppPermision> builder)
        {
            IsInvoked = true;

            builder.ConfigurationBase<long, int, AppPermision>();

            builder.HasOne(x => x.Permission).WithMany(x => x.AppPermisions).HasForeignKey(x => x.IdPermission);
            builder.HasOne(x => x.Application).WithMany(x => x.AppPermisions).HasForeignKey(x => x.IdApplication);
        }
    }
}
