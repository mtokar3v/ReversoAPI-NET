using ReversoAPI.Web.Clients;
using ReversoAPI.Web.Models.Values;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var reverso = new ReversoClient();
            var response = reverso.Symonims.GetAsync("folk", Language.English);

            await DisplayContext(reverso);
        }

        private static async Task DisplayContext(IReversoClient reverso)
        {
            var response = await reverso.Context.GetAsync("hello", Language.English, Language.Russian);

            Console.WriteLine(response.Text);
            Console.WriteLine($"Source Language: {response.Source}");
            Console.WriteLine($"Target Language: {response.Target}");
            Console.WriteLine("___________");
            response.Translations.ToList().ForEach(t => Console.WriteLine($"Word: {t.Value}, Part of speech: {t.PartOfSpeech}, Language: {t.Language}"));
            Console.WriteLine("___________");
            response.Examples.ToList().ForEach(t =>
            {
                Console.WriteLine($"Source: {t.Source.Text}");
                Console.WriteLine($"Target: {t.Target.Text}");
                Console.WriteLine("<");
            });
        }
    }
}