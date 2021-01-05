using CodeDesignPlus.EFCore.Operations;
using CodeDesignPlus.EFCore.Sample.Api.Entities;

namespace CodeDesignPlus.EfCore.Sample.Api.Abstractions
{
    public interface IProductRepository : IOperationBase<long, string, Product>
    {
    }
}
