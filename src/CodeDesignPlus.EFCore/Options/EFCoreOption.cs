using System.ComponentModel.DataAnnotations;

namespace CodeDesignPlus.EFCore.Options
{
    /// <summary>
    /// Configuration options for CodeDesignPlus.EFCore
    /// </summary>
    public class EFCoreOption
    {
        /// <summary>
        /// Gets or sets the claims identity 
        /// </summary>
        [Required]
        public ClaimsOption ClaimsIdentity { get; set; }
    }
}
