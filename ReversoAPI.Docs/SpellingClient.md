# SpellingClient Class

## Defenition

Client for accessing Reverso grammar check feature

```csharp
public class SpellingClient : ISpellingClient
```

Namespace: ReversoAPI

Inheritance: Object -> SpellingClient

Implements: ISpellingClient  

## Remarks

This class provides access to spelling retrieval by word, phrase, or whole sentence. Based on Reverso site API. 

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also delete together.

```csharp
using var reverso = new ReversoClient();
var reversoSpelling = reverso.Spelling;
```

## Methods

## GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default) Method
  
## Defenition
  
Returns mistakes, corrections and other information about the entered text.
  
```csharp
Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
```
### Parameters

`text` string 

Text containing a text.
  
`language` Language
  
Enum containing the source language.

`locale` Locale

Enum containing language's locale
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<SpellingData>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains mistakes, corrections and other helpful information.
  
### Remarks

The GetAsync method is based on site API.

### Exceptions
  
`ArgumentException`
  
The input text is considered invalid if it is null or empty or if its length exceeds the allowed range limit.
  
`NotSupportedException`
  
The language input is not supported by Reverso.net.
  
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

        using var reverso = new ReversoClient();
        var spellingsData = await reverso.Spelling.GetAsync(text, source);

        if (spellingsData is null)
        {
            Console.WriteLine("Failed to get spellings");
            return;
        }

        Console.WriteLine($"Input: {spellingsData.Text} | Language: {spellingsData.Language}");
        Console.WriteLine();

        foreach (var selection in spellingsData.Correction.Select((s, i) => new { Index = i, Spelling = s }))
        {
            Console.WriteLine($"Number {selection.Index}");

            Console.WriteLine("Before:");
            Console.WriteLine(selection.Spelling.MistakeText);
            Console.WriteLine();
            Console.WriteLine("After:");
            Console.WriteLine(selection.Spelling.CorrectedText);
            Console.WriteLine();
            Console.WriteLine("Short description: " + selection.Spelling.ShortDescription);
            Console.WriteLine("Long description: " + selection.Spelling.LongDescription);
            Console.WriteLine("Other:");
            Console.WriteLine($"Start index: {selection.Spelling.StartIndex} End index: {selection.Spelling.EndIndex}");
            Console.WriteLine($"Suggestions: {string.Join("; ", selection.Spelling.Suggestions)}");
        }
    }
}
```
  

