using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Server.Api.Entities

{
    public class AppUser: IdentityUser
    {
        [Required]
        public string FirstName {get; set;}

        [Required]
        public string LastName {get; set;}

        [Required]
        public string ProfileIcon { get; set; }

        public ICollection<Team> Teams { get; set; }
        
        public ICollection<BadgeGift> Badges { get; set; }
    }
}