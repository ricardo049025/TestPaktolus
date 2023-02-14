using System;
namespace Domain.Domain.Interfaces.Service
{
	public interface IBaseService<TEntity> where TEntity: class
	{
		IEnumerable<TEntity> getAll();
		void Add(TEntity entity);
	}
}

