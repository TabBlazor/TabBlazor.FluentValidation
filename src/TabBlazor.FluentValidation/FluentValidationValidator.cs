using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TabBlazor.FluentValidation;

public class FluentValidationValidator : ComponentBase, IDisposable
{
    private IDisposable _subscriptions;
    [Inject] private IServiceProvider ServiceProvider { get; set; } = default!;
    [CascadingParameter] private EditContext CurrentEditContext { get; set; }
    [Parameter] public IValidator Validator { get; set; }
    [Parameter] public bool DisableAssemblyScanning { get; set; }
    [Parameter] public Action<ValidationStrategy<object>> Options { get; set; }
    internal Action<ValidationStrategy<object>> ValidateOptions { get; set; }

    public bool Validate(Action<ValidationStrategy<object>> options = null)
    {
        if (CurrentEditContext is null)
        {
            throw new NullReferenceException(nameof(CurrentEditContext));
        }

        ValidateOptions = options;

        try
        {
            return CurrentEditContext.Validate();
        }
        finally
        {
            ValidateOptions = null;
        }
    }

    public async Task<bool> ValidateAsync(Action<ValidationStrategy<object>> options = null)
    {
        if (CurrentEditContext is null)
        {
            throw new NullReferenceException(nameof(CurrentEditContext));
        }

        ValidateOptions = options;

        try
        {
            CurrentEditContext.Validate();

            if (CurrentEditContext!.Properties.TryGetValue(
                    FluentValidationSubscription.PendingAsyncValidation, out var asyncValidationTask))
            {
                await (Task<ValidationResult>)asyncValidationTask;
            }


            return !CurrentEditContext.GetValidationMessages().Any();
        }
        finally
        {
            ValidateOptions = null;
        }
    }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(FluentValidationValidator)} requires a cascading " +
                                                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidationValidator)} " +
                                                $"inside an {nameof(EditForm)}.");
        }

        _subscriptions = CurrentEditContext.AddFluentValidation(ServiceProvider, DisableAssemblyScanning, Validator, this);
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}