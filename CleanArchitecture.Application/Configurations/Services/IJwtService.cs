using CleanArchitecture.Domain.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Configurations.Services
{
    public interface IJwtServices
    {
        string CreateToken(TokenData loginUser);
        TokenData GetLoggedUser();
    }
}
