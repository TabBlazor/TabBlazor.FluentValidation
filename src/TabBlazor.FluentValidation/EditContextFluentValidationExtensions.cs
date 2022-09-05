using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components.Forms;

namespace TabBlazor.FluentValidation;

public static class EditContextFluentValidationExtensions
{
    private static readonly char[] separators = new[] { '.', '[' };

    public static EditContext AddFluentValidation(this EditContext editContext, IServiceProvider provider,
        string[] ruleSets)
    {
        if (editContext == null)
        {
            throw new ArgumentNullException(nameof(editContext));
        }

        var messages = new ValidationMessageStore(editContext);

        editContext.OnFieldChanged += (sender, eventArgs)
            => ValidateModel((EditContext)sender, messages, provider, ruleSets);

        editContext.OnValidationRequested += (sender, eventArgs)
            => ValidateModel((EditContext)sender, messages, provider, ruleSets);

        return editContext;
    }

    private static void ValidateModel(EditContext editContext, ValidationMessageStore messages,
        IServiceProvider provider, string[] ruleSets)
    {
        var validator = GetValidatorForModel(editContext.Model, provider);
        if (validator == null)
        {
            return;
        }

        var validationResult = validator.Validate(new ValidationContext<object>(editContext.Model, new PropertyChain(),
            new RulesetValidatorSelector(ruleSets)));
        messages.Clear();
        foreach (var error in validationResult.Errors)
        {
            var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
            messages.Add(fieldIdentifier, error.ErrorMessage);
        }

        editContext.NotifyValidationStateChanged();
    }

    private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
    {
        var obj = editContext.Model;

        while (true)
        {
            var nextTokenEnd = propertyPath.IndexOfAny(separators);
            if (nextTokenEnd < 0)
            {
                return new FieldIdentifier(obj, propertyPath);
            }

            var nextToken = propertyPath.Substring(0, nextTokenEnd);
            propertyPath = propertyPath.Substring(nextTokenEnd + 1);

            object newObj;
            if (nextToken.EndsWith("]"))
            {
                nextToken = nextToken.Substring(0, nextToken.Length - 1);
                var prop = obj.GetType().GetProperty("Item");
                var indexerType = prop.GetIndexParameters()[0].ParameterType;
                var indexerValue = Convert.ChangeType(nextToken, indexerType);
                newObj = prop.GetValue(obj, new object[] { indexerValue });
            }
            else
            {
                var prop = obj.GetType().GetProperty(nextToken);
                if (prop == null)
                {
                    throw new InvalidOperationException(
                        $"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                }

                newObj = prop.GetValue(obj);
            }

            if (newObj == null)
            {
                // This is as far as we can go
                return new FieldIdentifier(obj, nextToken);
            }

            obj = newObj;
        }
    }

    private static IValidator GetValidatorForModel(object model, IServiceProvider provider)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(model.GetType());
        return (IValidator)provider.GetService(validatorType);
    }
}