using System;
using System.Collections.Generic;
using System.Text;
using Core.Aspect.Autofac.Validation;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
    }
}
