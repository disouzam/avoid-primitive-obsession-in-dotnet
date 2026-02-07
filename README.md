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
