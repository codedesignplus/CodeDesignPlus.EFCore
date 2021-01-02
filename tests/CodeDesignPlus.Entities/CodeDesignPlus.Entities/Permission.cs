using CodeDesignPlus.Core.Abstractions;
using System;
using System.Collections.Generic;

namespace CodeDesignPlus.Entities
{
    public class Permission : IEntityLong<int>
    {
        public Permission()
        {
            this.AppPermisions = new List<AppPermision>();
            this.RolePermisions = new List<RolePermission>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool State { get; set; }
        public int IdUserCreator { get; set; }
        public DateTime DateCreated { get; set; }


        public List<AppPermision> AppPermisions { get; set; }
        public List<RolePermission> RolePermisions { get; set; }
    }
}
