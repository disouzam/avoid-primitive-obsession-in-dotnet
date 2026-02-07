using System;
using System.Globalization;
using System.Resources;

using PrimitiveObsession.Reference.Properties;

namespace PrimitiveObsession.Reference;

public static class Program
{
    static void Main(string[] args)
    {
        var key= "BaseGreeting";
        var translator = new StringTranslator();

        var parsedMessage = translator.ParseString(key);
        Console.WriteLine(parsedMessage);

        var parsedMessage2 = translator.ParseString(key, new CultureInfo("pt-BR"));
        Console.WriteLine(parsedMessage2);
    }
}


internal class StringTranslator
{
    private readonly ResourceManager resourceManager;

    public StringTranslator()
    {
        resourceManager = new ResourceManager(
            "PrimitiveObsession.Reference.Properties.Translations", // Namespace + .resx filename (without extension)
            typeof(Program).Assembly
        );
    }

    public string ParseString(string key, CultureInfo? cultureInfo = null)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
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