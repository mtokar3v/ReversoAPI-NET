# ReversoClient Class

## Defenition

Common class for accessing Reverso features

```csharp
public class ReversoClient : IReversoClient
```

Namespace: ReversoAPI

Inheritance: Object -> ReversoClient
Implements: IReversoClient  

## Remarks

The class provides a wrapper for accessing Reverso features through nested clients such as IContextClient, ISynonymsClient, ISpellingClient, ITranslationClient, IPronunciationClient, IConjugationClient. The class also implements the IDisposable interface to flush out all managed resources.
The constructor builds an object with default settings, but you can configure the client yourself with ReversoClientConfig

## Instantiate a ReversoClient

To create a ReversoClientConfig object with default settings, you can invoke the default constructor.

```csharp
using var config = new ReversoClient();
```

Alternatively, you can configure ReversoClient.

```csharp
var config = new ReversoClientConfig()
  .CreateDefault()
  .WithLogger(new YourLogger());
using var reverso = new ReversoClient(config);
```
