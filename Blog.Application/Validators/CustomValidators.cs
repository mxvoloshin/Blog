using System;
using FluentValidation;

namespace Blog.Application.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, DateTime?> IsNotFutureDate<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
        {
            return ruleBuilder.Must(x => x != null && x.Value > DateTime.UtcNow).WithMessage("Is future date.");
        }
    }
}