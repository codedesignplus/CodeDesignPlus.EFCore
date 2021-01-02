using CodeDesignPlus.Core.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDesignPlus.EFCore.Operations
{
    /// <summary>
    /// Allows the repository to update a record by assigning the information to the transversal properties of the entity
    /// </summary>
    /// <typeparam name="TKey">Type of data that will identify the record</typeparam>
    /// <typeparam name="TUserKey">Type of data that the user will identify</typeparam>
    /// <typeparam name="TEntity">Type of entity to update</typeparam>
    public interface IUpdateOperation<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        /// <summary>
        /// Method that updates a record in the database
        /// </summary>
        /// <param name="id">Id of the record to update</param>
        /// <param name="entity">Entity with the information to update</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        Task<bool> UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default);
    }
}
