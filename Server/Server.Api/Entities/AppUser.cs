using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Server.Api.Entities

{
    /// <summary>
    /// Table storing users in the application
    /// </summary>
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ProfileIcon { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<BadgeGift> Badges { get; set; }
    }
}