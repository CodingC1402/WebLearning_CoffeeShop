using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Models;
using WebAPI.DTOs.RandomInfoAPI;

namespace WebAPI.Data.Validator;

public class EmployeeValidator : PersonValidator<Employee>
{
    const string RESIGN_DATE_ERROR = "Resign date has to be greater than the starting date";
    const string START_DATE_ERROR = "Start date has to be greater than the DOB";

    public EmployeeValidator()
    {
        RuleFor(x => x.ResignDate).Custom((resignDate, context) => {
            if (resignDate == null || resignDate > context.InstanceToValidate.StartDate) 
                return;

            context.AddFailure(new ValidationFailure(nameof(context.InstanceToValidate.ResignDate), RESIGN_DATE_ERROR));
        });

        RuleFor(x => x.StartDate).GreaterThan(x => x.DOB).WithMessage(START_DATE_ERROR);
    }
}