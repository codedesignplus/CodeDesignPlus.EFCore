using CodeDesignPlus.Core.Abstractions;
using CodeDesignPlus.EFCore.Middleware;
using CodeDesignPlus.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDesignPlus.EFCore.Operations
{
    /// <summary>
    /// Implementación de las operaciones CRUD estandarizadas en el SDK
    /// </summary>
    /// <typeparam name="TKey">Type of data that will identify the record</typeparam>
    /// <typeparam name="TUserKey">Type of data that the user will identify</typeparam>
    /// <typeparam name="TEntity">The entity type to be configured.</typeparam>
    public abstract class OperationBase<TKey, TUserKey, TEntity> : RepositoryBase<TKey, TUserKey>, IOperationBase<TKey, TUserKey, TEntity>
        where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        /// <summary>
        /// List of properties that will not be updated
        /// </summary>
        private readonly List<string> blacklist = new List<string>() {
            nameof(IEntityBase<TKey, TUserKey>.Id),
            nameof(IEntityBase<TKey, TUserKey>.DateCreated),
            nameof(IEntityBase<TKey, TUserKey>.IdUserCreator)
        };

        /// <summary>
        /// Provide the information of the authenticated user during the request
        /// </summary>
        protected readonly IAuthenticateUser<TUserKey> AuthenticateUser;

        /// <summary>
        /// Initializes a new instance of CodeDesignPlus.EFCore.Operations.Operation class using the speciffied options.
        /// </summary>
        /// <param name="authenticatetUser">Information of the authenticated user during the request</param>
        /// <param name="context">Represents a session with the database and can be used to query and save instances of your entities</param>
        protected OperationBase(IAuthenticateUser<TUserKey> authenticatetUser, DbContext context) : base(context)
        {
            this.AuthenticateUser = authenticatetUser;
        }

        /// <summary>
        /// Method to create a record in the database
        /// </summary>
        /// <param name="entity">Entity to create</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public virtual async Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.IdUserCreator = this.AuthenticateUser.IdUser;
            entity.DateCreated = DateTime.Now;

            entity = await base.CreateAsync(entity, cancellationToken);

            return entity.Id;
        }

        /// <summary>
        /// Method that deletes a record in the database
        /// </summary>
        /// <param name="id">Id of the record to delete</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public virtual Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync<TEntity>(x => x.Id.Equals(id), cancellationToken);
        }

        /// <summary>
        /// Method that updates a record in the database
        /// </summary>
        /// <param name="id">Id of the record to update</param>
        /// <param name="entity">Entity with the information to update</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public virtual async Task<bool> UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityUpdated = await base.GetEntity<TEntity>().FindAsync(id);

            if (entityUpdated != null)
            {
                var properties = typeof(TEntity).GetProperties().Where(x => !this.blacklist.Contains(x.Name)).ToList();

                foreach (var property in properties)
                {
                    var value = entity.GetType().GetProperty(property.Name).GetValue(entity);

                    if (value != null)
                        entityUpdated.GetType().GetProperty(property.Name).SetValue(entityUpdated, value, null);
                }

                return await base.UpdateAsync(entity, cancellationToken);
            }

            return false;
        }
    }
}
