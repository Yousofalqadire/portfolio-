using api.Models;

namespace api.Dtos
{
    public class TutorialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Lesson Lesson { get; set; }
        
    }
}