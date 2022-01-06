using Microsoft.AspNetCore.Http;

namespace api.Dtos
{
    public class SkillDto
    {
          public string Name { get; set; }
        public IFormFile Certification { get; set; }
    }
}