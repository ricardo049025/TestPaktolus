using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Models
{
	public class Hobby
	{
		[Key]
		public int Id { set; get; }
		[Required]
		[StringLength(50)]
		public string? Name { set; get; }
		[Required]
		public bool Active { set; get; }

        public virtual IList<StudentHobby> StudentHobbies { set; get; }
    }
}

