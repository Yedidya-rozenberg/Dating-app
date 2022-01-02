using System.Security.Cryptography;
using System.Net;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController :BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext Context)
        {
         _context = Context;   
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            if (await UserExist(registerDto.Username)) return BadRequest("The user name alredy exist");

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return(user);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto LoginDto)
        {
            var user = await this._context.AppUsers.SingleOrDefaultAsync(x=>x.UserName == LoginDto.UserName.ToLower());
            if (user == null) return Unauthorized("invalid username");
            
            using var hmac = new HMACSHA512(user.PasswordSalt)
            var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(LoginDto.Password));
            for (int i=0; i<ComputeHash.Length; i++)
            {
                if (ComputeHash[i]!= user.PasswordHash[i]) return Unauthorized("invalid password");
            }
            return user;
        }

    
    private async Task<bool> UserExist(string UserName)
    {
        return await _context.AppUsers.AnyAsync(x=>x.UserName == UserName.ToLower());
    }

    }
}