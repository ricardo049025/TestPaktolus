using System;
using Domain.Domain.Dtos;
using Domain.Domain.Models;

namespace Domain.Domain.Interfaces
{
	public interface IStudentHobbyRepository : IBaseRepository<StudentHobby>
    {
        public void DeleteRange(int studentId, List<int> ids);
        public string GetListHobbiesByStudentId(int id);
        public List<int> GetListHobbiesIdByStudentId(int id);
    }
}

