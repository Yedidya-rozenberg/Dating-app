using System.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Servises;
using Microsoft.Extensions.Configuration;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController :BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext Context,ITokenService tokenService)
        {
         _context = Context;
         _tokenService = tokenService; 
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            if (await UserExist(registerDto.Username)) return BadRequest("The user name alredy exist");

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();
            return new UserDto 
            {
                Username = user.UserName,
                Token = _tokenService.CteateToken(user),
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto LoginDto)
        {
            var user = await this._context.Users.SingleOrDefaultAsync(x=>x.UserName == LoginDto.UserName.ToLower());
            if (user == null) return Unauthorized("invalid username");
            
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoginDto.Password));
            for (int i=0; i<ComputeHash.Length; i++)
            {
                if (ComputeHash[i]!= user.PasswordHash[i]) return Unauthorized("invalid password");
            }
            return new UserDto 
            {
                Username = user.UserName,
                Token = _tokenService.CteateToken(user),
                PhotoUrl = user.Photos?.FirstOrDefault(p=>p.IsMain)?.Url

            };
        }

    
    private async Task<bool> UserExist(string UserName)
    {
        return await _context.Users.AnyAsync(x=>x.UserName == UserName.ToLower());
    }

    }
}