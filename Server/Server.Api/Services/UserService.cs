using static System.StringComparison;
using Server.Api.Entities;
using Server.Api.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Server.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<AppUser>> GetAll(){
            return await repository.GetAll();
        }
        public async Task<IEnumerable<AppUser>> GetEligibleForCourse(int courseId, string searchString){
            var allUsers = await repository.GetAllWithTeams();
            return allUsers.Where(user => FilterByName(user, searchString))
            .Where(user => EligibleForCourse(user, courseId))
            .Where(user => IsStudent(user));
        }

        private Boolean EligibleForCourse(AppUser user, int courseId){
            return !user.Teams.Any(team => team.CourseId == courseId);
        }

        private Boolean FilterByName(AppUser user, string searchString){
            return user.LastName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) || user.FirstName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase);
        }

        private Boolean IsStudent(AppUser user){
            return true;
        }
    }
}