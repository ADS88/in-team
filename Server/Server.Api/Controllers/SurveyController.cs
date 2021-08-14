using Microsoft.AspNetCore.Mvc;
using Server.Api.Services;
using AutoMapper;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SurveyController
    {
        private readonly ISurveyService service;

        private readonly IMapper mapper;

        public SurveyController(ISurveyService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}