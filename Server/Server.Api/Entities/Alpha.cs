using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public record Alpha
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }

    }
}