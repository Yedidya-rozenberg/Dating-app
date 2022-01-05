using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Servises
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config )
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CteateToken(AppUser user)
        {
            var claims = new List<Claim>(){
                            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
        var crads = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

        var tokendescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = crads
        };

        var tokenHendler =  new JwtSecurityTokenHandler();
        var token = tokenHendler.CreateToken(tokendescriptor);
        var rtn =  tokenHendler.WriteToken(token);
        return rtn;
        }
    }
}