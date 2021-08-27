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


namespace Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly JwtConfig jwtConfig;

        public AuthManagementController(UserManager<AppUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor){
            this.userManager = userManager;
            jwtConfig = optionsMonitor.CurrentValue;
        }

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
            var jwtToken = GenerateJwtToken(existingUser);
            var roles = await userManager.GetRolesAsync(existingUser);
            return Ok(new UserRegistrationResponseDto(){
                    Success = true,
                    Token = jwtToken,
                    Role = roles[0]
                });
        }

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

            var newUser = new AppUser() {UserName = user.Email, Email = user.Email, FirstName=user.FirstName, LastName=user.LastName};
            var isCreated = await userManager.CreateAsync(newUser, user.Password);

            if(isCreated.Succeeded){
                await userManager.AddToRoleAsync(newUser, Roles.STUDENT);
                var jwtToken = GenerateJwtToken(newUser);
                return Ok(new UserRegistrationResponseDto(){
                    Success = true,
                    Token = jwtToken,
                    Role = Roles.STUDENT
                });
            } else {
                return BadRequest(new UserRegistrationResponseDto(){
                    Errors = isCreated.Errors.Select(err => err.Description).ToList(),
                    Success = false
                }); 
            }

        }
        private string GenerateJwtToken(IdentityUser user){
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new [] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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