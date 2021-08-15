using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public record State
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

        [Required]
        public Alpha Alpha { get; init; }

        [Required]
        public int AlphaId { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}