using System;
using System.Globalization;
using System.Resources;

using PrimitiveObsession.Reference.Properties;

namespace PrimitiveObsession.Reference;

public static class Program
{
    static void Main(string[] args)
    {
        var message = "Hello, World!";
        var translator = new StringTranslator();

        var parsedMessage = translator.ParseString(message);
        Console.WriteLine(parsedMessage);

        // Option 1: Using the auto-generated Resources class (strongly typed)
        Console.WriteLine("Strongly typed resource:");
        Console.WriteLine(Translations.BaseGreeting);

        // Option 2: Using ResourceManager manually
        Console.WriteLine("\nUsing ResourceManager manually:");
        ResourceManager rm = new ResourceManager(
            "PrimitiveObsession.Reference.Properties.Translations", // Namespace + .resx filename (without extension)
            typeof(Program).Assembly
        );

        // Retrieve default culture string
        string defaultMessage = rm.GetString("BaseGreeting");
        Console.WriteLine(defaultMessage);

        // Retrieve specific culture string (if localized .resx exists)
        string portugueseMessage = rm.GetString("BaseGreeting", new CultureInfo("pt-BR"));
        Console.WriteLine(portugueseMessage ?? "(Portuguese (BR) translation not found)");
    }
}


internal class StringTranslator
{
    public string ParseString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        return input.Trim();
    }
}