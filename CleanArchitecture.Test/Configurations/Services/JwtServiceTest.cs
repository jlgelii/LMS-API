using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Services
{
    public class JwtServiceTest : IJwtServices
    {
        public string CreateToken(TokenData loginUser)
        {
            return "token";
        }

        public TokenData GetLoggedUser()
        {
            return new TokenData { UserId = 1 };
        }
    }
}
