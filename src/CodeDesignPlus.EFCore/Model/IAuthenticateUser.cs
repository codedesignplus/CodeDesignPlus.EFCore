namespace CodeDesignPlus.EFCore.Model
{
    /// <summary>
    /// Provide the information of the authenticated user during the request
    /// </summary>
    /// <typeparam name="TKeyUser">Type of data that the user will identify</typeparam>
    public interface IAuthenticateUser<TKeyUser>
    {
        /// <summary>
        /// Gets a value boolean that indicates whether is a application
        /// </summary>
        bool IsApplication { get; set; }
        /// <summary>
        /// Gets the Id User authenticated
        /// </summary>
        TKeyUser IdUser { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        bool IsAuthenticated { get; set; }
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        string Name { get; set; }
    }
}