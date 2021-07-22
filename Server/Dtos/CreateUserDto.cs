using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Dtos
{
    public record CreateUserDto
    {

        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }
        
    }
}
