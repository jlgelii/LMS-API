using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Tokens;
using CleanArchitecture.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class JwtService : IJwtServices
    {
        private readonly IHttpContextAccessor _accessor;

        public JwtService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        public string CreateToken(TokenData loginUser)
        {
            var loggedToken = loginUser.ToString();

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("LoggedUser", loggedToken) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(TokenHelper.GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }

        public TokenData GetLoggedUser()
        {
            TokenData token = null;

            var userclaim = _accessor.HttpContext
                                     .User
                                     .Claims
                                     .FirstOrDefault(x => x.Type.Equals("LoggedUser", StringComparison.InvariantCultureIgnoreCase));

            if (userclaim != null)
            {
                token = JsonConvert.DeserializeObject<TokenData>(userclaim.Value);
            }

            return token;
        }
    }
}
