﻿using CodeDesignPlus.Core.Abstractions;
using System;

namespace CodeDesignPlus.EFCore.Sample.Api.Entities
{
    public class Product : IEntityLong<string>
    {
        public long Id { get; set; }
        public long IdCategory { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string IdUserCreator { get; set; }
        public DateTime DateCreated { get; set; }

        public Category Category { get; set; }
    }
}
