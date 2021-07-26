using System;
namespace Server.Api.Dtos
{
    public record CourseDto
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
