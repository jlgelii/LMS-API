using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserAccount>>
    {
        private readonly ILMSDbContext _context;
        private readonly IJwtServices _jwtServices;

        public CreateUserCommandHandler(ILMSDbContext context, IJwtServices jwtServices)
        {
            this._context = context;
            this._jwtServices = jwtServices;
        }

        public async Task<Response<UserAccount>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.UserAccount
                                  .FirstOrDefault(u => u.Username == request.Username
                                                    && (u.Deleted == false || u.Deleted == null));

            if (user != null)
                return await Task.FromResult(Response.Fail(user, "Username already exist."));

            user = new UserAccount
            {
                Username = request.Username,
                Password = request.Password,
            };

            _context.UserAccount.Add(user);
            _context.SaveChanges();

            return Response.Success(user);
        }
    }
}
