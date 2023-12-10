using DevIO.Business.Utils;
using FluentValidation;

namespace DevIO.Business.Models.Validations;

public class SupplierValidation : AbstractValidator<Supplier>
{
    public SupplierValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The field {PropertyName} must be provided")
            .Length(2, 100)
            .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

        When(x => x.SupplierType == ESupplierType.PhysicalPerson, () =>
        {
            RuleFor(x => x.Document.Length).Equal(CpfValidacao.TamanhoCpf)
            .WithMessage("The field {PropertyName} must be {ComparisonValue} chacacters and was provided {PropertyValue}");

            RuleFor(x => CpfValidacao.Validar(x.Document)).Equal(true)
            .WithMessage("The field {PropertyName} is invalid");
        });

        When(x => x.SupplierType == ESupplierType.LegalPerson, () =>
        {
            RuleFor(x => x.Document.Length).Equal(CnpjValidacao.TamanhoCnpj)
            .WithMessage("The field {PropertyName} must be {ComparisonValue} chacacters and was provided {PropertyValue}");

            RuleFor(x => CnpjValidacao.Validar(x.Document)).Equal(true)
            .WithMessage("The field {PropertyName} is invalid");
        });
    }
}
