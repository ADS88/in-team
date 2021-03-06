using System;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing information about a course
    /// </summary>
    public record CourseDto
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public virtual ICollection<TeamDto> Teams { get; init ;}
        public virtual ICollection<IterationDto> Iterations { get; init; }
    }
}
