using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HobbyController : Controller
    {
        private readonly ILogger<HobbyController> logger;
        private IHobbyService hobbyService;

        public HobbyController(ILogger<HobbyController> logger, IHobbyService hobbyService)
        {
            this.logger = logger;
            this.hobbyService = hobbyService;
        }

        //get students
        [HttpGet]
        public IEnumerable<HobbyResponseDto> Index()
        {
            var hobbies = this.hobbyService.getAll();
            return (from ho in hobbies
                    select new HobbyResponseDto { id = ho.Id, name = ho.Name });
        }

        [HttpPost]
        public HobbyResponseDto Create([FromBody] HobbyRequestDto request)
        {
            try
            {
                return this.hobbyService.AddNewHobby(request);
            }
            catch (Exception ex)
            {
                return new HobbyResponseDto { success = false, xerror = ex.Message };
            }
        }
    }
}
