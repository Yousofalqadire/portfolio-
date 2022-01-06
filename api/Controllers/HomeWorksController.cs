using System.Collections.Generic;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/homeworks")]
    public class HomeWorksController : ControllerBase
    {
        private readonly IHomeWork homeWork;
        public HomeWorksController(IHomeWork _homeWork)
        {
           this.homeWork = _homeWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HomeWork>> GetHomeWork(int id)
        {
            var homeWor = await homeWork.GetHomeWorkByIdAsync(id);
            return Ok(homeWor);   
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeWork>>> GetHomeWorks()
        {
           return Ok(await homeWork.GetHomeWorksAsync()); 
        }
        
        [HttpPost("add-homeWork")]
        public async Task<ActionResult<HomeWork>> CreatHomeWork(HomeWork model)
        {
        
            return Ok(await homeWork.AddHomeWorkAsync(model));
        }

        [HttpPut("update-homeWork")]
        public async Task<ActionResult<HomeWork>> UpdateHomeWork(HomeWork model)
        {
            if(model == null) return NotFound();
            return Ok(await homeWork.UpdateHomeWorkAsync(model));
        }
    }
}