using System.ComponentModel.DataAnnotations;
using Server.Api.Configuration;

namespace Server.Api.Dtos
{
  public class UserRegistrationResponseDto: AuthResult {
      public string Role { get; set; }
      public string Id { get; set; }
  }
}
