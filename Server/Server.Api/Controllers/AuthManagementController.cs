using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Server.Api.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Server.Api.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using Server.Api.Enums;
using Server.Api.Entities;
using Microsoft.Extensions.Configuration;


namespace Server.Api.Controllers
{
    /// <summary>
    /// Controller containing endpoints related to authentication and authorization
    /// </summary>
    [Route("api/[controller]")]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly JwtConfig jwtConfig;

        private readonly IConfiguration configuration;

        public AuthManagementController(UserManager<AppUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, IConfiguration configuration){
            this.userManager = userManager;
            jwtConfig = optionsMonitor.CurrentValue;
            this.configuration = configuration;
        }

        /// <summary>
        /// Endpoint that allows a user to login to the application
        /// </summary>
        /// <param name="user">A login request containing the users credentials</param>
        /// <returns>a JWT token if the login is successful, else appropriate error</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user){
            if(!ModelState.IsValid){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Invalid payload"
                    },
                    Success = false
                });
            }
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if(existingUser == null){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Invalid login request"
                    },
                    Success = false
                });
            }

            var isCorrectPassword = await userManager.CheckPasswordAsync(existingUser, user.Password);
            if(!isCorrectPassword){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Invalid login request"
                    },
                    Success = false
                });
            }
            var roles = await userManager.GetRolesAsync(existingUser);
            var jwtToken = GenerateJwtToken(existingUser, roles[0]);
            return Ok(new UserRegistrationResponseDto(){
                    Success = true,
                    Token = jwtToken,
                    Role = roles[0],
                    Id = existingUser.Id
                });
        }

        /// <summary>
        /// Endpoint that allows a user to register a new account 
        /// </summary>
        /// <param name="user">Details about the user and login credentials</param>
        /// <returns>A 201 response if creation is successful, else appropriate HTTP error code</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user){
            if(!ModelState.IsValid){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Invalid payload"
                    },
                    Success = false
                });
            }

            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if(existingUser != null){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Email already in use"
                    },
                    Success = false
                });
            }

            var secretLecturerPassword = configuration["LecturerPassword:Password"];
            
            if(user.IsLecturer && user.LecturerPassword != secretLecturerPassword){
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = new List<string>{
                        "Wrong lecturer password"
                    },
                    Success = false
                });
            }

            var newUser = new AppUser() {
                UserName = user.Email, 
                Email = user.Email,
                FirstName = user.FirstName, 
                LastName = user.LastName,
                ProfileIcon = "angler" 
                };
            var isCreated = await userManager.CreateAsync(newUser, user.Password);

            if(isCreated.Succeeded){
                var newUserRole = user.IsLecturer ? Roles.LECTURER : Roles.STUDENT;
                await userManager.AddToRoleAsync(newUser, newUserRole);
                var jwtToken = GenerateJwtToken(newUser, newUserRole);
                return Ok(new UserRegistrationResponseDto(){
                    Success = true,
                    Token = jwtToken,
                    Role = newUserRole,
                    Id = newUser.Id
                });
            } else {
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = isCreated.Errors.Select(err => err.Description).ToList(),
                    Success = false
                }); 
            }

        }

        /// <summary>
        /// Generates a JWT token to use for further requests
        /// </summary>
        /// <param name="user">The user to generate the token for</param>
        /// <param name="role">The role the user has in the application for authorization</param>
        /// <returns>a JWT token as a String</returns>
        private string GenerateJwtToken(IdentityUser user, string role){
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new [] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}