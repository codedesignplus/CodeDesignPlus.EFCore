using System.ComponentModel.DataAnnotations;

namespace CodeDesignPlus.EFCore.Options
{
    /// <summary>
    /// Claims available to obtain user information
    /// </summary>
    public class ClaimsOption
    {
        /// <summary>
        /// Gets or sets the claim to get name user
        /// </summary>
        [Required]
        public string User { get; set; }
        /// <summary>
        /// Gets or sets the claim to get id user
        /// </summary>
        [Required]
        public string IdUser { get; set; }
        /// <summary>
        /// Gets or sets the claim to get email
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the claim to get email
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
