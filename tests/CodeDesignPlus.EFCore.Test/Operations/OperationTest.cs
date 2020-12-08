using CodeDesignPlus.EFCore.Model;
using CodeDesignPlus.Entities;
using CodeDesignPlus.InMemory;
using CodeDesignPlus.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Operations
{
    public class OperationTest
    {
        [Fact]
        public async Task CreateAsync_CreateEntity_ReturnId()
        {
            // Arrange
            var permission = new Permission()
            {
                Name = "Create - Permissions",
                Description = "Can create to permissions",
                Controller = "Permission",
                Action = "Post",
                State = true
            };

            var authenticateUser = new AuthenticateUser<int>()
            {
                Name = "codedesignplus",
                IdUser = new Random().Next(0, int.MaxValue),
                IsAuthenticated = true,
                IsApplication = false,
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(OperationTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new PermissionRepository(authenticateUser, context);

            // Act
            var id = await repository.CreateAsync(permission);

            // Assert
            Assert.Equal(permission.Id, id);
            Assert.Equal("Create - Permissions", permission.Name);
            Assert.Equal("Can create to permissions", permission.Description);
            Assert.Equal("Permission", permission.Controller);
            Assert.Equal("Post", permission.Action);
            Assert.True(permission.State);
            Assert.Equal(authenticateUser.IdUser, permission.IdUserCreator);
            Assert.True(permission.DateCreated > DateTime.MinValue);
        }


        [Fact]
        public async Task UpdateAsync_UpdateEntity_ReturnTrue()
        {
            // Arrange
            var permission = new Permission()
            {
                Name = "Create - Permissions",
                Description = "Can create to permissions",
                Controller = "Permission",
                Action = "Post",
                State = true
            };

            var authenticateUser = new AuthenticateUser<int>()
            {
                Name = "codedesignplus",
                IdUser = new Random().Next(0, int.MaxValue),
                IsAuthenticated = true,
                IsApplication = false,
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(OperationTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new PermissionRepository(authenticateUser, context);

            var id = await repository.CreateAsync(permission);

            // Act
            var entityUpdate = new Permission()
            {
                Name = "Update - Permissions",
                Description = "Can update to permissions",
                Controller = "Permission",
                Action = "Put",
                State = false
            };

            var success = await repository.UpdateAsync(id, entityUpdate);

            // Assert
            var entity = await repository.GetEntity<Permission>().FindAsync(id);

            Assert.True(success);
            Assert.Equal(id, entity.Id);
            Assert.Equal("Update - Permissions", entity.Name);
            Assert.Equal("Can update to permissions", entity.Description);
            Assert.Equal("Permission", entity.Controller);
            Assert.Equal("Put", entity.Action);
            Assert.False(entity.State);
            Assert.Equal(authenticateUser.IdUser, entity.IdUserCreator);
            Assert.Equal(permission.DateCreated, entity.DateCreated);
        }

        [Fact]
        public async Task UpdateAsync_EntityNotExist_ReturnFalse()
        {
            // Arrange
            var authenticateUser = new AuthenticateUser<int>()
            {
                Name = "codedesignplus",
                IdUser = new Random().Next(0, int.MaxValue),
                IsAuthenticated = true,
                IsApplication = false,
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(OperationTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new PermissionRepository(authenticateUser, context);

            // Act
            var entityUpdate = new Permission()
            {
                Name = "Update - Permissions",
                Description = "Can update to permissions",
                Controller = "Permission",
                Action = "Put",
                State = false
            };

            var success = await repository.UpdateAsync(new Random().Next(1, int.MaxValue), entityUpdate);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async Task DeleteAsync_DeleteEntity_ReturnTrue()
        {
            // Arrange
            var permission = new Permission()
            {
                Name = "Create - Permissions",
                Description = "Can create to permissions",
                Controller = "Permission",
                Action = "Post",
                State = true
            };

            var authenticateUser = new AuthenticateUser<int>()
            {
                Name = "codedesignplus",
                IdUser = new Random().Next(0, int.MaxValue),
                IsAuthenticated = true,
                IsApplication = false,
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(OperationTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new PermissionRepository(authenticateUser, context);

            var id = await repository.CreateAsync(permission);

            // Act
            var success = await repository.DeleteAsync(id);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async Task DeleteAsync_EntityNotExist_ReturnFalse()
        {
            // Arrange
            var authenticateUser = new AuthenticateUser<int>()
            {
                Name = "codedesignplus",
                IdUser = new Random().Next(0, int.MaxValue),
                IsAuthenticated = true,
                IsApplication = false,
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(OperationTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new PermissionRepository(authenticateUser, context);

            // Act
            var success = await repository.DeleteAsync(new Random().Next(1, int.MaxValue));

            // Assert
            Assert.False(success);
        }
    }
}
