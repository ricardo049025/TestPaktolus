using System;
namespace Domain.Domain.Dtos
{
	public class StudentNumberDto
	{
		public int id { set; get; }
		public string name { set; get; }
		public string email { set; get; }
		public string zipcode { set; get; }
		public string phone { set; get; }
		public List<int> hobbies { set; get; }
    
	}
}

