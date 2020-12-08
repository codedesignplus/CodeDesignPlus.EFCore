using CodeDesignPlus.Abstractions;
using CodeDesignPlus.EFCore.Model;
using CodeDesignPlus.EFCore.Operations;
using CodeDesignPlus.Entities;

namespace CodeDesignPlus.InMemory.Repositories
{
    public class PermissionRepository : OperationBase<long, int, Permission>, IPermissionRepository
    {
        public PermissionRepository(IAuthenticateUser<int> user, CodeDesignPlusContextInMemory context) : base(user, context)
        {
        }
    }
}
