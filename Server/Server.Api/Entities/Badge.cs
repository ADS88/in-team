using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public record Badge
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }
    }

}
