using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    public record CreateQuestionDto
    {
        [Required]
        public String Content { get; init; }

    }
}