using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace PrimitiveObsession.SolutionOne;

/// <summary>
/// This simple program aims to show an example of how primitive obsession can be solved - for strings - using strong data types and implicit casting
/// </summary>
public static class Program
{
    /// <summary>
    /// Entry point of console application
    /// </summary>
    /// <param name="args"></param>
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

/// <summary>
/// Enumeration that contains all available options for translation keys
/// </summary>
internal enum TranslationKeyOption
{
    BaseGreeting = 0,
    FarewellMessage = 1
}

/// <summary>
/// This type aims to replace strings in StringTranslator by mapping from TranslationKeyOption to a pre-defined string that can be searched in Resources
/// </summary>
internal record struct TranslationKeys
{
    public string Value { get; private set; }

    private readonly Dictionary<TranslationKeyOption, string> keyMapping = new Dictionary<TranslationKeyOption, string>
    {
        { TranslationKeyOption.BaseGreeting, "BaseGreeting" },
        { TranslationKeyOption.FarewellMessage, "FarewellMessage" }
    };

    /// <summary>
    /// Constructor that validates translation key option
    /// </summary>
    /// <param name="keyOption"></param>
    /// <exception cref="ArgumentException"></exception>
    public TranslationKeys(TranslationKeyOption keyOption)
    { 
        // Key mapping must be kept up-to-date with the available strings in Resource files
        // The advantage here is that an effort must be made to use an invalid value
        // Allowed values are easy to use. First because they are enumerated in enum TranslationKeyOptions
        // and mapping provides the existing strings in resources
        if (!keyMapping.ContainsKey(keyOption))
        {
            throw new ArgumentException($"Key option '{keyOption}' is not defined in the mapping.", nameof(keyOption));
        }
        Value = keyMapping[keyOption];
    }

    public static implicit operator string(TranslationKeys translationKey) => translationKey.Value;

    public static implicit operator TranslationKeys(TranslationKeyOption keyOption) => new TranslationKeys(keyOption);
}

/// <summary>
/// An example of a class with a simple helper function that looks for keys in Resources and gets translated messages
/// This class was originally built with dependency of strings for its function ParseString
/// And later modified to use strongly typed TranslationKeys
/// </summary>
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

    /// <summary>
    /// This function takes a translation key and provides a translated message to culture provided
    /// As translation keys can be implicitly casted to a string no effort is done to use it downstream
    /// </summary>
    /// <param name="translationKeys"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
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