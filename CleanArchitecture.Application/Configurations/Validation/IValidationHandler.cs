using CleanArchitecture.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Configurations.Validation
{
    public interface IValidationHandler { }
    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResults> Validate(T request);
    }
}
