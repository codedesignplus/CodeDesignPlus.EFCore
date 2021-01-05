using CodeDesignPlus.EFCore.Extensions;
using CodeDesignPlus.EFCore.Middleware;
using CodeDesignPlus.EFCore.Options;
using CodeDesignPlus.EFCore.Repository;
using CodeDesignPlus.Entities;
using CodeDesignPlus.InMemory;
using CodeDesignPlus.InMemory.EntityConfiguration;
using CodeDesignPlus.InMemory.Repositories;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Extensions
{
    /// <summary>
    /// Unit tests to the EFCoreExtensions class
    /// </summary>
    public class EFCoreExtensionsTest
    {
        /// <summary>
        /// Validate that the EFCoreExtensions.ConfigurationBase extension method assigns the base configurations to the entity
        /// </summary>
        [Fact]
        public void ConfigurationBase_ValidateConfigProperties_ConfigDefaults()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(EFCoreExtensionsTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var modelBuilder = new ModelBuilder(ConventionSet.CreateConventionSet(context));

            var entityTypeBuilder = modelBuilder.Entity<Permission>();

            var customerEntityConfiguration = new PermissionEntityConfiguration();

            customerEntityConfiguration.Configure(entityTypeBuilder);

            // Act
            var idProperty = entityTypeBuilder.Metadata.FindDeclaredProperty(nameof(Permission.Id));
            var idUserCreatorProperty = entityTypeBuilder.Metadata.FindDeclaredProperty(nameof(Permission.IdUserCreator));
            var stateProperty = entityTypeBuilder.Metadata.FindDeclaredProperty(nameof(Permission.State));
            var dateCreatedProperty = entityTypeBuilder.Metadata.FindDeclaredProperty(nameof(Permission.DateCreated));

            // Assert
            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(idProperty.IsNullable);
            Assert.Equal(ValueGenerated.OnAdd, idProperty.ValueGenerated);
            Assert.False(idUserCreatorProperty.IsNullable);
            Assert.False(stateProperty.IsNullable);
            Assert.False(dateCreatedProperty.IsNullable);
        }

        /// <summary>
        /// Validate that the EFCoreExtensions.ToPageAsync extension method returns the default object
        /// </summary>
        [Fact]
        public async Task ToPageAsync_ArgumentsInvalid_Null()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(EFCoreExtensionsTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var pager = await repository.GetEntity<Application>().ToPageAsync(0, 0);

            // Assert
            Assert.Null(pager);
        }

        /// <summary>
        /// Validate that the EFCoreExtensions.ToPageAsync extension method returns the Pager object with the information
        /// </summary>
        [Fact]
        public async Task ToPageAsync_PageFromDb_Pager()
        {
            // Arrange
            var currentPage = 1;
            var pageSize = 10;
            var totalItems = 500;
            var maxPages = 10;
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(EFCoreExtensionsTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var applications = new List<Application>();

            for (int i = 0; i < totalItems; i++)
            {
                applications.Add(new Application()
                {
                    Name = $"{nameof(Application.Name)}-{i}",
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = $"{nameof(Application.Description)}-{i}"
                });
            }

            await repository.CreateRangeAsync(applications);

            // Act
            var pager = await repository.GetEntity<Application>().ToPageAsync(currentPage, pageSize);

            // Assert
            Assert.Equal(totalItems, pager.TotalItems);
            Assert.Equal(currentPage, pager.CurrentPage);
            Assert.Equal(pageSize, pager.PageSize);
            Assert.Equal(totalPages, pager.TotalPages);
            Assert.Equal(pager.Pages.Min(), pager.StartPage);
            Assert.Equal(pager.Pages.Max(), pager.EndPage);
            Assert.Equal(maxPages, pager.Pages.Count());
            Assert.Equal(startIndex, pager.StartIndex);
            Assert.Equal(endIndex, pager.EndIndex);
        }

        /// <summary>
        /// Validate that the EFCoreExtensions.AddRepositories method scans and registers the repositories in the ServiceCollection
        /// </summary>
        [Fact]
        public void AddRepositories_ScanAndRegisterWithTransient_AddRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddRepositories<long, int, CodeDesignPlusContextInMemory>();

            // Assert
            var repository = serviceCollection.Where(x => typeof(IRepositoryBase<long, int>).IsAssignableFrom(x.ServiceType)).ToList();

            repository.ForEach(x => Assert.Equal(ServiceLifetime.Transient, x.Lifetime));
        }

        /// <summary>
        /// Validate that the settings are registered correctly
        /// </summary>
        [Fact]
        public void AddEfCore_RegisterConfigurations_EfCoreOption()
        {
            // Arrange
            var section = "EFCore";
            var configBuilder = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { $"{section}:{nameof(EFCoreOption.ClaimsIdentity)}:{nameof(ClaimsOption.User)}", "user"},
                    { $"{section}:{nameof(EFCoreOption.ClaimsIdentity)}:{nameof(ClaimsOption.IdUser)}", "sub"},
                    { $"{section}:{nameof(EFCoreOption.ClaimsIdentity)}:{nameof(ClaimsOption.Email)}", "email"},
                    { $"{section}:{nameof(EFCoreOption.ClaimsIdentity)}:{nameof(ClaimsOption.Role)}", "role"},
                });

            /*
             * appsettings.json
             * {
             *      "EFCore": {
             *          "ClaimsIdentity": {
             *              "User": "user",
             *              "IdUser": "sub",
             *              "Email": "email",
             *              "Role": "role"
             *          }
             *      }
             * }
             */

            var configuration = configBuilder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            // Act 
            services.AddEFCore(configuration, section);

            // Assert
            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetService<IOptions<EFCoreOption>>();
            var efCoreOption = options.Value;

            Assert.NotNull(efCoreOption);
            Assert.Equal("user", efCoreOption.ClaimsIdentity.User);
            Assert.Equal("email", efCoreOption.ClaimsIdentity.Email);
            Assert.Equal("sub", efCoreOption.ClaimsIdentity.IdUser);
            Assert.Equal("role", efCoreOption.ClaimsIdentity.Role);
        }

        /// <summary>
        /// Validate that the services are registered to obtain and assign user information
        /// </summary>
        [Fact]
        public void AddIdentityService_RegisterServices_Services()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddIdentityService<string>();

            // Assert
            var httpContextAccessor = services.FirstOrDefault(x => typeof(IHttpContextAccessor).IsAssignableFrom(x.ServiceType));
            var authenticateUser = services.FirstOrDefault(x => typeof(IAuthenticateUser<string>).IsAssignableFrom(x.ServiceType));
            var identityService = services.FirstOrDefault(x => typeof(IIdentityService<string>).IsAssignableFrom(x.ServiceType));

            Assert.NotNull(httpContextAccessor);
            Assert.NotNull(authenticateUser);
            Assert.NotNull(identityService);
            Assert.Equal(ServiceLifetime.Singleton, httpContextAccessor.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, authenticateUser.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, identityService.Lifetime);
        }

        /// <summary>
        /// Validate that middleware is registered for invocation
        /// </summary>
        [Fact]
        public void UseIdentityService_RegisterMiddleware_Middleware()
        {
            // Arrange
            var services = new ServiceCollection();

            var applicationBuilder = new ApplicationBuilder(services.BuildServiceProvider());

            // Act
            applicationBuilder.UseIdentityService<int>();

            // Assert
            var app = applicationBuilder.Build();

            Assert.Contains("Middleware", app.Target.ToString());
        }

        /// <summary>
        /// Validate that the EFCoreExtensions.RegisterEntityConfigurations method scans, instance and 
        /// invokes the Configure method of the classes that implement the IEntityTypeConfiguration <TEntity> interface
        /// </summary>
        [Fact]
        public void RegisterEntityConfigurations_ScanAndInvokeConfigure_EntityConfigurationInvoked()
        {
            var modelBuilder = new ModelBuilder();

            modelBuilder.RegisterEntityConfigurations<CodeDesignPlusContextInMemory>();

            Assert.True(ApplicationEntityConfiguration.IsInvoked);
            Assert.True(AppPermissionEntityConfiguration.IsInvoked);
            Assert.True(PermissionEntityConfiguration.IsInvoked);
            Assert.True(RolePermissionEntityConfiguration.IsInvoked);
        }
    }
}
