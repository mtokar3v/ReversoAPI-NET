# ConjugationClient Class

## Defenition

Client for accessing Reverso conjugation feature

```csharp
public class ConjugationClient : IConjugationClient
```

Namespace: ReversoAPI

Inheritance: Object -> ConjugationClient

Implements: IConjugationClient  

## Remarks

This class provides access to conjugation retrieval by verb. Reverso.net has no site API to call this feature, so context retrieval by text is done by parsing the html page. The IParseService<ConjugationData> is responsible for this. 

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also united together.

```csharp
using var reverso = new ReversoClient();
var reversoConjugation = reverso.Conjugation;
```

## Methods

## GetAsync(string text, Language language, CancellationToken cancellationToken = default) Method
  
## Defenition
  
Returns context and other information about the entered text.
  
```csharp
Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
```
### Parameters

`text` string 

Text containing a verb.
  
`language` Language
  
Enum containing the source language.
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<ConjugationData>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains tenses groups with conjugations.
  
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

        using var reverso = new ReversoClient();
        var conjugationsData = await reverso.Conjugation.GetAsync(text, source);

        if (conjugationsData is null)
        {
            Console.WriteLine("Failed to get conjugations");
            return;
        }

        Console.WriteLine($"Input: {conjugationsData.Text} | Language: {conjugationsData.Language}");
        Console.WriteLine();

        foreach (var group in conjugationsData.Conjugations.Keys)
        {
            Console.WriteLine($"Group: {group}");
            if (!conjugationsData.Conjugations.TryGetValue(group, out var conjugations))
                continue;

            foreach (var selection in conjugations.Select((c, i) => new { Index = i, Conjugation = c }))
            {
                Console.WriteLine($"{selection.Index}. {selection.Conjugation.Verb}");
            }

            Console.WriteLine();
        }
    }

}
```
  

