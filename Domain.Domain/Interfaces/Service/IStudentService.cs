using System;
using Domain.Domain.Dtos;
using Domain.Domain.Models;

namespace Domain.Domain.Interfaces.Service
{
	public interface IStudentService: IBaseService<Student>
	{
        StudentsResponseDto AddNewStudent(StudentRequestDto request);

        StudentsResponseDto UpdateStudent(StudentRequestDto request);

        List<StudentDto> getStudentWithHobbies();

        StudentNumberDto getStudentById(int id);

    }
}

