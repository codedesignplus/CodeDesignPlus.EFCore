using CodeDesignPlus.InMemory;
using CodeDesignPlus.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Repository
{
    public class RepositoryBaseTest
    {
        [Fact]
        public void GetContext_CastContext()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<CodeDesignPlusContextInMemory>();

            var options = builder.UseInMemoryDatabase(nameof(RepositoryBaseTest)).Options;

            var context = new CodeDesignPlusContextInMemory(options);

            var applicationRepository = new ApplicationRepository(context);

            // Act
            var result = applicationRepository.GetContext<CodeDesignPlusContextInMemory>();

            // Assert
            Assert.IsType<CodeDesignPlusContextInMemory>(result);
        }
    }
}
