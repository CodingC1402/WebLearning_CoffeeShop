using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Data.Models;

namespace WebAPI.Data.Validator;

public abstract class PersonValidator<T> : AbstractValidator<T> where T : Person
{
    const string NAME_MIN_LENGTH_ERROR = "Your name can't be empty";
    const string NAME_MAX_LENGTH_ERROR = "Your name exceeds the maximum length";
    const string NAME_INVALID_CHARACTER_ERROR = "Your name contains invalid characters";
    const string GENDER_ERROR = "Invalid gender";
    const string PHONE_INVALID_PHONE_ERROR = "Your phone number is not valid";

    public PersonValidator() {
        RuleFor(x => x.FullName)
            .MinimumLength(1).WithMessage(NAME_MIN_LENGTH_ERROR)
            .MaximumLength(128).WithMessage(NAME_MAX_LENGTH_ERROR)
            .Matches(@"^[^!-@[-_{-}]+$").WithMessage(NAME_INVALID_CHARACTER_ERROR);
        RuleFor(x => x.Gender).IsInEnum().WithMessage(GENDER_ERROR);
        RuleFor(x => x.DOB).GreaterThan(DateTime.Today.AddYears(130));
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.PhoneNumber)
            .Matches(@"/^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$/gm")
                .WithMessage(PHONE_INVALID_PHONE_ERROR);
    }
}