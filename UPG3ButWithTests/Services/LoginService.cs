using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UPG3ButWithTests.Repository.Interfaces;
using UPG3ButWithTests.Services.Interfaces;
using UPG3ButWithTests.Models;

namespace UPG3ButWithTests.Services
{
    public class LoginService (ILoginRepo loginRepo, IConfiguration configuration) : ILoginService
    {
        private readonly ILoginRepo _loginRepo = loginRepo;
        private readonly IConfiguration _configuration = configuration;

        public string Login(string loginKey)
        {
            Login login = _loginRepo.Login(loginKey);
            if (login != null)
            {
               return GenerateToken(login.LoginId, login.Admin);
            }
            return "Invalid Credentials";
        }

        private string GenerateToken(int loginId, string role)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, loginId.ToString()),
        new Claim(ClaimTypes.Role, role.TrimEnd()),
    };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set expiration to 60 minutes
            var expires = DateTime.UtcNow.AddMinutes(60);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5212/",
                audience: "http://localhost:5212/",
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            // Remove double quotes from the token string
            tokenString = tokenString.Replace("\"", string.Empty);

            return tokenString;
        }
    }
}
