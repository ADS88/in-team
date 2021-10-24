using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new state for an alpha
    /// </summary>
    public record CreateStateDto
    {
        [Required]
        public String Name { get; init; }

    }
}