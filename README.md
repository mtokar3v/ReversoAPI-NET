<img src="https://github.com/mtokar3v/ReversoAPI-NET/blob/master/ReversoAPI.Docs/Static/Images/Title.jpg">

[![License](https://img.shields.io/github/license/JohnnyCrazy/SpotifyAPI-NET?style=flat-square)](./LICENSE)
[![ReversoAPI NuGET](https://img.shields.io/nuget/vpre/ReversoAPI?label=ReversoAPI&style=flat-square)](https://www.nuget.org/packages/ReversoAPI/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ReversoAPI.svg)](https://www.nuget.org/packages/ReversoAPI/)

## What's this?

This is an open source API client for the [Reverso.net](https://www.reverso.net/), which offers a comprehensive set of language processing tools, including translation, grammar checking, verb conjugation, context finding, synonym discovery, and text-to-speech pronunciation. By leveraging the Reverso site API and parsing HTML, it provides an easy-to-use interface for .NET-based languages like C# and VisualBasic .NET. 

## Features
* ✅ Complete Reverso.net functionality:
    - `Translation`
    - `Grammar checking`
    - `Verb conjugation`
    - `Context finding`
    - `Synonym discovery`
    - `Text-to-speech pronunciation`
* ✅ Built on .NET Standard, supporting multiple platforms
* ✅ Included HTTPClient, but feel free to bring your own!
* ✅ Logging supported
* ✅ Retry Handlers supported
* ✅ Modular structure for easy unit testing and customization

## Getting Started

### Installation
To install the ReversoAPI library, you can use the NuGet package manager or download the package directly from the [NuGet website](https://www.nuget.org/packages/ReversoAPI/).

### Usage
To get started with the ReversoAPI library, you can use the following code snippet:

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using ReversoAPI;

class Program
{
    static async Task Main()
    {
        var reverso = new ReversoClient();

        var translation = await reverso.Translation.GetAsync("run", Language.English, Language.Russian);
        Console.WriteLine(translation.Translations.First().Value);
    }
}
```
    
More examples can be found in the [ReversoAPI.Web.Examples](https://github.com/mtokar3v/ReversoAPI-NET/tree/master/ReversoAPI.Web.Examples)  directory.

## Docs
Documentation for using this library can be found in the [ReversoAPI.Docs](https://github.com/mtokar3v/ReversoAPI-NET/tree/master/ReversoAPI.Docs). In this folder, you'll find a variety of resources, including API reference documentation, code examples, and tutorials to help you get started with using the library in your project. 

If you have any questions or issues with the library, please don't hesitate to open an issue on the repository or reach out to the project maintainers for assistance.


## License
This library is distributed under the MIT license. See the [LICENSE](https://github.com/mtokar3v/ReversoAPI-NET/blob/master/LICENSE) file for more information.
