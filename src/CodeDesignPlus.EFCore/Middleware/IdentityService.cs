using CodeDesignPlus.EFCore.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Security.Principal;

namespace CodeDesignPlus.EFCore.Middleware
{
    /// <summary>
    /// Clase que implementa la interfaz IIdentityRepository{TKeyUser}
    /// </summary>
    /// <typeparam name="TUserKey">Type of data that the user will identify</typeparam>
    public class IdentityService<TUserKey> : IIdentityService<TUserKey>
    {
        /// <summary>
        /// Defines the basic functionality of an identity object.
        /// </summary>
        private readonly IIdentity identity;
        /// <summary>
        /// Configuration options for CodeDesignPlus.EFCore
        /// </summary>
        private readonly EFCoreOption efCoreOptions;
        /// <summary>
        /// Provide the information of the authenticated user during the request
        /// </summary>
        private readonly IAuthenticateUser<TUserKey> authenticateUser;
        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the CodeDesignPlus.EFCore.Middleware.IdentityService
        /// </summary>
        /// <param name="options">Configuration options for CodeDesignPlus.EFCore</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="authenticateUser">Information of the authenticated user during the request</param>
        public IdentityService(IOptions<EFCoreOption> options, IHttpContextAccessor httpContextAccessor, IAuthenticateUser<TUserKey> authenticateUser)
        {
            this.efCoreOptions = options.Value;
            this.httpContextAccessor = httpContextAccessor;
            this.identity = httpContextAccessor.HttpContext.User.Identity;
            this.authenticateUser = authenticateUser;
        }

        /// <summary>
        /// Method that assigns identity values to IAuthenticateUser{TKeyUser}
        /// </summary>
        /// <returns>Return an IAuthenticateUser{TKeyUser}</returns>
        public IAuthenticateUser<TUserKey> BuildAuthenticateUser()
        {
            authenticateUser.IsAuthenticated = this.identity.IsAuthenticated;

            if (authenticateUser.IsAuthenticated)
            {
                authenticateUser.Name = this.GetUser();
                authenticateUser.Email = this.GetEmail();
                authenticateUser.IdUser = this.GetIdUser();
                authenticateUser.IsApplication = this.IsApplication();
            }

            return this.authenticateUser;
        }

        /// <summary>
        /// Method that obtains the UserName of the authenticated user based on the assigned configuration
        /// </summary>
        /// <returns>Returns the username of the authenticated user</returns>
        private string GetUser() => this.GetClaimValue(this.efCoreOptions.ClaimsIdentity.User);

        /// <summary>
        /// Method that obtains the email of the authenticated user based on the assigned configuration
        /// </summary>
        /// <returns>Returns the email of the authenticated user</returns>
        private string GetEmail() => this.GetClaimValue(this.efCoreOptions.ClaimsIdentity.Email);


        /// <summary>
        /// Method that obtains the id of the authenticated user based on the assigned configuration
        /// </summary>
        /// <returns>Returns the id of the authenticated user</returns>
        private TUserKey GetIdUser()
        {
            var idUser = this.httpContextAccessor.HttpContext.User.FindFirst(this.efCoreOptions.ClaimsIdentity.IdUser);

            if (idUser != null)
                return (TUserKey)Convert.ChangeType(idUser.Value, typeof(TUserKey));

            return default;
        }

        /// <summary>
        /// Method that validates if the identity is an application based on the claim role
        /// </summary>
        /// <returns>Returns true if it is an application, otherwise it returns false</returns>
        private bool IsApplication()
        {
            var role = this.httpContextAccessor.HttpContext.User.FindFirst(this.efCoreOptions.ClaimsIdentity.Role);

            return role == null;
        }

        /// <summary>
        /// Method that consults the value of a certain claim
        /// </summary>
        /// <param name="claimType">Claim from which the value will be obtained</param>
        /// <returns>Return the value of the claim</returns>
        private string GetClaimValue(string claimType)
        {
            var user = this.httpContextAccessor.HttpContext.User.FindFirst(claimType);

            if (user != null)
                return user.Value;

            return default;
        }
    }
}
