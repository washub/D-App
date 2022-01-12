using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapp.API.Data;
using Dapp.API.DTOs;
using Dapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dapp.API.Controllers
{
   
    [Route("api/[controller]")]
     [ApiController]
    public class AuthController: Controller
    {
        public IAuthRepository _repo { get; }
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDto userRegisterDto){
            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();
            if(await _repo.UserExists(userRegisterDto.UserName))
                return BadRequest(string.Format("Sorry this username -> {0} is already taken",userRegisterDto.UserName));
            
            var user = new User{UserName = userRegisterDto.UserName};
            user = await _repo.Register(user, userRegisterDto.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserForLoginDto userLoginDto){
            var userFromRepo = await _repo.Login(userLoginDto.UserName, userLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();


            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
            };
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescreptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddMinutes(10),
                SigningCredentials = sign
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescreptor);
            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });



        }
    }
}