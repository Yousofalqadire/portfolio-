using api.Dtos;
using api.Models;
using AutoMapper;

namespace api.Profiles
{
    public class ProjectAutoMaper : Profile
    {
        public ProjectAutoMaper()
        {
            CreateMap<Lesson,LessonDto>();
        }
    }
}