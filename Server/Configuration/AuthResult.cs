using System.Collections.Generic;

namespace Server.Configuration
{
  public class AuthResult
  {
    public string Token {get; set;}

    public bool Success {get; set;}

    public List<string> Errors {get; set;}
  }
}