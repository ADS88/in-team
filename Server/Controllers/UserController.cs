using System;
using System.Collections.Generic;
using System.Linq;
using Server.Dtos;
using Server.Entities;
using Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace apitestreal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //Gives controller same name as class (route/items)
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository repository;

        public UserController(IUsersRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            var user = repository.GetUser(id);
            if (user is null)
            {
                return NotFound();
            }
            return user.AsDto();
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser(CreateUserDto userDto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.AsDto());
        }
    }
}
