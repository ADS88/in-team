using System;
using System.ComponentModel.DataAnnotations;


namespace Server.Api.Dtos
{
    /// <summary>
    /// Information about a single badge
    /// </summary>
    public record BadgeDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }
    }
}