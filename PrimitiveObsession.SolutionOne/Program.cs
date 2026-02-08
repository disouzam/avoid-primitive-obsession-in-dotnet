using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace PrimitiveObsession.SolutionOne;

public static class Program
{
    static void Main(string[] args)
    {
        try
        {
            var key1 = new TranslationKeys(TranslationKeyOption.BaseGreeting);
            var translator = new StringTranslator();

            var parsedMessage = translator.ParseString(key1);
            Console.WriteLine(parsedMessage);

            var parsedMessage2 = translator.ParseString(key1, new CultureInfo("pt-BR"));
            Console.WriteLine(parsedMessage2);
            
            var key2 = new TranslationKeys(TranslationKeyOption.FarewellMessage);
            var farewellMessage = translator.ParseString(key2, new CultureInfo("pt-BR"));
            Console.WriteLine(farewellMessage);

            var incorrectKey = new TranslationKeys((TranslationKeyOption)999);
            var parsedMessage3 = translator.ParseString(incorrectKey);
            Console.WriteLine(parsedMessage3);
        }
        catch
        {
            Console.WriteLine("One or more errors were detected!");
        }

    }
}

internal enum TranslationKeyOption
{
    BaseGreeting = 0,
    FarewellMessage = 1
}

internal record struct TranslationKeys
{
    public string Value { get; private set; }

    private readonly Dictionary<TranslationKeyOption, string> keyMapping = new Dictionary<TranslationKeyOption, string>
    {
        { TranslationKeyOption.BaseGreeting, "BaseGreeting" },
        { TranslationKeyOption.FarewellMessage, "FarewellMessage" }
    };

    public TranslationKeys(TranslationKeyOption keyOption)
    { 
        if (!keyMapping.ContainsKey(keyOption))
        {
            throw new ArgumentException($"Key option '{keyOption}' is not defined in the mapping.", nameof(keyOption));
        }
        Value = keyMapping[keyOption];
    }

    public static implicit operator string(TranslationKeys translationKey) => translationKey.Value;

    public static implicit operator TranslationKeys(TranslationKeyOption keyOption) => new TranslationKeys(keyOption);
}

internal class StringTranslator
{
    private readonly ResourceManager resourceManager;

    public StringTranslator()
    {
        resourceManager = new ResourceManager(
            "PrimitiveObsession.SolutionOne.Properties.Translations", // Namespace + .resx filename (without extension)
            typeof(Program).Assembly
        );
    }

    public string ParseString(TranslationKeys translationKeys, CultureInfo? cultureInfo = null)
    {
        if (cultureInfo != null)
        {
            var localizedMessage = resourceManager.GetString(translationKeys, cultureInfo);
            if (!string.IsNullOrEmpty(localizedMessage))
            {
                return localizedMessage;
            }
            else
            {
                return string.Format("Translation for key '{0}' not found in culture '{1}'.", translationKeys, cultureInfo.Name);
            }
        }
        else
        {
            var defaultMessage = resourceManager.GetString(translationKeys);
            if (!string.IsNullOrEmpty(defaultMessage))
            {
                return defaultMessage;
            }
            else
            {
                return string.Format("Translation for key '{0}' not found in culture '{1}'.", translationKeys, CultureInfo.CurrentCulture);
            }
        }
    }
}