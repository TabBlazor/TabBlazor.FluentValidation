# TabBlazor.FluentValidation
A library for using FluentValidation with [TabBlazor](https://github.com/joadan/TabBlazor)

[![Build](https://github.com/magahl/TabBlazor.FluentValidation/actions/workflows/ci.yml/badge.svg)](https://github.com/magahl/TabBlazor.FluentValidation/actions/workflows/ci.yml)

[![Nuget](https://img.shields.io/nuget/v/tabblazor.fluentvalidation.svg)](https://www.nuget.org/packages/TabBlazor.FluentValidation/)
## Installing

You can install from Nuget using the following command:
`dotnet add package TabBlazor.FluentValidation`


1. Add validation into DI like below

```csharp
services
    .AddTabBlazor()
    .AddValidation<TabBlazorFluentValidationValidator>();
```

2. Add validators:

```csharp
services
    .AddTransient<IValidator<Person>, PersonValidator>();
```

3. Profit