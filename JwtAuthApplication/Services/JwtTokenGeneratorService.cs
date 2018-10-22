namespace JwtAuthApplication.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using JwtAuthApplication.Constants;
    using JwtAuthApplication.Models;

    using Microsoft.IdentityModel.Tokens;

    public class JwtTokenGeneratorService
    {
        public static TokenInfo GenerateToken(User user)
        {
            // create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthConstants.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject = new ClaimsIdentity(new Claim[]
                                                                           {
                                                                               new Claim(ClaimTypes.Name, user.Name)
                                                                           }),
                                          Expires = DateTime.UtcNow.AddMinutes(AuthConstants.TokenLifeTime),
                                          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                                      };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenInfo
                       {
                           Token = tokenHandler.WriteToken(token),
                           ValidFrom = token.ValidFrom,
                           ValidTo = token.ValidTo
                       };
        }
    }
}
