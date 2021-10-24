using AutoMapper;
using Server.Api.Entities;
using Server.Api.Dtos;

namespace Server.Api.Helpers
{
    /// <summary>
    /// Class to automatically map database objects to DTOs used in HTTP responses
    /// </summary>
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper(){
            CreateMap<Team, TeamDto>().PreserveReferences();
            CreateMap<Alpha, AlphaDto>().PreserveReferences();
            CreateMap<State, StateDto>().PreserveReferences();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<SurveyAttempt, SurveyAttemptDto>().ReverseMap();
            CreateMap<Team, SimplifiedTeamDto>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, FullUserDto>();
            CreateMap<Iteration, IterationDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Survey, SurveyQuestionsDto>().PreserveReferences();
            CreateMap<Badge, BadgeDto>().ReverseMap();
        }
    }
}