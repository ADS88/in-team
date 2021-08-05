using System.ComponentModel;
using System.Collections.ObjectModel;
using System;
using Server.Api.Dtos;
using Server.Api.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Server.Api
{
    public static class Extensions {
        public static CourseDto AsDto(this Course course) {
            ICollection<TeamDto> teams = course.Teams is null ? new Collection<TeamDto>() : course.Teams.Select(team => team.AsDto()).ToList();
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                CreatedDate = course.CreatedDate,
                Teams = teams
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

        public static UserDto AsDto(this AppUser user) {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
