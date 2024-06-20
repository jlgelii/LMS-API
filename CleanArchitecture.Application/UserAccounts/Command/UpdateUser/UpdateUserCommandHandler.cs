using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<UserAccount>>
    {
        private readonly ISampleDbContext _context;
        private readonly IJwtServices _jwtServices;

        public UpdateUserCommandHandler(ISampleDbContext context, IJwtServices jwtServices)
        {
            this._context = context;
            this._jwtServices = jwtServices;
        }

        public async Task<Response<UserAccount>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccount
                                     .FirstOrDefaultAsync(u => u.Id == request.Id
                                                            && (u.Deleted == false || u.Deleted == null));

            if (user == null)
                return await Task.FromResult(Response.Fail(user, "User does not exist"));

            var isExist = await _context.UserAccount
                                        .FirstOrDefaultAsync(u => u.Username == request.Username
                                                               && u.Id != request.Id
                                                               && (u.Deleted == false || u.Deleted == null));

            if (isExist != null)
                return await Task.FromResult(Response.Fail(isExist, "Username already used."));

            user.Username = request.Username;
            user.Password = request.Password;

            _context.SaveChanges();

            return await Task.FromResult(Response.Success(user));
        }
    }
}
