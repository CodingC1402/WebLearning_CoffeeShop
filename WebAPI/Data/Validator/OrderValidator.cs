using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Data.Models;

namespace WebAPI.Data.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Total).ScalePrecision(2, 4);
            RuleFor(x => x.Details).NotEmpty();
        }
    }
}