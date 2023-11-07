using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfinityDapper.Services.Common
{
    public class CommonMethods
    {
        public static string GenerateJwtToken(int userId, string userName, string emailId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string Secret = "eyJhbGciOiJIUzI1NiIsInJ5cCI6IkpXVCJ9";

            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", userId.ToString()) ,
                                                     new Claim("Name", userName),
                                                     new Claim("Email", emailId)}),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
