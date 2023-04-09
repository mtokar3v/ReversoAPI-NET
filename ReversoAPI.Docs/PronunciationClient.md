# PronunciationClient Class

## Defenition

Client for accessing Reverso text-to-speech feature

```csharp
public class PronunciationClient : IPronunciationClient
```

Namespace: ReversoAPI

Inheritance: Object -> PronunciationClient

Implements: IPronunciationClient  

## Remarks

This class provides access to text-to-speech conversion. Based on site API. 

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also delete together.

```csharp
using var reverso = new ReversoClient();
var reversoPronunciation = reverso.Pronunciation;
```

## Methods

## GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default); Method
  
## Defenition
  
Returns a stream with a voiceover of the text.
  
```csharp
Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default)
```
### Parameters

`text` string 

Text containing a text.
  
`language` Language
  
Enum containing the source language.

`speed` Int32

Pronunciation rate
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<Stream>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains stream with a voiceover of the text.
  
### Remarks

The GetAsync method is based on site API.

### Exceptions
  
`ArgumentException`
  
The input text is considered invalid if it is null or empty or exceeds the allowed range limit of text or speed.
  
`NotSupportedException`
  
The language input is not supported by Reverso.net.
  
## Examples
  
```csharp
using ReversoAPI;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var text = "run";
        var source = Language.English;

        using var reverso = new ReversoClient();
        var pronunciationStream = await reverso.Pronunciation.GetAsync(text, source);

        if (pronunciationStream is null)
        {
            Console.WriteLine("Failed to get pronunciation");
            return;
        }

        using (var fileStream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
        {
            pronunciationStream!.CopyTo(fileStream);
        }
    }
}
```
  
