using EQBAuth.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EQBAuth.API.Services
{
    public class AuthService: IAuthService
    {
        IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public async Task<string> GenerateToken(Models.Request.ReqAuth auth)
        {
            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, auth.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims,
                //expires: DateTime.Now.AddMinutes(1),
                expires:DateTime.Now.AddHours(24),

                signingCredentials: credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

    }

    public interface IAuthService
    {
        Task<string> GenerateToken(Models.Request.ReqAuth auth);
    
    }
}
