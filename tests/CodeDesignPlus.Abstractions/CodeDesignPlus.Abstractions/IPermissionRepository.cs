using CodeDesignPlus.EFCore.Operations;
using CodeDesignPlus.Entities;

namespace CodeDesignPlus.Abstractions
{
    public interface IPermissionRepository : IOperationBase<long, int, Permission>
    {
    }
}
