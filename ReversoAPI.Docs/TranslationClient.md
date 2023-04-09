# TranslationClient Class

## Defenition

Client for accessing Reverso translation feature

```csharp
public class TranslationClient : ITranslationClient
```

Namespace: ReversoAPI

Inheritance: Object -> TranslationClient

Implements: ITranslationClient  

## Remarks

This class provides access to conjugation retrieval by word, phrase, or whole sentence. Based on Reverso site API.

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also delete together.

```csharp
using var reverso = new ReversoClient();
var reversoTranslation = reverso.Translation;
```

## Methods

## GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default) Method
  
## Defenition
  
Returns translation and other information about the entered text.
  
```csharp
Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
```
### Parameters

`text` string 

Text containing a verb.
  
`source` Language
  
Enum containing the source language.

`target` Language
  
Enum containing the target language.
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<ConjugationData>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains translations and other helpful information.
  
### Remarks

The GetAsync method is based on Reverso site API.

### Exceptions
  
`ArgumentException`
  
The input text is considered invalid if it is null or empty, if its length exceeds the allowed range limit, or if the input languages are the same.
  
`NotSupportedException`
  
The language input is not supported by Reverso.net.
  
`FormatException`

Received data from Reverso is not in HTML format
  
## Examples
  
```csharp
using ReversoAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var text = "run";
        var source = Language.English;
        var target = Language.Russian;

        using var reverso = new ReversoClient();
        var translationsData = await reverso.Translation.GetAsync(text, source, target);

        if (translationsData is null)
        {
            Console.WriteLine("Failed to get translation");
            return;
        }

        Console.WriteLine($"Input: {translationsData.Text} | Source language: {translationsData.Source} | Target language: {translationsData.Target}");
        Console.WriteLine();

        foreach (var selection in translationsData.Translations.Select((t, i) => new { Index = i, Translation = t }))
        {
            Console.WriteLine($"Number {selection.Index}");

            Console.WriteLine($"Language: {selection.Translation.Source}");
            Console.WriteLine(selection.Translation.Text);

            Console.WriteLine($"Language: {selection.Translation.Target}");
            Console.WriteLine(selection.Translation.Value);

            if (selection.Translation.Frequency.HasValue)
            {
                Console.WriteLine("Extra information:");
                Console.WriteLine($"Is colloquial: {selection.Translation.IsColloquial}");
                Console.WriteLine($"Is rude: {selection.Translation.IsRude}");
                Console.WriteLine($"Frequency: {selection.Translation.Frequency}");
                Console.WriteLine($"Transliteration: {selection.Translation.Transliteration}");
            }

            Console.WriteLine();
        }
    }
}
```
  
