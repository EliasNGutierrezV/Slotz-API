using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Slotz_API.Data;
using Slotz_API.Models;
using Slotz_API.Repositories.Interfaces;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;


namespace Slotz_API.Repositories
{
    public class AccessService : IAccess
    {
        private readonly dbContext _dbContext;
        private readonly IConfiguration _config;

        public AccessService(dbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public RequestModel Authentication(LoginRequest loginRequest)
        {
            if (loginRequest == null) return null;

            var finded = _dbContext.user.FirstOrDefault(x =>
            x.Email == loginRequest.Email && x.Password == loginRequest.Password);

            if (finded == null) return null;

            string tokengenerated = GenerateToken(finded.IdUser.ToString());

            return new RequestModel { IdUser = finded.IdUser, Email = finded.Email, Status = finded.Status, Username = finded.Username, Password = finded.Password, Role = finded.Role, Token = tokengenerated};
        }

        public bool Exists(User user)
        {
            if (user == null) throw new ArgumentNullException();

            var finded = _dbContext.user.FirstOrDefault(x =>
            x.Username == user.Username || x.Email == user.Email);

            if (finded == null) return false;

            return true;
        }

        private string GenerateToken(string idUser)
        {
            var key = _config.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));

            var TokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = TokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string createdToken = tokenHandler.WriteToken(tokenConfig);

            return createdToken;
        }

    }
}
