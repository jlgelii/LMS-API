using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<Response<List<UserAccount>>>
    {
    }
}
