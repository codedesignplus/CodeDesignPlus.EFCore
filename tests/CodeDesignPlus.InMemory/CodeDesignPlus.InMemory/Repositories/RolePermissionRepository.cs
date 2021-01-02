using CodeDesignPlus.Abstractions;
using CodeDesignPlus.EFCore.Repository;

namespace CodeDesignPlus.InMemory.Repositories
{
    public class RolePermissionRepository : RepositoryBase<long, int>, IRolePermissionRepository
    {
        public RolePermissionRepository(CodeDesignPlusContextInMemory context) : base(context)
        {
        }
    }
}
