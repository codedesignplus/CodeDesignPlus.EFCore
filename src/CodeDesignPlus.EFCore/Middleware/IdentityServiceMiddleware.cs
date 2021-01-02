using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CodeDesignPlus.EFCore.Middleware
{
    /// <summary>
    /// Middleware that obtains and assigns user information in the IAuthenticateUser <TKeyUser> object
    /// </summary>
    /// <typeparam name="TKeyUser">Type of data that the user will identify</typeparam>
    public class IdentityServiceMiddleware<TKeyUser>
    {
        /// <summary>
        /// A function that can process an HTTP request.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the CodeDesignPlus.EFCore.Middleware.IdentityRepositoryMiddleware
        /// </summary>
        /// <param name="next">A function that can process an HTTP request.</param>
        public IdentityServiceMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Method invoked in the request flow
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        /// <param name="identityService">The IIdentityService for the current request.</param>
        /// <returns>A Task that represents the execution of this middleware.</returns>
        public async Task InvokeAsync(HttpContext context, IIdentityService<TKeyUser> identityService)
        {
            identityService.BuildAuthenticateUser();

            await this.next(context);
        }
    }
}
