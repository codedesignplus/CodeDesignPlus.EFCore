using CodeDesignPlus.Entities;
using CodeDesignPlus.InMemory.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CodeDesignPlus.InMemory
{
    public class CodeDesignPlusContextInMemory : DbContext
    {
        public CodeDesignPlusContextInMemory([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AppPermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionEntityConfiguration());
        }

        public DbSet<Application> Application { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<AppPermision> AppPermision { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
    }
}
