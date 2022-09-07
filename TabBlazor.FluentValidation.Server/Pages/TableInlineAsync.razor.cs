using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TabBlazor.Components.Modals;
using TabBlazor.FluentValidation.Server.Validation;
using TabBlazor.Services;

namespace TabBlazor.FluentValidation.Server.Pages;

public partial class TableInlineAsync
{
    [Inject] public IModalService Modal { get; set; }

    private IList<Document> Persons { get; set; } = new List<Document>();

    protected override void OnInitialized()
    {
        Persons = new List<Document>()
        {
            new() { Name = "Magnus", Directory = "C:/Test" }
        };
    }

    private async Task Submit(Document person)
    {
        await Modal.ShowDialogAsync(new DialogOptions()
        {
            MainText = "Form Is Valid",
            SubText = $"Person Name: {person.Name}"
        });
    }
}