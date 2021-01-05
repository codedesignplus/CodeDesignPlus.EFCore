using CodeDesignPlus.EfCore.Sample.Api.Abstractions;
using CodeDesignPlus.EFCore.Middleware;
using CodeDesignPlus.EFCore.Operations;
using CodeDesignPlus.EFCore.Sample.Api.Entities;

namespace CodeDesignPlus.EfCore.Sample.Api.SqlServer.Repositories
{
    public class CategoryRepository : OperationBase<long, string, Category>, ICategoryRepository
    {
        public CategoryRepository(IAuthenticateUser<string> authenticatetUser, SqlServerContext context)
            : base(authenticatetUser, context)
        {
        }
    }
}
