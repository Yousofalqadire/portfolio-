using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Lessons")]
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VedioId { get; set; }
        public string Documentaion { get; set; }
        public Tutorial Tutorial { get; set; }
        public int TutorialId { get; set; }
    }
}