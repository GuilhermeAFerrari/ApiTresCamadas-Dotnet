using DevIO.Business.Models;
using FluentValidation;

namespace DevIO.Business.Services;

public abstract class BaseService
{
    protected bool ExecuteValidation<TValidator, TEntity>
    (
        TValidator validator,
        TEntity entity
    )
        where TValidator : AbstractValidator<TEntity>
        where TEntity : Entity
    {
        var validatorResult = validator.Validate(entity);

        if (validatorResult.IsValid) return true;

        // Throw notifications

        return false;
    }

    protected void Notifier(string message)
    {

    }
}
