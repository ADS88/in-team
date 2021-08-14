using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    public record CreateStateDto
    {
        [Required]
        public String Name { get; init; }

    }
}