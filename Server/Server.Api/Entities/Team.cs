
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace Server.Api.Entities

{
    /// <summary>
    /// Table storing teams within courses
    /// </summary>
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

        [Required]
        public int Points { get; set; }

        public virtual ICollection<AppUser> Members { get; set; }
        
        public virtual ICollection<Survey> Surveys { get; set; }

    }
}
