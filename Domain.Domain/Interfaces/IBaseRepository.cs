using System;
namespace Domain.Domain.Interfaces
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> getAll();
		TEntity getById(int id);
        void Add(TEntity entity);
		void AddRange(List<TEntity> entities);
		void Update(TEntity entity);
		void UpdateRange(List<TEntity> entities);
		void DeleteRange(List<TEntity> entities);
	}
}

