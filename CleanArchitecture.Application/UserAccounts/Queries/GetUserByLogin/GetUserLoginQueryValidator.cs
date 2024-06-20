using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin
{
    public class GetUserLoginQueryValidator : IValidationHandler<GetUserByLoginQuery>
    {
        public async Task<ValidationResults> Validate(GetUserByLoginQuery request)
        {
            if (string.IsNullOrEmpty(request.Username))
                return ValidationResults.Fail("Please input username.");

            if (string.IsNullOrEmpty(request.Password))
                return ValidationResults.Fail("Please input password.");

            return ValidationResults.Success;
        }
    }
}
