using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order_Manag.Core.Entites.Identity;
using Order_Manag.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User User, UserManager<User> userManager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,User.DisplayName),
                new Claim(ClaimTypes.Email,User.Email)
            };


            var roles = User.Role.Split(',');   
            foreach (var role in roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));

            }


            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]));


            var Token = new JwtSecurityToken
                (
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssured"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature));


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
