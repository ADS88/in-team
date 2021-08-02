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

        public static TeamDto AsDto(this Team team) {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                CreatedDate = team.CreatedDate,
                Members = team.Members
            };
        }
    }
}
