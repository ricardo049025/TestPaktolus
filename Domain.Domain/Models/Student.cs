using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Models
{
	public class Student
	{
        [Key]
		public int Id { set; get; }
		[Required]
		[StringLength(50)]
		public string? Name { set; get; }
		[Required]
		[StringLength(80)]
		public string? Email { set; get; }
		[Required]
		[StringLength(12)]
		public string? Phone { set; get; }
        [StringLength(5)]
        public string? ZipCode { set; get; }

        public virtual IList<StudentHobby> StudentHobbies { get; set; }
    }
}

