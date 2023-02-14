using System;
namespace Domain.Domain.Dtos
{
	public class CustomeResponseStudentDto
	{
		public List<StudentDto> students { set; get; }
		public bool success { set; get; }
	}
}

