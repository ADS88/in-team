using System.ComponentModel.DataAnnotations;
namespace Server.Api.Dtos
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}