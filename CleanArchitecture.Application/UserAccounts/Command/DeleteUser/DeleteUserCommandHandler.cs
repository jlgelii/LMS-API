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

namespace CleanArchitecture.Application.UserAccounts.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<UserAccount>>
    {
        private readonly ILMSDbContext _context;
        private readonly IJwtServices _jwtServices;
        private readonly IDateTimeService _dateTimeService;

        public DeleteUserCommandHandler(ILMSDbContext context, 
            IJwtServices jwtServices,
            IDateTimeService dateTimeService)
        {
            this._context = context;
            this._jwtServices = jwtServices;
            this._dateTimeService = dateTimeService;
        }

        public async Task<Response<UserAccount>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccount
                                     .SingleOrDefaultAsync(u => u.Id == request.Id
                                                             && (u.Deleted == false || u.Deleted == null));

            if (user == null)
                return await Task.FromResult(Response.Fail(user, "User does not exist."));

            user.Deleted = true;
            user.DeletedBy = _jwtServices.GetLoggedUser().UserId;
            user.DeletedDate = _dateTimeService.Now;

            _context.SaveChanges();

            return Response.Success(user);
        }
    }
}
