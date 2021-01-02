using CodeDesignPlus.EFCore.Middleware;
using CodeDesignPlus.EFCore.Options;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using MEO = Microsoft.Extensions.Options;

namespace CodeDesignPlus.EFCore.Test.Middleware
{
    /// <summary>
    /// Unit tests to the IdentityServiceMiddlewareTest class
    /// </summary>
    public class IdentityServiceMiddlewareTest
    {
        /// <summary>
        /// Validate that the middleware associates user information with the IAuthenticateUser object
        /// </summary>
        [Fact]
        public async Task InvokeAsync_UserAuthenticated_IAuthenticateUser()
        {
            // Arrange
            var authenticateUser = new AuthenticateUser<int>();

            var claimsIdentity = new ClaimsOption()
            {
                Email = "email",
                IdUser = "sub",
                Role = "role",
                User = "name"
            };

            var options = MEO.Options.Create(new EFCoreOption()
            {
                ClaimsIdentity = claimsIdentity
            });

            var httpContextAccessor = this.GetHttpContextAccesor(out HttpContext context, claimsIdentity);

            var identityService = new IdentityService<int>(options, httpContextAccessor, authenticateUser);

            var requestDelegate = Mock.Of<RequestDelegate>();

            var middleware = new IdentityServiceMiddleware<int>(requestDelegate);

            // Act
            await middleware.InvokeAsync(context, identityService);

            // Assert
            Assert.True(authenticateUser.IsAuthenticated);
            Assert.False(authenticateUser.IsApplication);
            Assert.Equal("TestUser", authenticateUser.Name);
            Assert.Equal("test-user@example.com", authenticateUser.Email);
            Assert.Equal(1, authenticateUser.IdUser);
        }

        /// <summary>
        /// Create the HttpContextAccessor mock
        /// </summary>
        /// <param name="context">Parameter Out - The HttpContext for the current request.</param>
        /// <param name="claimsIdentity"> Claims available to obtain user information</param>
        /// <returns>Returns an object of type HttpContextAccesor</returns>
        private IHttpContextAccessor GetHttpContextAccesor(out HttpContext context, ClaimsOption claimsIdentity)
        {
            context = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                     new Claim(claimsIdentity.User, "TestUser"),
                     new Claim(claimsIdentity.Role, "admin"),
                     new Claim(claimsIdentity.IdUser, "1"),
                     new Claim(claimsIdentity.Email, "test-user@example.com"),
                }, "user-mock"))
            };

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            return mockHttpContextAccessor.Object;
        }
    }
}
