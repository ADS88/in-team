using System.Collections.Generic;

namespace Server.Api.Configuration
{
  /// <summary>
  /// The result of whether an authentication attempt was successful
  /// </summary>
  public class AuthResult
  {
    public string Token {get; set;}

    public bool Success {get; set;}

    public List<string> Errors {get; set;}
  }
}