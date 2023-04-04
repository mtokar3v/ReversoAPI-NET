# SimpleConsoleLogger Class

## Defenition

Logs errors and other information.

```csharp
public class SimpleConsoleLogger : ILogger
```

Namespace: ReversoAPI

Inheritance: Object -> SimpleConsoleLogger

Implements: ILogger

## Remarks

The Simple Logger is a logging tool that provides a standard list of methods, including Debug, Info, Warning, Error, and Fatal. It displays log information in a format that includes the date, log type, and message for easy readability.

## Setting up SimpleConsoleLogger

You can use ReversoClientConfig to set SimpleConsoleLogger.

```csharp
var config = new ReversoClientConfig()
    .CreateDefault()
    .WithLogger(new SimpleConsoleLogger());

var reverso = new ReversoClient(config);
```

You can also set your own logger, if it implements the ILogger interface.

```csharp
var config = new ReversoClientConfig()
    .CreateDefault()
    .WithLogger(new YourLogger());

var reverso = new ReversoClient(config);
```

## Log example
```
Friday, October 31, 2023 9:04:32 AM
ERROR: Reverso translation response is empty.
Failed to get translation
```
