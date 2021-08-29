using System;
using System.ComponentModel.DataAnnotations;


namespace Server.Api.Dtos
{
    public record UserBadgeDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

        [Required]
        public int Count { get; init; }
    }
}