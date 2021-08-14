using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    public record CreateAlphaDto
    {
        [Required]
        public String Name { get; init; }

    }
}