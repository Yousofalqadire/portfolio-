using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IHomeWork
    {
         Task<HomeWork> GetHomeWorkByIdAsync(int id);
         Task<IEnumerable<HomeWork>> GetHomeWorksAsync();
         Task<HomeWork> AddHomeWorkAsync(HomeWork homeWork);
         Task<HomeWork> UpdateHomeWorkAsync(HomeWork homeWork);
    }
}