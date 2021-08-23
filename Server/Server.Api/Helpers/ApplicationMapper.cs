using AutoMapper;
using Server.Api.Entities;
using Server.Api.Dtos;

namespace Server.Api.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper(){
            CreateMap<Team, TeamDto>().PreserveReferences();
            CreateMap<Alpha, AlphaDto>().PreserveReferences();
            CreateMap<State, StateDto>().PreserveReferences();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<SurveyAttempt, SurveyAttemptDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<Iteration, IterationDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Survey, SurveyQuestionsDto>().PreserveReferences();
        }
    }
}