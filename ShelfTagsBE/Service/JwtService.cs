using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ShelfTagsBE.Service;

public class JwtService
{
        private readonly string key;

        public JwtService(string key)
    {
        this.key = key;
    }

    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyy = Encoding.ASCII.GetBytes(key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials( 
                new SymmetricSecurityKey(keyy),
                SecurityAlgorithms.HmacSha256Signature
               )

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
        
    }
}
