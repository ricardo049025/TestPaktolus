using System;
using Domain.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Domain
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<StudentHobby>().HasKey(sc => new { sc.StudentId, sc.HobbyId });
        }

		public DbSet<Student> Students { set; get; }
		public DbSet<Hobby> Hobbies { set; get; }
		public DbSet<StudentHobby> StudentHobbies { set; get; }
		
	}
}

