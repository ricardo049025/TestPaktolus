using System;
using Domain.Domain.Models;

namespace Domain.Domain.Dtos
{
	public class StudentDto
	{
		public int id { set; get; }
		public string name { set; get; }
		public string email { set; get; }
		public string zipcode { set; get; }
		public string phone { set; get; }
		public string hobbies { set; get; }
		
	}
}

