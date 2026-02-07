using System;

namespace PrimitiveObsession.Reference;

public static class Program
{
    static void Main(string[] args)
    {
        var message = "Hello, World!";
        var translator = new StringTranslator();

        var parsedMessage = translator.ParseString(message);
        Console.WriteLine(parsedMessage);
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