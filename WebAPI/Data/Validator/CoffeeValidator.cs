using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Data.Models;

namespace WebAPI.Data.Validator;

public class CoffeeValidator : AbstractValidator<Coffee>
{
    const string STRING_TOO_LONG = "Input text is too long or too short!";

    public CoffeeValidator() {
        RuleFor(x => x.Notes).MaximumLength(128).MinimumLength(1).WithMessage(x => BuildMessage(nameof(x.Notes)));
        RuleFor(x => x.Origin).MaximumLength(128).MinimumLength(1).WithMessage(x => BuildMessage(nameof(x.Origin)));
        RuleFor(x => x.Price).ScalePrecision(2, 2);
        RuleFor(x => x.Variety).MaximumLength(128).MinimumLength(1).WithMessage(x => BuildMessage(nameof(x.Variety)));
        RuleFor(x => x.Name).MaximumLength(50).MinimumLength(1).WithMessage(x => BuildMessage(nameof(x.Name)));
    }

    public string BuildMessage(string text) {
        return $"{STRING_TOO_LONG} Field: {text}";
    }
}