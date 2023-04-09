# SynonymsClient Class

## Defenition

Client for accessing Reverso synonyms feature

```csharp
public class SynonymsClient : ISynonymsClient
```

Namespace: ReversoAPI

Inheritance: Object -> SynonymsClient

Implements: ISynonymsClient   

## Remarks

This class provides access to synonyms retrieval by word or phrase. Reverso.net has no site API to call this feature, so context retrieval by text is done by parsing the html page. The IParseService<SynonymsData> is responsible for this. 

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also united together.

```csharp
using var reverso = new ReversoClient();
var reversoSynonyms = reverso.Synonyms;
```

## Methods

## GetAsync(string text, Language language, CancellationToken cancellationToken = default) Method
  
## Defenition
  
Returns synonums and other information about the entered text.
  
```csharp
Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
```
### Parameters

`text` string 

Text containing a word or phrase.
  
`source` Language
  
Enum containing the source language.
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<ContextData>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains synonyms and other information.
  
### Remarks

The GetAsync method is based on parsing HTML pages from the Reverso.net website.

### Exceptions
  
`ArgumentException`
  
The input text is considered invalid if it is null or empty or if its length exceeds the allowed range limit.
  
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

        var reverso = new ReversoClient();
        var synonimsData = await reverso.Synonyms.GetAsync(text, source);

        if (synonimsData is null)
        {
            Console.WriteLine("Failed to get synonims");
            return;
        }

        Console.WriteLine($"Input: {synonimsData.Text} | Language: {synonimsData.Language}");
        Console.WriteLine();

        foreach (var selection in synonimsData.Synonyms.Select((s, i) => new { Index = i, Synonim = s }))
        {
            Console.WriteLine($"{selection.Index}. {selection.Synonim.Value} ({selection.Synonim.PartOfSpeech})");
        }

        Console.WriteLine();
    }
}
```
  
