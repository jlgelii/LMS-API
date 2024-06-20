using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.DeleteUser
{
    public class DeleteUserCommandValidator : IValidationHandler<DeleteUserCommand>
    {
        public async Task<ValidationResults> Validate(DeleteUserCommand request)
        {
            if (request.Id <= 0)
                return ValidationResults.Fail("Invalid user id.");

            return ValidationResults.Success;
        }
    }
}
