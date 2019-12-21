using Backend.Security.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace Backend.Security.Implementations
{
    public class JWTTokenizer : ITokenizer
    {
        const string secret = "Some really really safe secert";
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        public IDictionary<string, string> DeTokenize(string token)
        {
            byte[] key = Encoding.ASCII.GetBytes(secret);
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            try
            {
                return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _)
                                   .Claims.ToDictionary(u => u.Type, u => u.Value);
            }
            catch(Exception)
            {
                return null;
            }
        }
        public string Tokenize(IDictionary<string, string> claims)
        {
            byte[] key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value))),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}