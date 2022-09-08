using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace TabBlazor.FluentValidation;

public static class EditContextFluentValidationExtensions
{
    public static IDisposable AddFluentValidation(this EditContext editContext, IServiceProvider serviceProvider,
        bool disableAssemblyScanning, IValidator validator, FluentValidationValidator fluentValidationValidator)
    {
        return new FluentValidationSubscription(editContext, serviceProvider, disableAssemblyScanning, validator,
            fluentValidationValidator);
    }
}