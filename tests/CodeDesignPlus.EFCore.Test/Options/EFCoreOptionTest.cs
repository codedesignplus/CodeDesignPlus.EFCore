using CodeDesignPlus.EFCore.Options;
using CodeDesignPlus.EFCore.Test.Helpers.Extensions;
using System.Linq;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Options
{
    /// <summary>
    /// Unit tests to the EFCoreOption class
    /// </summary>
    public class EFCoreOptionTest
    {
        /// <summary>
        /// Configuration options for CodeDesignPlus.EFCore
        /// </summary>
        private readonly EFCoreOption efCoreOption = new EFCoreOption()
        {
            ClaimsIdentity = new ClaimsOption()
            {
                Email = nameof(ClaimsOption.Email),
                IdUser = nameof(ClaimsOption.IdUser),
                Role = nameof(ClaimsOption.Role),
                User = nameof(ClaimsOption.User),
            }
        };

        /// <summary>
        /// Validate accessors and data annotations
        /// </summary>
        [Fact]
        public void Properties_AccessorsAndDataAnnotations_IsValid()
        {
            // Act
            var results = this.efCoreOption.Validate();

            // Assert
            Assert.Empty(results);
            Assert.Equal(nameof(ClaimsOption.Email), this.efCoreOption.ClaimsIdentity.Email);
            Assert.Equal(nameof(ClaimsOption.IdUser), this.efCoreOption.ClaimsIdentity.IdUser);
            Assert.Equal(nameof(ClaimsOption.Role), this.efCoreOption.ClaimsIdentity.Role);
            Assert.Equal(nameof(ClaimsOption.User), this.efCoreOption.ClaimsIdentity.User);
        }

        /// <summary>
        /// Validate accessors and data annotations
        /// </summary>
        [Theory]
        [InlineData(nameof(EFCoreOption.ClaimsIdentity))]
        public void Properties_SetNullProperty_PropertyNullIsNotValid(string property)
        {
            // Arrange
            this.efCoreOption.GetType().GetProperty(property).SetValue(this.efCoreOption, null);

            // Act
            var results = this.efCoreOption.Validate();

            // Assert
            Assert.NotEmpty(results);
            //The Email field is required.
            Assert.Equal($"The {property} field is required.", results.Select(x => x.ErrorMessage).FirstOrDefault());
        }
    }
}
