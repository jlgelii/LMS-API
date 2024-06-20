using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Response<List<UserAccount>>>
    {
        private readonly ISampleDbContext _context;

        public GetUsersQueryHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<List<UserAccount>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _context.UserAccount
                                .Where(u => (u.Deleted == null || u.Deleted == false))
                                .ToList();

            return await Task.FromResult(Response.Success(users));
        }
    }
}
