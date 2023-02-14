using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Domain.Models
{
    public class StudentHobby
    {
        [Required]
        public int StudentId { set; get; }
        public virtual Student Student { get; set; }

        [Required]
        public int HobbyId { set; get; }
        public virtual Hobby Hobby { set; get; }
    }
}

