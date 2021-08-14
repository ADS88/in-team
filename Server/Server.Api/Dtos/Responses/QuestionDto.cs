using System.ComponentModel.DataAnnotations;
using System;

namespace Server.Api.Dtos
{
    public class QuestionDto
    {
        public int Id { get; init; }

        [Required]
        public String Content { get; init; }
        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        [Required]
        public int StateId { get; init; }
    }
}