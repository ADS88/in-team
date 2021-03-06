using System.ComponentModel.DataAnnotations;
namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO holding the users login credentials
    /// </summary>
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}