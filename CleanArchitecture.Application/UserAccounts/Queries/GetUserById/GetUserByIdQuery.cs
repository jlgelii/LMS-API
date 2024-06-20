using CleanArchitecture.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdDto>>
    {
        public int Id { get; }
        
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
