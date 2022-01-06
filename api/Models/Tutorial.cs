using System.Collections.Generic;

namespace api.Models
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}