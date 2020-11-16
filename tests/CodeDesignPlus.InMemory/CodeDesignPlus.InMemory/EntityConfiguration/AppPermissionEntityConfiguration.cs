using CodeDesignPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.InMemory.EntityConfiguration
{
    public class AppPermissionEntityConfiguration : IEntityTypeConfiguration<AppPermision>
    {
        public void Configure(EntityTypeBuilder<AppPermision> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdUserCreator).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();

            builder.HasOne(x => x.Permission).WithMany(x => x.AppPermisions).HasForeignKey(x => x.IdPermission);
            builder.HasOne(x => x.Application).WithMany(x => x.AppPermisions).HasForeignKey(x => x.IdApplication);
        }
    }
}
