using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> logger;
        private IStudentService studentService;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            this.logger = logger;
            this.studentService = studentService;
        }

        //get students
        [HttpGet]
        public List<StudentDto> Index()
        {
            return this.studentService.getStudentWithHobbies();
        }

        //post student
        [HttpPost]
        public StudentsResponseDto AddUser([FromBody] StudentRequestDto request)
        {
            try
            {
                return this.studentService.AddNewStudent(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred !");
                return new StudentsResponseDto { xerror= ex.Message, success = false, student = null };
            }
        }

        [HttpPatch]
        [Route("update/{id:int}")]
        public StudentsResponseDto UpdateUser([FromBody] StudentRequestDto request)
        {
            try
            {
                return this.studentService.UpdateStudent(request);

            }
            catch (Exception ex)
            {
                return new StudentsResponseDto { success = false, student = null };
            }

        }

        [HttpGet]
        [Route("{id:int}")]
        public StudentNumberDto getStudentById(int id)
        {
            try
            {
                return this.studentService.getStudentById(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred !");
                return null;
            }
        } 
    }

}