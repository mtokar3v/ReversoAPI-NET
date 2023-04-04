# ReversoClientConfig Class

## Defenition

Build settings for a new ReversoClient instance

```csharp
public class ReversoClientConfig
```

Namespace: ReversoAPI

Inheritance: Object -> ReversoClientConfig

## Remarks

The ReversoClientConfig class manages the configuration settings of the ReversoClient. It includes variables and methods for managing HTTP requests, parsing API response data, and modifying configuration settings at runtime. It is designed to be flexible and customizable, and includes a default configuration method and a constructor for specifying custom settings at initialization.

## Instantiate a ReversoClientConfig

To create a ReversoClientConfig object with default settings, you can invoke the default constructor.

```csharp
var config = new ReversoClientConfig();
var reverso = new ReversoClient(config);
```

Alternatively, you can explicitly call the CreateDefault() method.

```csharp
var config = new ReversoClientConfig().CreateDefault();
var reverso = new ReversoClient(config);
```

It will create a new object with a default SimpleHttpClient, APIConnector, and several ParseService objects for handling different types of API responses.

## Setting up ReversoClient

You have the flexibility to implement your own variants of critical infrastructure objects such as IHttpClient, IAPIConnector, IParseService, and more, which can be used to configure the ReversoClient. This allows you to customize and extend the functionality of the client to suit your specific needs.

```csharp
var config = new ReversoClientConfig()
    .CreateDefault()
    .WithLogger(new YourLogger())
    .WithContextParseService(new YourContextParser);

var reverso = new ReversoClient(config);
```
The main thing is that your infrastructure must implement the appropriate interfaces
