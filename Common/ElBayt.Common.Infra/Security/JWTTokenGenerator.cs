using ElBayt.Common.Core.ISecurity;
using ElBayt.Common.Core.SecurityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElBayt.Common.Infra.Security
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly IConfiguration _config;
        public JWTTokenGenerator(IConfiguration config) 
        {
            _config = config;
        }

        public string GenerateToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescripator = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescripator);

            return tokenHandler.WriteToken(token);

        }
    }
}
