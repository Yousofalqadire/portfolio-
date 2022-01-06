using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Reposetory
{
    public class HomeWorkRepository : IHomeWork
    {
        private readonly ApplicationDbContext db;

        public HomeWorkRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public  async Task<HomeWork> AddHomeWorkAsync(HomeWork homeWork)
        {
            var result = new HomeWork{Date= homeWork.Date, FileUrl = homeWork.FileUrl};
            await db.HomeWorks.AddAsync(result);
            await db.SaveChangesAsync();
            return result;
        }

        public async Task<HomeWork> GetHomeWorkByIdAsync(int id)
        {
            var result = await db.HomeWorks.SingleOrDefaultAsync(x=> x.Id == id );
            return result;
        }

        public async Task<IEnumerable<HomeWork>> GetHomeWorksAsync()
        {
            var result = await db.HomeWorks.ToListAsync();
            return result;
        }

        

        public async Task<HomeWork> UpdateHomeWorkAsync(HomeWork homeWork)
        {
            var result = await db.HomeWorks.SingleOrDefaultAsync(x=> x.Id == homeWork.Id);
            if(result != null)
            {
                result.Date = homeWork.Date;
                result.FileUrl = homeWork.FileUrl;
                
            }
            await db.SaveChangesAsync();
            return result;
        }
    }
}