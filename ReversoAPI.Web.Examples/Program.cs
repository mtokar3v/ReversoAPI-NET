using ReversoAPI.Web.Clients;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Examples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var reverso = new ReversoClient();
            var ee = await reverso.Pronunciation.GetAsync("Чертила", Language.English);

            using (var fileStream = File.Create("C:\\Users\\Максим\\Desktop\\text.mp3"))
            {
                ee!.Seek(0, SeekOrigin.Begin);
                ee!.CopyTo(fileStream);
            }


            var x = await reverso.Spelling.GetAsync("Helo", Language.English);

            for (var i = 0; i < 100; i++)
                Console.WriteLine((await reverso.Synonyms.GetAsync("folk", Language.English)).Text);
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