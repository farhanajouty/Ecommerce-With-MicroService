using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(JwtOptions jwtOptions) { 
          _jwtOptions = jwtOptions;
        }
        public string GenerateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key= Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimlist = new List<Claim>()
            {
             new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
             new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
             new Claim(JwtRegisteredClaimNames.Name,applicationUser.Name),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimlist),
                Expires = DateTime.UtcNow.AddDays(7),
            };

            return "";
        }
    }
}
