using FluentValidation;

namespace TabBlazor.FluentValidation.Server.Validation;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage($"Fluent: 'Name' cannot be empty") 
            .MaximumLength(10)
            .WithMessage("Fluent: 'Name' max length is 10");
        
        RuleFor(x => x.Age)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}