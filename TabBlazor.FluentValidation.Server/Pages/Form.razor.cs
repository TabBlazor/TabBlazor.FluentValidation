using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TabBlazor.Components.Modals;
using TabBlazor.FluentValidation.Server.Validation;
using TabBlazor.Services;

namespace TabBlazor.FluentValidation.Server.Pages;

public partial class Form
{
    [Inject] public IModalService Modal { get; set; }

    private Person Model { get; set; }

    protected override void OnInitialized()
    {
        Model = new Person { Name = "Magnus", Age = 36 };
    }

    private async Task Submit(EditContext context)
    {
        await Modal.ShowDialogAsync(new DialogOptions()
        {
            MainText = "Form Is Valid",
            SubText = $"Person Name: {Model.Name}"
        });
    }
}