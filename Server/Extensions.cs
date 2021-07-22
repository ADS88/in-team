using System;
using Server.Dtos;
using Server.Entities;

namespace apitestreal
{
    public static class Extensions {
        public static UserDto AsDto(this User user) {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = user.CreatedDate
            };
        }
    }
}
