using System;
using Server.Api.Dtos;
using Server.Api.Entities;

namespace Server.Api
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
