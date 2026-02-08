# About

This repository was created to test some ideas related with the well-known phenomena in C# / .NET world (but I assume it is present in other languages as well) known as primitive obsession. This phenomena is characterized by the repeated usage of types like `strings`, `int`, `double` / `decimal` and `bool` to represent function arguments and data flowing across the application. Quite frequently, validations are scattered along the system or duplicated in several places. But the worse scenario is when a validation is forgotten or a wrong data is passed around. In function arguments, for example, when a method has two arguments, side by side, of the same type, let say a string, you can easily switch them over and make a mistake in the usage of the function.

Although the phenomena is well-known and well characterized, I still didn't have to care much about it until today where a real situation prompted me to look for alternatives to replace old code with a better alternative. This repository, then, will hold sample code, mimicking the scenario in a real (but closed-source) software system where I try to apply ideas from articles and multimedia content about the topic.

Some references I collected today (I'm writing this on February 07th, 2026) are listed below. The list is not exhaustive and nor it is an endorsement of each approach. It is more like a guidance for ideas.

On blogs documenting alternatives:
- [A Modern Way to Create Value Objects to Solve Primitive Obsession in .NET by Anton Martyniuk](https://dev.to/antonmartyniuk/a-modern-way-to-create-value-objects-to-solve-primitive-obsession-in-net-1gkm)
- [Avoiding Primitive Obsession in .NET by Assis Zang](https://www.telerik.com/blogs/avoiding-primitive-obsession-dotnet)
- [C# - Obsessão por tipos primitivos (primitive obsession) por José Carlos Macoratti](https://www.macoratti.net/21/05/c_primobsess1.htm)
- [Superando a obsessão por tipos primitivos by Elemar Jr.](https://elemarjr.com/livros/programacao-orientada-a-objetos/superando-a-obsessao-por-tipos-primitivos-capitulo-4-v-1-1/)
- [Value objects by Chris Dunderdale](https://thatstatsguy.github.io/blog/2023/Value-Objects-and-Primitive-Obsession/)
- [Value Objects: Solving Primitive Obsession in .NET by Pawel Gerr](https://www.thinktecture.com/en/net/value-objects-solving-primitive-obsession-in-net/)

On technical aspects:
- [Create record types](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/records)
- [Operator overloading - predefined unary, arithmetic, equality, and comparison operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)


# Base project

A base console project will be created with dummy code to illustrate some problems of primitive obsession

```bash
mkdir PrimitiveObsession.Reference
cd PrimitiveObsession.Reference
# https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new
# https://learn.microsoft.com/en-us/dotnet/standard/frameworks
dotnet new console --language C# --name PrimitiveObsession.Reference --framework net9.0 --use-program-main --output . --dry-run # Check what will be done
dotnet new console --language C# --name PrimitiveObsession.Reference --framework net9.0 --use-program-main --output .
```

The idea built in the base project was to pass string as argument to search for a key in resource files. Resource files are, by nature, static and keys are fixed and known at compile time. So it would be good if some pre-validation happened when calling parsing function to obtain translation messages, in this case. One would argue that enums would be a good use case here and they are indeed useful but, to explore a broader scenario, a new implementation will be tried.

# Improved project

```bash
mkdir PrimitiveObsession.SolutionOne
cd PrimitiveObsession.SolutionOne
dotnet new console --language C# --name PrimitiveObsession.SolutionOne --framework net9.0 --use-program-main --output . --dry-run # Check what will be done
dotnet new console --language C# --name PrimitiveObsession.SolutionOne --framework net9.0 --use-program-main --output .
```

This **supposedly** improved project started as a verbatim copy of previous project, described above, and it was modified along the commits that constituted [Pull Request #2](https://github.com/disouzam/avoid-primitive-obsession-in-dotnet/pull/2) - An alternative approach to solve primitive obsession using record struct instead of strings and using enums to limit possible values.

Instead of using `strings` as a loose argument type to `ParseString` function, a new record struct type named `TranslationKeys` was created with implicit operators defined to enable seamless use with strings, without need of frequent explicit casting or conversions all the time. This type enabled that validation happened inside object creation and not inside `ParseString` function so that `ParseString` would focus only in its logic of looking for translations in resource files (`*.resx`). An additional part of current solution was the creation of an enumeration that would hold the allowed values present in resource files. Here in this example it looks like an over-engineering effort but in a large application, it is usually hard to match all these elements (resource files and their keys, strings passed as argument to functions, understand what keys are in use and what are not) without a lot of manual effort.

So an instance of `TranslationKeys` is created (using its constructor) using `TranslationKeyOptions` as its single argument and internally mapped to the string used by resource files. Depending on the schema of the keys, an enum would suffice for this mapping. But for keys that are strings in formats not allowed as enum values, a mapping using a Dictionary is required.

In a nutshell, this is the sequence of steps to make a change with regards to updates to resources (which require a search string):

1. A resource is updated with a new key and corresponding values in each language (e.g English and Portuguese-Brazil);
2. The enumeration `TranslationKeyOptions` must be updated to include this new key;
3. Then `TranslationKeys` record struct must be updated so that its private key to string mapping contains the new key and the value as string;
4. No change needs to be performed in `StringTranslator`.
    