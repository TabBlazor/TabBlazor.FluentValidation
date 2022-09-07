using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TabBlazor.Components.Modals;
using TabBlazor.FluentValidation.Server.Validation;
using TabBlazor.Services;

namespace TabBlazor.FluentValidation.Server.Pages;

public partial class AsyncForm
{
    [Inject] public IModalService Modal { get; set; }

    private Document Model { get; set; }
    public TablerForm TheForm { get; set; }

    protected override void OnInitialized()
    {
        Model = new Document() { Name = "Magnus", Directory = "C:/Test" };
    }

    private async Task Save()
    {
        if (await TheForm.ValidateAsync())
        {
            await Modal.ShowDialogAsync(new DialogOptions()
            {
                MainText = "Form Is Valid",
                SubText = $"Person Name: {Model.Name}"
            });
        }
    }
}