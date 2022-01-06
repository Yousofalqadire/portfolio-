using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkilsController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IPhoto image;

        public SkilsController(ApplicationDbContext _db,IPhoto _image)
        {
            db = _db;
            image = _image;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAllSkills()
        {
            var skills = await db.Skills
            .Include(x=> x.Photo)
            .ToListAsync();
            return Ok(skills);
        }

        [HttpPost("craet-skill")] // ready to upload
        public async Task<ActionResult<Skill>> AddSkill([FromForm] SkillDto model)
        {
           var result = await image.AddPhotoAsync(model.Certification);
           if(result.Error != null) return BadRequest(result.Error.Message);
           var photo = new Photo
            {
                 Url = result.SecureUrl.AbsoluteUri,
             PublicId = result.PublicId
            };
            var skill = new Skill();
            skill.Name = model.Name;
            skill.Photo = photo;
            await db.Skills.AddAsync(skill);
            await db.SaveChangesAsync();

            
            return Ok(skill);
        }
    }
}





// var file = Request.Form.Files[0];
//            var folderName = Path.Combine("Assests","Pdf");
//            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);
//            if(file.Length < 0) return BadRequest("unsupported medya types");
//            var fileName = ContentDispositionHeaderValue
//            .Parse(file.ContentDisposition).FileName.Trim('"');
//            var fullPath = Path.Combine(pathToSave,fileName);
//            var dbPath = Path.Combine(folderName,fileName);

//            using(var Stream = new FileStream(fullPath,FileMode.Create))
//            {
//                file.CopyTo(Stream);
//            }

//            var skill = new Skill();
//            var data = Request.Form;
//            skill.Name = data["Name"];
//            skill.Certification = dbPath;
//            await db.Skills.AddAsync(skill);
//            await db.SaveChangesAsync();