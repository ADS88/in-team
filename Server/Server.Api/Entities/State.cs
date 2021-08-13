using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public class State
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

        [Required]
        public Alpha Alpha { get; init; }

        [Required]
        public int AlphaId { get; init; }
    }
}