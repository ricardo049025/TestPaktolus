using System;
using Domain.Domain.Interfaces;
using Domain.Domain.Interfaces.Service;

namespace Service.Main
{
	public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
	{
		protected IBaseRepository<TEntity> _baseRepository;

		public BaseService(IBaseRepository<TEntity> baseRepository)
		{
			this._baseRepository = baseRepository;
		}

		public IEnumerable<TEntity> getAll()
		{
			return this._baseRepository.getAll();
		}

		public void Add(TEntity entity)
		{
			this._baseRepository.Add(entity);
		}
	}
}

