using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/tutorials")]

    public class TutorialsController : ControllerBase
    {
        private readonly ITutorial tutorial;
        private readonly IMapper maper;

        public TutorialsController(ITutorial _tutorial,IMapper _maper)
        {
            tutorial = _tutorial;
            maper = _maper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTutorialById(int id)
        {
            return Ok(await tutorial.GetTutorialById(id));
        }
        [HttpGet]
        public async Task<ActionResult> GetTutorials()
        {
            return Ok(await tutorial.GetTutorialsAsync());
        }
        [HttpPost]
        public async Task<ActionResult> CreateTutorial([FromBody]Tutorial model)
        {
            return Ok(await tutorial.CreatTutorial(model));
        }

        [HttpGet("get-lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons(string name)
        {
            var lessons = new List<Lesson>();
            var result = await tutorial.GetTutorialByName(name);
            foreach(var l in result.Lessons)
            {
               var les = new Lesson{Id = l.Id, Name = l.Name,Documentaion=l.Documentaion,VedioId=l.VedioId};
               lessons.Add(les);
            }
           var  lessonToRetutrn = maper.Map<IEnumerable<LessonDto>>(lessons);
            return Ok(lessonToRetutrn);
        }

    }
}