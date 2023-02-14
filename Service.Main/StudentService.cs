using System;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces;
using Domain.Domain.Interfaces.Service;
using Domain.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Service.Main
{
	public class StudentService: BaseService<Student>, IStudentService
	{
		protected IStudentRepository studentRepository;
		protected IStudentHobbyRepository studentHobbyRepository;

		public StudentService(IStudentRepository studentRepository, IStudentHobbyRepository studentHobbyRepository) :base(studentRepository)
		{
			this.studentRepository = studentRepository;
			this.studentHobbyRepository = studentHobbyRepository;
		}

		public List<StudentDto> getStudentWithHobbies()
		{
            List<StudentDto> students = (from st in studentRepository.getAll()
                                         select
										 new StudentDto
										 { id = st.Id,
											name = st.Name,
											email = st.Email,
											phone = st.Phone.Insert(3, "-").Insert(7, "-"),
											zipcode = st.ZipCode
										 }).ToList();

			foreach (var item in students)
				item.hobbies = this.studentHobbyRepository.GetListHobbiesByStudentId(item.id);
				
			return students;

		}

		public StudentsResponseDto AddNewStudent(StudentRequestDto request)
		{

			if(this.studentRepository.getAll().Count(x => x.Email == request.email || x.Phone == request.phone) != 0)
				throw new Exception("Exists an user with the same email or phone ");
			Student student = new Student();
			student.Name = request.name;
			student.Email = request.email;
			student.Phone = request.phone;
			student.ZipCode = request.zipCode;

			studentRepository.Add(student);

			List<StudentHobby> studentHobbies = new List<StudentHobby>();

			foreach (int item in request.hobbies)
			{
                StudentHobby hobbystudent = new StudentHobby();
				hobbystudent.HobbyId = item;
				hobbystudent.StudentId = student.Id;

				studentHobbies.Add(hobbystudent);
            }

			studentHobbyRepository.AddRange(studentHobbies);
			student.StudentHobbies = null;

			//returning result
			StudentDto st = new StudentDto();
			st.id = student.Id;
			st.name = student.Name;
			st.email = student.Email;
			st.phone = student.Phone.Insert(3,"-").Insert(7,"-");
			st.zipcode = student.ZipCode;
			st.hobbies = this.studentHobbyRepository.GetListHobbiesByStudentId(student.Id);


            return new StudentsResponseDto { student = st, success = true };

        }

		public StudentsResponseDto UpdateStudent(StudentRequestDto request)
		{
			List<StudentHobby> hobbies = studentHobbyRepository.getAll().
				Where(x => x.StudentId == request.id).ToList();

			Student student = new Student();
			student.Id = request.id ??0;
			student.Name = request.name;
			student.Email = request.email;
			student.Phone = request.phone;
			student.ZipCode = request.zipCode;

			this.studentRepository.Update(student);

			this.studentHobbyRepository.DeleteRange(student.Id,hobbies.Select(x=> x.HobbyId).ToList());
            List<StudentHobby> studentHobbies = new List<StudentHobby>();

            foreach (var item in request.hobbies)
			{
                StudentHobby hobbystudent = new StudentHobby();
                hobbystudent.HobbyId = item;
                hobbystudent.StudentId = student.Id;

                studentHobbies.Add(hobbystudent);
            }

			this.studentHobbyRepository.AddRange(studentHobbies);

            //returning result
            StudentDto st = new StudentDto();
            st.id = student.Id;
            st.name = student.Name;
            st.email = student.Email;
            st.phone = student.Phone;
            st.zipcode = student.ZipCode;
            st.hobbies = this.studentHobbyRepository.GetListHobbiesByStudentId(student.Id);


            return new StudentsResponseDto { student = st, success = true };
        }

		public StudentNumberDto getStudentById(int id)
		{
			Student student = this.studentRepository.getById(id);
			StudentNumberDto stDto = new StudentNumberDto();
			stDto.id = student.Id;
			stDto.name = student.Name;
			stDto.email = student.Email;
			stDto.phone = student.Phone;
			stDto.zipcode = student.ZipCode;
			stDto.hobbies = this.studentHobbyRepository.GetListHobbiesIdByStudentId(id);

			return stDto;
		}
    }
}

