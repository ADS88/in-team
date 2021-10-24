using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table storing badges
    /// </summary>
    public record Badge
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }
    }

}
