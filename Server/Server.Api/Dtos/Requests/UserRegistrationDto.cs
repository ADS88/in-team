using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{
  public class UserRegistrationDto{

    [Required]
    [EmailAddress]
    public string Email{get; set;}

    [Required]
    [MinLength(2)]
    public string FirstName{get; set;}

    [Required]
    [MinLength(2)]
    public string LastName{get; set;}
    [Required]
    public string Password{get; set;}
  }
}
