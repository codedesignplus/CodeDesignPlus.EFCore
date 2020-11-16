using CodeDesignPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.InMemory.EntityConfiguration
{
    public class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IdUserCreator).IsRequired();
            builder.Property(x => x.NameRole).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();

            builder.HasOne(x => x.Permission).WithMany(x => x.RolePermisions).HasForeignKey(x => x.IdPermission);
            builder.HasOne(x => x.Application).WithMany(x => x.RolePermisions).HasForeignKey(x => x.IdApplication);
        }
    }
}
