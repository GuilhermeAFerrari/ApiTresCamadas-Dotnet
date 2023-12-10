﻿using FluentValidation;

namespace DevIO.Business.Models.Validations;

public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 200)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 1000)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");
    }
}
