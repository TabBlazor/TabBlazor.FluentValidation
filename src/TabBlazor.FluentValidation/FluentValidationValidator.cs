using Microsoft.AspNetCore.Components.Forms;
using TabBlazor.Validation;

namespace TabBlazor.FluentValidation;

public class FluentValidationValidator : IFormValidator
{
    private readonly IServiceProvider provider;

    public FluentValidationValidator(IServiceProvider provider)
    { 
        this.provider = provider;
    }

    public void EnableValidation(EditContext editContext)
    {
        editContext.AddFluentValidation(provider, new[] { "default" });
    }
}