using CodeDesignPlus.Core.Abstractions;
using CodeDesignPlus.EFCore.Repository;

namespace CodeDesignPlus.EFCore.Operations
{
    /// <summary>
    /// Enables standardized CRUD operations in the SDK
    /// </summary>
    /// <typeparam name="TKey">Type of data that will identify the record</typeparam>
    /// <typeparam name="TUserKey">Type of data that the user will identify</typeparam>
    /// <typeparam name="TEntity">The entity type to be configured.</typeparam>
    public interface IOperationBase<TKey, TUserKey, TEntity> :
        ICreateOperation<TKey, TUserKey, TEntity>,
        IUpdateOperation<TKey, TUserKey, TEntity>,
        IDeleteOperation<TKey, TUserKey, TEntity>,
        IRepositoryBase<TKey, TUserKey>
        where TEntity : class, IEntityBase<TKey, TUserKey>
    { }
}
