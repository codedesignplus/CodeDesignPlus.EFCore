using CodeDesignPlus.EFCore.Options;
using CodeDesignPlus.EFCore.Test.Helpers.Extensions;
using System.Linq;
using Xunit;

namespace CodeDesignPlus.EFCore.Test.Options
{
    /// <summary>
    /// Unit tests to the ClaimsOption class
    /// </summary>
    public class ClaimsOptionTest
    {
        /// <summary>
        /// Claims available to obtain user information
        /// </summary>
        private readonly ClaimsOption claimsOption = new ClaimsOption()
        {
            Email = nameof(ClaimsOption.Email),
            IdUser = nameof(ClaimsOption.IdUser),
            Role = nameof(ClaimsOption.Role),
            User = nameof(ClaimsOption.User),
        };

        /// <summary>
        /// Validate accessors and data annotations
        /// </summary>
        [Fact]
        public void Properties_AccessorsAndDataAnnotations_IsValid()
        {
            // Act
            var results = this.claimsOption.Validate();

            // Assert
            Assert.Empty(results);
            Assert.Equal(nameof(ClaimsOption.Email), this.claimsOption.Email);
            Assert.Equal(nameof(ClaimsOption.IdUser), this.claimsOption.IdUser);
            Assert.Equal(nameof(ClaimsOption.Role), this.claimsOption.Role);
            Assert.Equal(nameof(ClaimsOption.User), this.claimsOption.User);
        }

        /// <summary>
        /// Validate accessors and data annotations
        /// </summary>
        [Theory]
        [InlineData(nameof(ClaimsOption.Email))]
        [InlineData(nameof(ClaimsOption.IdUser))]
        [InlineData(nameof(ClaimsOption.Role))]
        [InlineData(nameof(ClaimsOption.User))]
        public void Properties_SetNullProperty_PropertyNullIsNotValid(string property)
        {
            // Arrange
            this.claimsOption.GetType().GetProperty(property).SetValue(this.claimsOption, null);

            // Act
            var results = this.claimsOption.Validate();

            // Assert
            Assert.NotEmpty(results);
            //The Email field is required.
            Assert.Equal($"The {property} field is required.", results.Select(x => x.ErrorMessage).FirstOrDefault());
        }
    }
}
