using CodeDesignPlus.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace CodeDesignPlus.EFCore.Sample.Api.Entities
{
    public class Category : IEntityLong<string>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Product> Products { get; set; }
    }
}
