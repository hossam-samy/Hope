using FluentValidation;
using FluentValidation.Results;
using Hope.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Common
{
    public class GlobalValidator<T> where T : class
    {
        private readonly IValidator<T> validator;

        public GlobalValidator(IValidator<T> validator)
        {
            this.validator = validator;
        }

       public async Task<ValidationResult> ValidateAsync(T model)
        {
            var result = await validator.ValidateAsync(model);

            return result;
        }
    }
}
