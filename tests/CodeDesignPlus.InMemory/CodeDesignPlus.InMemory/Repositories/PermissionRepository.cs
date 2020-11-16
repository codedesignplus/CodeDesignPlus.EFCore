using CodeDesignPlus.Abstractions;
using CodeDesignPlus.EFCore.Repository;

namespace CodeDesignPlus.InMemory.Repositories
{
    public class PermissionRepository : RepositoryBase<long, int>, IPermissionRepository
    {
        public PermissionRepository(CodeDesignPlusContextInMemory context) : base(context)
        {
        }
    }
}
