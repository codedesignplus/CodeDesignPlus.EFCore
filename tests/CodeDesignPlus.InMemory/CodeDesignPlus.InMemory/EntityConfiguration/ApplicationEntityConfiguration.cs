using CodeDesignPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeDesignPlus.InMemory.EntityConfiguration
{
    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnType("varchar(64)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(512)").IsRequired();
            builder.Property(x => x.IdUserCreator).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
        }
    }
}
