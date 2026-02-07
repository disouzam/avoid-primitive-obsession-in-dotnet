using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

using PrimitiveObsession.SolutionOne.Properties;

namespace PrimitiveObsession.SolutionOne;

public static class Program
{
    static void Main(string[] args)
    {

        try
        {
            var key = "BaseGreeting";
            var translator = new StringTranslator();

            var parsedMessage = translator.ParseString(key);
            Console.WriteLine(parsedMessage);

            var parsedMessage2 = translator.ParseString(key, new CultureInfo("pt-BR"));
            Console.WriteLine(parsedMessage2);

            var farewellMessage = translator.ParseString("FarewellMessage", new CultureInfo("pt-BR"));
            Console.WriteLine(farewellMessage);

            var incorrectKey = "WelcomeMessage";
            var parsedMessage3 = translator.ParseString(incorrectKey);
            Console.WriteLine(parsedMessage3);
        }
        catch 
        {
            Console.WriteLine("One or more errors were detected!");
        }

    }
}


internal class StringTranslator
{
    private readonly ResourceManager resourceManager;

    private readonly List<string> allowedKeys = new List<string> 
    { 
        "BaseGreeting", 
        "FarewellMessage" 
    };

    public StringTranslator()
    {
        resourceManager = new ResourceManager(
            "PrimitiveObsession.SolutionOne.Properties.Translations", // Namespace + .resx filename (without extension)
            typeof(Program).Assembly
        );
    }

    public string ParseString(string key, CultureInfo? cultureInfo = null)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        if (!allowedKeys.Contains(key))
        {
            throw new ArgumentException($"Key '{key}' is not allowed. Allowed keys are: {string.Join(", ", allowedKeys)}.", nameof(key));
        }

        if (cultureInfo != null)
        {
            var localizedMessage = resourceManager.GetString(key, cultureInfo);
            if (!string.IsNullOrEmpty(localizedMessage))
            {
                return localizedMessage;
            }else
            {
                return string.Format("Translation for key '{0}' not found in culture '{1}'.", key, cultureInfo.Name);
            }
        }
        else
        {
            var defaultMessage = resourceManager.GetString(key);
            if (!string.IsNullOrEmpty(defaultMessage))
            {
                return defaultMessage;
            }
            else
            {
                return string.Format("Translation for key '{0}' not found in culture '{1}'.", key, CultureInfo.CurrentCulture);
            }
        }
    }
}