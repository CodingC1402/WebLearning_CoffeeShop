using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Data.Models;

namespace WebAPI.Data.Validator
{
    public class ShopValidator : AbstractValidator<Shop>
    {
        public ShopValidator() {
            RuleFor(x => x.Address).MaximumLength(100);
            RuleFor(x => x.Phone).MaximumLength(22);
        }
    }
}