using AutoMapper;
using Server.Api.Entities;
using Server.Api.Dtos;

namespace Server.Api.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper(){
            CreateMap<Team, TeamDto>().PreserveReferences();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}