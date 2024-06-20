using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<GetUserByIdDto>>
    {
        private readonly ILMSDbContext _context;

        public GetUserByIdQueryHandler(ILMSDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _context.UserAccount
                               .Select(u => new GetUserByIdDto
                               {
                                   Id = u.Id,
                                   Password = u.Password,
                                   Username = u.Username
                               })
                               .FirstOrDefault(u => u.Id == request.Id);

            if (user == null)
                return await Task.FromResult(Response.Fail(user, "User does not exist."));

            return await Task.FromResult(Response.Success(user));
        }
    }
}
