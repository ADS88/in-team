using Microsoft.EntityFrameworkCore;
using Server.Entities;
 
namespace Server.Configuration
{
    public class JwtConfig
    {
        public string Secret {get; set;}
    }
}