using FluentValidation;

namespace DevIO.Business.Models.Validations;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 200)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.District)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 100)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(8)
            .WithMessage("The field {PropertyName} must be {MaxLength} characters");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 100)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(1, 50)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");
    }
}
