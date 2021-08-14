using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Server.Api.Entities
{
    public record Course
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }
        [Required]
        public DateTimeOffset CreatedDate { get; init; }
        public virtual ICollection<Team> Teams {get; init;}
        public virtual ICollection<Iteration> Iterations {get; init;}
    }
}
