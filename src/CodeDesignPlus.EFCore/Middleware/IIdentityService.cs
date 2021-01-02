namespace CodeDesignPlus.EFCore.Middleware
{
    /// <summary>
    /// Build the IAuthenticationUser object and that is used in IIdentityService{TKeyUser}
    /// </summary>
    /// <typeparam name="TUserKey">Type of data that the user will identify</typeparam>
    public interface IIdentityService<TUserKey>
    {
        /// <summary>
        /// Method that assigns identity values to IAuthenticateUser<TKeyUser>
        /// </summary>
        /// <returns>Return an IAuthenticateUser{TKeyUser}</returns>
        IAuthenticateUser<TUserKey> BuildAuthenticateUser();
    }
}