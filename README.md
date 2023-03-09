<img src="https://github.com/mtokar3v/ReversoAPI-NET/blob/master/ReversoAPI.Docs/Static/Images/Title.jpg">

This open source C# library for the Reverso Web API offers a comprehensive set of language processing tools, including translation, grammar checking, verb conjugation, context finding, synonym discovery, and text-to-speech pronunciation. By leveraging the Reverso site API and parsing HTML, it provides an easy-to-use interface for .NET-based languages like C# and VisualBasic .NET. Whether you're building an educational application or need to provide language processing capabilities for your users, this library is a valuable resource.

[![License](https://img.shields.io/github/license/JohnnyCrazy/SpotifyAPI-NET?style=flat-square)](./LICENSE)
[![ReversoAPI NuGET](https://img.shields.io/nuget/vpre/ReversoAPI?label=ReversoAPI&style=flat-square)](https://www.nuget.org/packages/ReversoAPI/)
## Example

```csharp
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
    
More examples can be found in the ReversoAPI.Web.Examples directory.
