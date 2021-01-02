using CodeDesignPlus.EFCore.Repository;

namespace CodeDesignPlus.Abstractions
{
    public interface IApplicationRepository : IRepositoryBase<long, int>
    {
    }
}
