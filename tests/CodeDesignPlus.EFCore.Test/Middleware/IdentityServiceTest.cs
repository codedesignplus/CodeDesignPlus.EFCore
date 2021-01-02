using CodeDesignPlus.EFCore.Middleware;
using CodeDesignPlus.EFCore.Options;
using CodeDesignPlus.EFCore.Test.Helpers.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using Xunit;
using MEO = Microsoft.Extensions.Options;

namespace CodeDesignPlus.EFCore.Test.Middleware
{
    /// <summary>
    /// Unit tests to the IdentityService class
    /// </summary>
    public class IdentityServiceTest
    {
        // Arrange
        private readonly AuthenticateUser<string> authenticateUser;
        private readonly ClaimsOption claimsIdentity;
        private readonly IOptions<EFCoreOption> options;

        /// <summary>
        /// Initializes a new instance of the CodeDesignPlus.EFCore.Test.Middleware.IdentityServiceTest
        /// </summary>
        public IdentityServiceTest()
        {
            this.authenticateUser = new AuthenticateUser<string>();

            this.claimsIdentity = new ClaimsOption()
            {
                Email = "email",
                IdUser = "sub",
                Role = "role",
                User = "name"
            };

            this.options = MEO.Options.Create(new EFCoreOption()
            {
                ClaimsIdentity = this.claimsIdentity
            });
        }

        /// <summary>
        /// Validate that it is not authenticated
        /// </summary>
        [Fact]
        public void BuildAuthenticateUser_UserNotAuthenticated_IAuthenticateUserWithoutInfo()
        {
            // Arrange
            var httpContextAccessor = this.GetHttpContextAccessor(HttpContextAccessorEnum.None);

            var identityService = new IdentityService<string>(options, httpContextAccessor, this.authenticateUser);

            // Act
            identityService.BuildAuthenticateUser();

            // Assert
            Assert.False(this.authenticateUser.IsAuthenticated);
            Assert.False(this.authenticateUser.IsApplication);
            Assert.Null(this.authenticateUser.Name);
            Assert.Null(this.authenticateUser.Email);
            Assert.Null(this.authenticateUser.IdUser);
        }

        /// <summary>
        /// Validate that you are a user with the information in the claims
        /// </summary>
        [Fact]
        public void BuildAuthenticateUser_UserAuthenticated_IAuthenticateUserWithInfo()
        {
            // Arrange
            var httpContextAccessor = this.GetHttpContextAccessor(HttpContextAccessorEnum.User);

            var identityService = new IdentityService<string>(options, httpContextAccessor, this.authenticateUser);

            // Act
            identityService.BuildAuthenticateUser();

            // Assert
            Assert.True(this.authenticateUser.IsAuthenticated);
            Assert.False(this.authenticateUser.IsApplication);
            Assert.Equal("TestUser", this.authenticateUser.Name);
            Assert.Equal("test-user@example.com", this.authenticateUser.Email);
            Assert.Equal("1", this.authenticateUser.IdUser);
        }

        /// <summary>
        /// Validate that it is an application
        /// </summary>
        [Fact]
        public void BuildAuthenticateUser_AppAuthenticated_IAuthenticateUserWithInfo()
        {
            // Arrange
            var httpContextAccessor = this.GetHttpContextAccessor(HttpContextAccessorEnum.App);

            var identityService = new IdentityService<string>(options, httpContextAccessor, this.authenticateUser);

            // Act
            identityService.BuildAuthenticateUser();

            // Assert
            Assert.True(this.authenticateUser.IsAuthenticated);
            Assert.True(this.authenticateUser.IsApplication);
            Assert.Null(this.authenticateUser.Name);
            Assert.Null(this.authenticateUser.Email);
            Assert.Null(this.authenticateUser.IdUser);
        }

        /// <summary>
        /// Create the HttpContextAccessor mock
        /// </summary>
        /// <param name="enum">Options for creating the HttpContextAccesor</param>
        /// <returns>Returns an object of type HttpContextAccesor</returns>
        private IHttpContextAccessor GetHttpContextAccessor(HttpContextAccessorEnum @enum)
        {
            var context = new DefaultHttpContext();

            switch (@enum)
            {
                case HttpContextAccessorEnum.None:
                    context.User = null;
                    break;
                case HttpContextAccessorEnum.User:
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                         new Claim(this.claimsIdentity.User, "TestUser"),
                         new Claim(this.claimsIdentity.Role, "admin"),
                         new Claim(this.claimsIdentity.IdUser, "1"),
                         new Claim(this.claimsIdentity.Email, "test-user@example.com"),
                    }, "user-mock"));
                    break;
                case HttpContextAccessorEnum.App:
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { }, "user-mock"));
                    break;
            }

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            return mockHttpContextAccessor.Object;
        }
    }
}
