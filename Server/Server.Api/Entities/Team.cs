
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace Server.Api.Entities

{
    public record Team
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }
        [Required]
        public DateTimeOffset CreatedDate { get; init; }

        [Required]
        public Course Course { get; init; }
        [Required]
        public int CourseId { get; init; }

        public ICollection<AppUser> Members { get; set; }

    }
}