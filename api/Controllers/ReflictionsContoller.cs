using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/reflictions")]
    public class ReflictionsContoller : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public ReflictionsContoller(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Refliction>>> GetREflictions()
        {
           var result = await db.Reflictions.ToListAsync();
           return Ok(result);
        }
        [HttpPost("creat-Refliction")]
        public async Task<ActionResult<Refliction>> CreatRefliction([FromBody]Refliction model)
        {
          var result = new Refliction{Text = model.Text,Date =model.Date};
          await  db.Reflictions.AddAsync(result);
          await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}