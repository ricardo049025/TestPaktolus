using System;
using Domain.Domain;
using Domain.Domain.Interfaces;

namespace Infraestructure.Data.Repositories
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected ApiContext context;

		public BaseRepository(ApiContext context)
		{
			this.context = context;
		}

		public IEnumerable<TEntity> getAll()
		{
			return this.context.Set<TEntity>();
		}


        public TEntity getById(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
		{
			this.context.Set<TEntity>().Add(entity);
			this.context.SaveChanges();
		}

		public void AddRange(List<TEntity> entities)
		{
			this.context.Set<TEntity>().AddRange(entities);
			this.context.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			this.context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			this.context.SaveChanges();
		}

		public void UpdateRange(List<TEntity> entities)
		{
			this.context.Set<TEntity>().UpdateRange(entities);
			this.context.SaveChanges();
		}

		public void DeleteRange(List<TEntity> entities)
		{
			this.context.Set<TEntity>().RemoveRange(entities);
			this.context.SaveChanges();
		}

	}
}

