using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public class Question
    {
        public int Id { get; init; }

        [Required]
        public String Content { get; init; }

        [Required]
        public State State { get; init; }

        [Required]
        public int StateId { get; init; }
    }
}