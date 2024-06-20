using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.UpdateUser
{
    public class UpdateUserCommandValidator : IValidationHandler<UpdateUserCommand>
    {
        public async Task<ValidationResults> Validate(UpdateUserCommand request)
        {
            if (string.IsNullOrEmpty(request.Username))
                return ValidationResults.Fail("Please input username.");

            if (string.IsNullOrEmpty(request.Password))
                return ValidationResults.Fail("Please input password.");

            if (request.Password.Length < 6)
                return ValidationResults.Fail("Password must contain atleast 6 characters.");

            return ValidationResults.Success;
        }
    }
}
