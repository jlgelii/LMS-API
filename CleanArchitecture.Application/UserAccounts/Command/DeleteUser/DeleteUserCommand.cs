using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<UserAccount>>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
