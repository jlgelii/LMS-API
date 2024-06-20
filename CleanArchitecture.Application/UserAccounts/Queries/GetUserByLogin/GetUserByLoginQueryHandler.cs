using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Tokens;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, Response<GetUserByLoginDto>>
    {
        private readonly ILMSDbContext _context;
        private readonly IJwtServices _jwtServices;

        public GetUserByLoginQueryHandler(ILMSDbContext context, IJwtServices jwtServices)
        {
            this._context = context;
            this._jwtServices = jwtServices;
        }

        public async Task<Response<GetUserByLoginDto>> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccount
                                     .Where(u => u.Username == request.Username
                                              && u.Password == request.Password
                                              && (u.Deleted == false || u.Deleted == null))
                                     .Select(u => new GetUserByLoginDto
                                     {
                                         Id = u.Id,
                                         Username = u.Username,
                                         Password = u.Password,
                                     })
                                     .FirstOrDefaultAsync();

            if (user == null)
                return await Task.FromResult(Response.Fail(user, "User does not exist."));

            user.Token = _jwtServices.CreateToken(new TokenData
            {
                UserId = user.Id,
            });

            return Response.Success(user);
        }
    }
}
