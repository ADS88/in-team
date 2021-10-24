using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new alpha
    /// </summary>
    public record CreateAlphaDto
    {
        [Required]
        public String Name { get; init; }

    }
}