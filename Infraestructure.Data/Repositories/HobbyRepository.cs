using System;
using Domain.Domain;
using Domain.Domain.Interfaces;
using Domain.Domain.Models;

namespace Infraestructure.Data.Repositories
{
	public class HobbyRepository : BaseRepository<Hobby>, IHobbyRepository
	{
		protected ApiContext context;
		
		public HobbyRepository(ApiContext context) : base(context)
		{
			this.context = context;
		}
	}
}

