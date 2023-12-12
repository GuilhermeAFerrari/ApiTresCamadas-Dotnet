using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Notifiers;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services;

public abstract class BaseService
{
    private readonly INotifier _notifier;

    protected BaseService(INotifier notifier)
    {
        _notifier = notifier;
    }

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

        Notify(validatorResult);

        return false;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors)
            Notify(item.ErrorMessage);
    }

    protected void Notify(string message) =>_notifier.Handle(new Notification(message));
}
