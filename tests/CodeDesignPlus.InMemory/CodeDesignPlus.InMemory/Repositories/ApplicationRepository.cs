using CodeDesignPlus.Abstractions;
using CodeDesignPlus.EFCore.Repository;

namespace CodeDesignPlus.InMemory.Repositories
{
    public class ApplicationRepository : RepositoryBase<long, int>, IApplicationRepository
    {
        public ApplicationRepository(CodeDesignPlusContextInMemory context) : base(context)
        {
        }
    }
}
