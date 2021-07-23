using System;
using Server.Dtos;
using Server.Entities;

namespace Server
{
    public static class Extensions {
        public static CourseDto AsDto(this Course course) {
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                CreatedDate = course.CreatedDate
            };
        }
    }
}
