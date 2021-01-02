namespace CodeDesignPlus.EFCore.Middleware
{
    /// <summary>
    /// Default implementation of IAuthenticateUser<typeparamref name="TKeyUser"/>
    /// </summary>
    /// <typeparam name="TKeyUser">Type of data that the user will identify</typeparam>
    public class AuthenticateUser<TKeyUser> : IAuthenticateUser<TKeyUser>
    {
        /// <summary>
        /// Gets a value boolean that indicates whether is a application
        /// </summary>
        public bool IsApplication { get; set; }
        /// <summary>
        /// Gets the Id User authenticated
        /// </summary>
        public TKeyUser IdUser { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated { get; set; }
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the email of the current user.
        /// </summary>
        public string Email { get; set; }
    }
}
