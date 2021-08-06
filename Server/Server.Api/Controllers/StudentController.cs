using System.Linq;
using Server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Api.Services;
using System.Collections.Generic;
using AutoMapper;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //Gives controller same name as class (route/items)
    public class StudentController : ControllerBase
    {

        private readonly IUserService service;

        private readonly IMapper mapper;

        public StudentController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllStudents()
        {
            var students = (await service.GetAll()).Select(student => mapper.Map<UserDto>(student));
            return students;
        }

    }
}
