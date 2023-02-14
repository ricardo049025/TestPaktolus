using System;
using Domain.Domain.Models;

namespace Domain.Domain.Dtos
{
	public class StudentsResponseDto
	{
		public string xerror { set; get; }
		public StudentDto student { set; get; }
		public bool success { set; get; }
	}
}

