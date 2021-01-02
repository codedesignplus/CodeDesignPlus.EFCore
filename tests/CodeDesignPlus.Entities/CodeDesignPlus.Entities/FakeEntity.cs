using CodeDesignPlus.Core.Abstractions;
using System;

namespace CodeDesignPlus.Entities
{
    public class FakeEntity : IEntityLong<int>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public int IdUserCreator { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
