using CodeDesignPlus.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeDesignPlus.EFCore.Repository
{
    /// <summary>
    /// This interface implement the most concurrent methods with the database
    /// </summary>
    public abstract class RepositoryBase<TKey, TUserKey> : IRepositoryBase<TKey, TUserKey>
    {
        /// <summary>
        /// Represents a session with the database and can be used to query and save instances of your entities
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Create a new instace of Repository
        /// </summary>
        /// <param name="context">Represents a session with the database and can be used to query and save instances of your entities</param>
        /// <exception cref="ArgumentNullException">If context is null</exception>
        protected RepositoryBase(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Convert the DbContext to the assigned generic type
        /// </summary>
        /// <typeparam name="TContext">Type of context to return</typeparam>
        /// <returns>Returns the context of the database</returns>
        public TContext GetContext<TContext>() where TContext : DbContext => (TContext)this.Context;

        /// <summary>
        /// Get a DbSet that can be used to query and save instances of TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        public DbSet<TEntity> GetEntity<TEntity>() where TEntity : class, IEntityBase<TKey, TUserKey> => this.Context.Set<TEntity>();

        /// <summary>
        /// Method that creates an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to create</typeparam>
        /// <param name="entity">Entity to create</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return this.ProcessCreateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Method that creates an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to create</typeparam>
        /// <param name="entity">Entity to create</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        private async Task<TEntity> ProcessCreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            await this.Context.AddAsync(entity);

            await this.Context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        /// <summary>
        /// Method that updates an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to udpate</typeparam>
        /// <param name="entity">Entity to update</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public Task<bool> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return this.ProcessUpdateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Method that updates an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to udpate</typeparam>
        /// <param name="entity">Entity to update</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        private async Task<bool> ProcessUpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            this.Context.Set<TEntity>().Update(entity);

            this.Context.Entry(entity).Property(nameof(IEntityBase<TKey, TUserKey>.IdUserCreator)).IsModified = false;
            this.Context.Entry(entity).Property(nameof(IEntityBase<TKey, TUserKey>.DateCreated)).IsModified = false;

            var success = await this.Context.SaveChangesAsync(cancellationToken) > 0;

            return success;
        }

        /// <summary>
        /// Method that deletes an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to delete</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return this.ProcessDeleteAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Method that deletes an entity in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to delete</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        private async Task<bool> ProcessDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            var entity = await this.Context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();

            if (entity != null)
            {
                this.Context.Set<TEntity>().Remove(entity);

                return await this.Context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }

        /// <summary>
        /// Method that creates a set of entities in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to create</typeparam>
        /// <param name="entities">List of entities to create</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public async Task<List<TEntity>> CreateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return entities;

            return await ProcessCreateRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Method that creates a set of entities in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to create</typeparam>
        /// <param name="entities">List of entities to create</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        private async Task<List<TEntity>> ProcessCreateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            await this.Context.AddRangeAsync(entities);

            await this.Context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        /// <summary>
        /// Method that updates a set of entities in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to update</typeparam>
        /// <param name="entities">List of entities to update</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public async Task<bool> UpdateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return false;

            this.Context.UpdateRange(entities);

            return await this.Context.SaveChangesAsync(cancellationToken) == entities.Count;
        }

        /// <summary>
        /// Method that deletes a set of entities in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to delete</typeparam>
        /// <param name="entities">List of entities to delete</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public async Task<bool> DeleteRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return false;

            this.Context.RemoveRange(entities);

            return await this.Context.SaveChangesAsync(cancellationToken) == entities.Count;
        }

        /// <summary>
        /// Method that will change the state to the registry in the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to update</typeparam>
        /// <param name="id">Id of the record to update</param>
        /// <param name="state">Status tha will be assigned to the record if it exists</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public async Task<bool> ChangeStateAsync<TEntity>(TKey id, bool state, CancellationToken cancellationToken = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            var entity = await this.Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity != null)
            {
                entity.State = state;

                return await this.Context.SaveChangesAsync(cancellationToken) > 0;
            }

            return false;
        }

        /// <summary>
        /// Method that allows multiple process in the database in a single transaction
        /// </summary>
        /// <typeparam name="TResult">Type of data to return if the process is succesful</typeparam>
        /// <param name="process">Process to execute in the transaction flow</param>
        /// <param name="isolation">Specifies the transaction locking behavior for the connection.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Represents an asynchronous operation that can return a value.</returns>
        public async Task<TResult> TransactionAsync<TResult>(Func<DbContext, Task<TResult>> process, IsolationLevel isolation = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        {
            var strategy = this.Context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async (cancellation) =>
            {
                using var transaction = await this.Context.Database.BeginTransactionAsync(isolation, cancellation);

                var result = await process(this.Context);

                await transaction.CommitAsync();

                return result;
            }, cancellationToken);
        }
    }
}
