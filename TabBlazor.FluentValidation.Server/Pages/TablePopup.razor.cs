using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TabBlazor.Components.Modals;
using TabBlazor.FluentValidation.Server.Validation;
using TabBlazor.Services;

namespace TabBlazor.FluentValidation.Server.Pages;

public partial class TablePopup
{
    [Inject] public IModalService Modal { get; set; }

    private IList<Person> Persons { get; set; } = new List<Person>();

    protected override void OnInitialized()
    {
        Persons = new List<Person>()
        {
            new() { Name = "Magnus", Age = 36 }
        };
    }

    private async Task Submit(Person person)
    {
        await Modal.ShowDialogAsync(new DialogOptions()
        {
            MainText = "Form Is Valid",
            SubText = $"Person Name: {person.Name}"
        });
    }
}