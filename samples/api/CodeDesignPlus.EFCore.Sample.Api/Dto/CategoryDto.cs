using CodeDesignPlus.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeDesignPlus.EFCore.Sample.Api.Dto
{
    public class CategoryDto : IDtoLong<string>
    {
        public long Id { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "The {0} field not is valid")]
        public string Name { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "The {0} field not is valid")]
        public string Description { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
