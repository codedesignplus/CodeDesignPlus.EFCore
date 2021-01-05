using CodeDesignPlus.EFCore.Operations;
using CodeDesignPlus.EFCore.Sample.Api.Entities;

namespace CodeDesignPlus.EfCore.Sample.Api.Abstractions
{
    public interface ICategoryRepository : IOperationBase<long, string, Category>
    {
    }
}
