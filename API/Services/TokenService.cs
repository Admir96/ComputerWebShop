﻿using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
           };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials= creds
            };

            var TokenHandler = new JwtSecurityTokenHandler();

            var token = TokenHandler.CreateToken(TokenDescription);

            return TokenHandler.WriteToken(token);
        }
    }
}
