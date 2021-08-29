using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Server.Api.Entities;
namespace Server.Api.Dtos
{
     public record FullUserDto
    {
        [Required]
        public String Id { get; init; }

        [Required]
        public String FirstName { get; init; }

        [Required]
        public String LastName { get; init; }

        [Required]
        public String ProfileIcon { get; init; }

        [Required]
        public ICollection<TeamDto> Teams { get; init; }

        [Required]
        public ICollection<BadgeGift> BadgeGifts { get; init; }

    }
}