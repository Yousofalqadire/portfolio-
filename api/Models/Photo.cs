using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
     [Table("Photos")]
    public class Photo
    {
         public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public Skill Skill { get; set; }
        public int SkillId  { get; set; }
    }
}