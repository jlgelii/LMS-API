using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.CreateUser
{
    public class CreateUserCommandValidator : IValidationHandler<CreateUserCommand>
    {
        public async Task<ValidationResults> Validate(CreateUserCommand request)
        {
            if (string.IsNullOrEmpty(request.Username))
                return ValidationResults.Fail("Please input username field.");

            if (string.IsNullOrEmpty(request.Password))
                return ValidationResults.Fail("Please input password field.");

            if (request.Password.Count() < 6)
                return ValidationResults.Fail("Password must atleast 6 characters long.");

            return ValidationResults.Success;
        }
    }
}
