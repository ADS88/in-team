using Server.Api.Configuration;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO showing the ID and role of a user once they log in 
    /// </summary>
    public class UserRegistrationResponseDto : AuthResult
    {
        public string Role { get; set; }
        public string Id { get; set; }
    }
}
