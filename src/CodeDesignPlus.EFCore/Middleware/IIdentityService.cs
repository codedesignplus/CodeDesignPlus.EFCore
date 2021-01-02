namespace CodeDesignPlus.EFCore.Middleware
{
    /// <summary>
    /// Build the IAuthenticationUser object and that is used in IIdentityService<TKeyUser>
    /// </summary>
    /// <typeparam name="TKeyUser">Type of data that the user will identify</typeparam>
    public interface IIdentityService<TKeyUser>
    {
        /// <summary>
        /// Method that assigns identity values to IAuthenticateUser<TKeyUser>
        /// </summary>
        /// <returns>Return an IAuthenticateUser<TKeyUser></returns>
        IAuthenticateUser<TKeyUser> BuildAuthenticateUser();
    }
}