using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Reposetory
{
    public class TutorialRepository : ITutorial
    {
        private readonly ApplicationDbContext db;

        public TutorialRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<Tutorial> CreatTutorial(Tutorial tutorial)
        {
            var t = new Tutorial();
            t.Name = tutorial.Name;
            var lessons = new List<Lesson>();
            foreach(var l in tutorial.Lessons)
            {
                var les = new Lesson
                {Name = l.Name,
                Documentaion = l.Documentaion,
                VedioId = l.VedioId
                };
                lessons.Add(l);
            }
            t.Lessons = lessons;
            await db.Tutorials.AddAsync(t);
            await db.SaveChangesAsync();
            return t; 
        }

        

        public async Task<Tutorial> GetTutorialById(int id)
        {
            return await db.Tutorials
            .Include(x=> x.Lessons)
            .SingleOrDefaultAsync(x=> x.Id == id);
            
            
        }

        public async Task<Tutorial> GetTutorialByName(string name)
        {
            return await db.Tutorials.Include(x=> x.Lessons)
            .SingleOrDefaultAsync(x=> x.Name == name);
        }

        public async Task<IEnumerable<Tutorial>> GetTutorialsAsync()
        {
            return await db.Tutorials.Include(x=> x.Lessons)
            .ToListAsync();
        }
    }
}