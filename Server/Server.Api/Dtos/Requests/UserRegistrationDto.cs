using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{

/// <summary>
/// DTO holding the users registration information
/// </summary>
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

    public string LecturerPassword { get; set; }

    public bool IsLecturer { get; set; }

  }
}
