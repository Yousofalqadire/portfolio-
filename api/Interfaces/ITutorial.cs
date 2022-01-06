using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface ITutorial
    {
         Task<Tutorial> CreatTutorial(Tutorial tutorial);
         Task<IEnumerable<Tutorial>> GetTutorialsAsync();
         Task<Tutorial> GetTutorialById(int id);
         //Task<Tutorial> UpdateTutorialAsync(Tutorial tutorial);
        Task<Tutorial> GetTutorialByName(string name);
        
         
    }
}