using Microsoft.AspNetCore.Components.Forms;

namespace TabBlazor.FluentValidation;

public class TabBlazorFluentValidationValidator : IFormValidator
{
    public async Task<bool> ValidateAsync(object validatorInstance, EditContext editContext)
    {
        if (validatorInstance is FluentValidationValidator validator)
        {
            return await validator.ValidateAsync();
        }

        return true;
    }

    public bool Validate(object validatorInstance, EditContext editContext)
    {
        if (validatorInstance is FluentValidationValidator validator)
        {
            return validator.Validate();
        }

        return true;
    }

    public Type Component => typeof(FluentValidationValidator);
}
