using CodeDesignPlus.Entities;
using CodeDesignPlus.InMemory;
using CodeDesignPlus.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Repository
{
    /// <summary>
    /// Unit tests to the RepositoryBase class
    /// </summary>
    public class RepositoryBaseTest
    {
        /// <summary>
        /// Validate that an exception is thrown when the argument is null
        /// </summary>
        [Fact]
        public void Constructor_ArgumentIsNull_ArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ApplicationRepository(null));

            Assert.Equal("Value cannot be null. (Parameter 'context')", exception.Message);
        }

        /// <summary>
        /// Validate that the context can be obtained
        /// </summary>
        [Fact]
        public void GetContext_CastContext()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var result = repository.GetContext<CodeDesignPlusContextInMemory>();

            // Assert
            Assert.IsType<CodeDesignPlusContextInMemory>(result);
        }

        /// <summary>
        /// Validate that the entity can be obtained
        /// </summary>
        [Fact]
        public void GetEntity_EntityExist_NotNull()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var dbset = repository.GetEntity<Application>();

            // Assert 
            Assert.NotNull(dbset);
            Assert.Equal(nameof(Application), dbset.EntityType.FullName());
        }

        /// <summary>
        /// Validate exception to be thrown when entity cannot be obtained
        /// </summary>
        [Fact]
        public void GetEntity_EntityNotExist_Exception()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var dbset = repository.GetEntity<FakeEntity>();

            // Assert 
            Assert.NotNull(dbset);

            var exception = Assert.Throws<InvalidOperationException>(() => dbset.EntityType);

            Assert.Equal($"Cannot create a DbSet for '{nameof(FakeEntity)}' because this type is not included in the model for the context.", exception.Message);
        }

        /// <summary>
        /// Validate that an exception is generated when the entity is null
        /// </summary>
        [Fact]
        public async Task CreateAsync_EntityIsNull_ArgumentNullException()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateAsync<Application>(null));

            Assert.Equal("Value cannot be null. (Parameter 'entity')", exception.Message);
        }

        /// <summary>
        /// Validate that the record id is generated
        /// </summary>
        [Fact]
        public async Task CreateAsync_EntityIsNotNull_IdIsGreeaterThanZero()
        {
            // Arrange 
            var entity = new Application()
            {
                Name = nameof(Application.Name),
                IdUserCreator = new Random().Next(1, 15),
                State = true,
                DateCreated = DateTime.Now,
                Description = nameof(Application.Description)
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act 
            var result = await repository.CreateAsync(entity);

            // Assert
            Assert.True(result.Id > 0);
            Assert.Equal(nameof(Application.Name), result.Name);
            Assert.Equal(nameof(Application.Description), result.Description);
            Assert.Equal(entity.IdUserCreator, result.IdUserCreator);
            Assert.Equal(entity.State, result.State);
            Assert.Equal(entity.DateCreated, result.DateCreated);
        }

        /// <summary>
        /// Validate exception to be thrown when entity is null
        /// </summary>
        [Fact]
        public async Task UpdateAsync_EntityIsNull_ArgumentNullException()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateAsync<Application>(null));

            Assert.Equal("Value cannot be null. (Parameter 'entity')", exception.Message);
        }

        /// <summary>
        /// Validate that the record is updated based on the id and the new information assigned
        /// </summary>
        [Fact]
        public async Task UpdateAsync_AssignUpdateInfo_Success()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var applicationCreated = await repository.CreateAsync(new Application()
            {
                Name = nameof(Application.Name),
                IdUserCreator = new Random().Next(1, 15),
                State = true,
                DateCreated = DateTime.Now,
                Description = nameof(Application.Description)
            });

            // Act
            var applicationUpdate = await repository.GetEntity<Application>().FirstOrDefaultAsync(x => x.Id == applicationCreated.Id);

            applicationUpdate.Description = "New Description";
            applicationUpdate.Name = "New Name";
            applicationUpdate.DateCreated = DateTime.MaxValue;
            applicationUpdate.State = false;
            applicationUpdate.IdUserCreator = 100;

            var success = await repository.UpdateAsync(applicationUpdate);

            // Assert
            var result = await repository.GetEntity<Application>().FirstOrDefaultAsync(x => x.Id == applicationUpdate.Id);

            Assert.True(success);
            Assert.Equal("New Name", result.Name);
            Assert.Equal("New Description", result.Description);
            Assert.False(result.State);
            Assert.Equal(applicationCreated.IdUserCreator, result.IdUserCreator);
            Assert.Equal(applicationCreated.DateCreated, result.DateCreated);
        }

        /// <summary>
        /// Validate that the exception is thrown when the entity is null
        /// </summary>
        [Fact]
        public async Task DeleteAsync_EntityIsNull_ArgumentNullException()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.DeleteAsync<Application>(null));

            Assert.Equal("Value cannot be null. (Parameter 'predicate')", exception.Message);
        }

        /// <summary>
        /// Validate that it returns false when the record does not exist
        /// </summary>
        [Fact]
        public async Task DeleteAsync_EntityNotExist_False()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var success = await repository.DeleteAsync<Application>(x => x.Id == 10);

            // Assert
            Assert.False(success);
        }

        /// <summary>
        /// Validate that an entity can be removed and return true if the record exists
        /// </summary>
        [Fact]
        public async Task DeleteAsync_EntityExist_True()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var applicationCreated = await repository.CreateAsync(new Application()
            {
                Name = nameof(Application.Name),
                IdUserCreator = new Random().Next(1, 15),
                State = true,
                DateCreated = DateTime.Now,
                Description = nameof(Application.Description)
            });

            // Act
            var success = await repository.DeleteAsync<Application>(x => x.Id == applicationCreated.Id);

            // Assert
            Assert.True(success);
        }

        /// <summary>
        /// Validate that a recordset cannot be created if the list is empty
        /// </summary>
        [Fact]
        public async Task CraeteRangeAsync_ListEmpty_ReturnListEmpty()
        {
            // Arrange 
            var entities = new List<Application>();

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var result = await repository.CreateRangeAsync(entities);

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Validate that a set of records can be created and the id is assigned
        /// </summary>
        [Fact]
        public async Task CreateRangeAsync_ListWithData_ReturnListAndIds()
        {
            // Arrange 
            var entities = new List<Application>
            {
                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                },
                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                }
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var result = await repository.CreateRangeAsync(entities);

            // Assert
            Assert.Contains(result, x => x.Id > 0);
        }

        /// <summary>
        /// Validate that false is returned when the list is empty
        /// </summary>
        [Fact]
        public async Task UpdateRangeAsync_ListEmpty_ReturnFalse()
        {
            // Arrange 
            var entities = new List<Application>();

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var success = await repository.UpdateRangeAsync(entities);

            // Assert
            Assert.False(success);
        }

        /// <summary>
        /// Validate that true is returned when the records assigned to the list are updated
        /// </summary>
        [Fact]
        public async Task UpdateRangeAsync_AssignUpdateInfo_Success()
        {
            // Arrange
            var entities = new List<Application>
            {
                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                },

                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                }
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var entitiesCreated = await repository.CreateRangeAsync(entities);

            // Act
            var entitiesUpdate = await repository.GetEntity<Application>().Where(x => x.State).ToListAsync();

            entitiesUpdate.ForEach(x =>
            {
                x.Description = "New Description";
                x.Name = "New Name";
                x.DateCreated = DateTime.MaxValue;
                x.State = false;
                x.IdUserCreator = 100;
            });

            var success = await repository.UpdateRangeAsync(entitiesUpdate);

            // Assert
            var result = await repository.GetEntity<Application>().Where(x => !x.State).ToListAsync();

            Assert.True(success);

            foreach (var item in result)
            {
                Assert.Equal("New Name", item.Name);
                Assert.Equal("New Description", item.Description);
                Assert.False(item.State);
            }
        }

        /// <summary>
        /// Validate that false is returned when the list is empty
        /// </summary>
        [Fact]
        public async Task DeleteRangeAsync_ListEmpty_ReturnFalse()
        {
            // Arrange 
            var entities = new List<Application>();

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act
            var success = await repository.DeleteRangeAsync(entities);

            // Assert
            Assert.False(success);
        }

        /// <summary>
        /// Validate that true is returned when the records assigned to the list are deleted
        /// </summary>
        [Fact]
        public async Task DeleteRangeAsync_EntityExist_True()
        {
            // Arrange 
            var entities = new List<Application>
            {
                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                },
                new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                }
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var entitiesCreated = await repository.CreateRangeAsync(entities);

            // Act
            var entitiesDelete = await repository.GetEntity<Application>().Where(x => x.State).ToListAsync();

            var success = await repository.DeleteRangeAsync(entitiesDelete);

            // Assert
            Assert.True(success);
        }

        /// <summary>
        /// Validate that false is returned when the record does not exist
        /// </summary>
        [Fact]
        public async Task ChangeStateAsync_EntityNotExist_ReturnFalse()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            // Act 
            var success = await repository.ChangeStateAsync<Application>(1, false);

            // Assert
            Assert.False(success);
        }

        /// <summary>
        /// Validate that true is returned when the record exists
        /// </summary>
        [Fact]
        public async Task ChangeStateAsync_EntityExist_ReturnTrue()
        {
            // Arrange 
            var entity = new Application()
            {
                Name = nameof(Application.Name),
                IdUserCreator = new Random().Next(1, 15),
                State = true,
                DateCreated = DateTime.Now,
                Description = nameof(Application.Description)
            };

            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var repository = new ApplicationRepository(context);

            var entityCreate = await repository.CreateAsync(entity);

            // Act 
            var success = await repository.ChangeStateAsync<Application>(entityCreate.Id, !entityCreate.State);

            // Assert
            Assert.True(success);
        }

        /// <summary>
        /// Validate that multiple processes in the database are processed in a single transaction
        /// </summary>
        [Fact]
        public async Task TransactionAsync_CommitedTransaction_ReturnResultDelegate()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UnitTestTransaction;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

            var context = new CodeDesignPlusContextInMemory(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var repository = new ApplicationRepository(context);

            // Act
            var result = await repository.TransactionAsync<bool>(async context =>
            {
                var applicationCreated = await repository.CreateAsync(new Application()
                {
                    Name = nameof(Application.Name),
                    IdUserCreator = new Random().Next(1, 15),
                    State = true,
                    DateCreated = DateTime.Now,
                    Description = nameof(Application.Description)
                });

                return applicationCreated.Id > 0;
            });

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Validate that multiple processes in the database are rolled back in a single transaction
        /// </summary>
        [Fact]
        public async Task TransactionAsync_RollbackTransaction_InvalidOperationException()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UnitTestTransaction;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

            var context = new CodeDesignPlusContextInMemory(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var repository = new ApplicationRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                var result = await repository.TransactionAsync<bool>(async context =>
                {
                    var applicationCreated = await repository.CreateAsync(new Application()
                    {
                        Name = nameof(Application.Name),
                        IdUserCreator = new Random().Next(1, 15),
                        State = true,
                        DateCreated = DateTime.Now,
                        Description = nameof(Application.Description)
                    });

                    if (applicationCreated.Id > 0)
                    {
                        throw new InvalidOperationException("Failed Transaction");
                    }

                    return applicationCreated.Id > 0;
                });
            });

            // Assert
            Assert.Equal("Failed Transaction", exception.Message);
        }
    }
}
