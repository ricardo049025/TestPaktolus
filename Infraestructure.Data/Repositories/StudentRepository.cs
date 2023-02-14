using System;
using Domain.Domain;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces;
using Domain.Domain.Models;

namespace Infraestructure.Data.Repositories
{
	public class StudentRepository : BaseRepository<Student>, IStudentRepository
	{
		protected ApiContext context;

		public StudentRepository(ApiContext context) :base(context)
		{ 
            this.context = context;
		}

    }
}

