using System.ComponentModel.DataAnnotations;
namespace Server.Dtos
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string password {get; set;}
    }
}