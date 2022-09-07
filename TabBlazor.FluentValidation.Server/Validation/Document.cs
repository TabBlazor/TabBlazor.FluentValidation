using FluentValidation;

namespace TabBlazor.FluentValidation.Server.Validation;

public class Document
{
    public string Name { get; set; }
    public string Directory { get; set; }
}

public class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage($"Fluent: 'Name' cannot be empty")
            .MaximumLength(10)
            .WithMessage("Fluent: 'Name' max length is 10");

        RuleFor(x => x.Directory)
            .MustAsync(async (directory, cancellation) =>
            {
                var result = await Task.Run(() => directory == "C:/Temp");
                return result;
            })
            .WithMessage("Directory must be C:/Temp");
    }
}