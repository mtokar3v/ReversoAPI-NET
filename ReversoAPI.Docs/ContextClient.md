# ContextClient Class

## Defenition

Client for accessing Reverso context feature

```csharp
public class ContextClient : IContextClient
```

Namespace: ReversoAPI

Inheritance: Object -> ContextClient
Implements: IContextClient  

## Remarks

This class provides access to context retrieval by word, phrase, or whole sentence. Reverso.net has no site API to call this feature, so context retrieval by text is done by parsing the html page. The IParseService<ContextData> is responsible for this. 

## Instantiate a ReversoClient

The client is created together with the parent ReversoClient and is also united together.

```csharp
using var reverso = new ReversoClient();
var reversoContext = reverso.Context;
```

## Methods

## GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default) Method
  
## Defenition
  
Returns context and other information about the entered text.
  
```csharp
Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
```
### Parameters

`text` string 

Text containing a word, phrase, or small sentence.
  
`source` Language
  
Enum containing the source language.
  
`target` Language
  
Enum containing the target language.
  
`cancellationToken` CancellationToken

The token to monitor for cancellation requests. The default value is None.

### Returns
  
`Task<ContextData>`
  
The task representing the asynchronous reading operation. The value of its Result property contains the response from Reverso, which contains examples of contexts with input text in two languages and other information.
  
### Remarks

The GetAsync method is based on parsing HTML pages from the Reverso.net website.

### Exceptions
  
`ArgumentException`
  
The input text is considered invalid if it is null or empty, if its length exceeds the allowed range limit, or if the input languages are the same.
  
`NotSupportedException`
  
The language input is not supported by Reverso.net.
  
## Examples
  
```csharp
namespace ReversoAPI.Web.Examples
{
    class Program
    {
        static async Task Main()
        {
            using var reverso = new ReversoClient();

            var text = "run";
            var source = Language.English;
            var target = Language.Russian;

            var contextsData = await reverso.Context.GetAsync(text, source, target);

            if (contextsData is null)
            {
                Console.WriteLine("Failed to get context");
                return;
            }

            Console.WriteLine($"Input: {contextsData.Text} | Source language: {contextsData.Source} | Target language: {contextsData.Target}");
            Console.WriteLine();

            foreach (var selection in contextsData.Examples.Select((e, i) => new { Index = i, Example = e }))
            {
                Console.WriteLine($"Number {selection.Index}");

                Console.WriteLine($"Language: {selection.Example.Source.Language}");
                Console.WriteLine(selection.Example.Source.Text);

                Console.WriteLine($"Language: {selection.Example.Target.Language}");
                Console.WriteLine(selection.Example.Target.Text);

                Console.WriteLine();
            }
        }
    }
}
```
  

