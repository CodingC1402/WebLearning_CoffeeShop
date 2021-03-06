using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Data.Models;

namespace WebAPI.Data.Validator
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.Count).LessThan(100).GreaterThan(0);
        }
    }
}