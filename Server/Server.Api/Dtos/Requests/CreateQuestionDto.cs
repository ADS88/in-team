using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO to create a new question for a State
    /// </summary>
    public record CreateQuestionDto
    {
        [Required]
        public String Content { get; init; }

    }
}