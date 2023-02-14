using System;
using Domain.Domain;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces;
using Domain.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Repositories
{
	public class StudentHobbyRepository : BaseRepository<StudentHobby>, IStudentHobbyRepository
	{
		protected ApiContext context;

		public StudentHobbyRepository(ApiContext context): base(context)
		{
			this.context = context;
		}


        public void DeleteRange(int studentId, List<int> ids)
        {
			var entities = this.context.StudentHobbies.Where(x => x.StudentId == studentId && ids.Contains(x.HobbyId));
			this.context.StudentHobbies.RemoveRange(entities);
			this.context.SaveChanges();
        }

        public string GetListHobbiesByStudentId(int id)
        {
            var query = (from sh in this.context.StudentHobbies
                         join h in this.context.Hobbies on sh.HobbyId equals h.Id
                         where sh.StudentId == id
						 select h.Name
                         );

			return String.Join(",", query);
        }

        public List<int> GetListHobbiesIdByStudentId(int id)
        {
            var query = (from sh in this.context.StudentHobbies
                         join h in this.context.Hobbies on sh.HobbyId equals h.Id
                         where sh.StudentId == id
                         select h.Id
                         );

            return query.ToList();
        }

    }
}

