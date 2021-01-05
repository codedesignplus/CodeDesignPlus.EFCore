using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.EFCore.Sample.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CodeDesignPlus.EfCore.Sample.Api.SqlServer
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.RegisterEntityConfigurations<SqlServerContext>();
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
