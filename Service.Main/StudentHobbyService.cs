using System;
using Domain.Domain.Interfaces;
using Domain.Domain.Interfaces.Service;
using Domain.Domain.Models;

namespace Service.Main
{
	public class StudentHobbyService: BaseService<StudentHobby>, IStudentHobbyService
    {
        protected IStudentHobbyRepository studentHobbyRepository;

        public StudentHobbyService(IStudentHobbyRepository studentHobbyRepository) : base(studentHobbyRepository)
        {
            this.studentHobbyRepository = studentHobbyRepository;
        }
    }
}

