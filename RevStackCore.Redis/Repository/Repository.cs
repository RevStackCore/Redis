using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RevStackCore.Pattern;
using RevStackCore.Redis.Client;

namespace RevStackCore.Redis
{
	/// <summary>
	/// Redis repository.
	/// </summary>
	public class RedisRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
	{
		private readonly TypedClient<TEntity, TKey> _typedClient;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RevStackCore.Redis.RedisRepository`2"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public RedisRepository(RedisDbContext context)
		{
			_typedClient = new TypedClient<TEntity, TKey>(context);
		}

		/// <summary>
		/// Get this instance.
		/// </summary>
		/// <returns>The get.</returns>
		public virtual IEnumerable<TEntity> Get()
		{
			return _typedClient.GetAll();
		}

		/// <summary>
		/// Gets the entity by identifier.
		/// </summary>
		/// <returns>The by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public virtual TEntity GetById(TKey id)
		{
			return _typedClient.GetById(id);
		}

		/// <summary>
		/// Find the specified predicate.
		/// </summary>
		/// <returns>The find.</returns>
		/// <param name="predicate">Predicate.</param>
		public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _typedClient.GetAll().AsQueryable().Where(predicate);
		}

		/// <summary>
		/// Add the specified entity.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="entity">Entity.</param>
		public virtual TEntity Add(TEntity entity)
		{
			return _typedClient.Insert(entity);
		}

		/// <summary>
		/// Update the specified entity.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="entity">Entity.</param>
		public virtual TEntity Update(TEntity entity)
		{
			return _typedClient.Store(entity);
		}

		/// <summary>
		/// Delete the specified entity.
		/// </summary>
		/// <returns>The delete.</returns>
		/// <param name="entity">Entity.</param>
		public virtual void Delete(TEntity entity)
		{
			_typedClient.Delete(entity);
		}


	}
}
