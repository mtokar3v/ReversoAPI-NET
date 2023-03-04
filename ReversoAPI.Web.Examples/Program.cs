using ReversoAPI.Web.Clients;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Examples
{
    internal class Program
    {
        private static readonly IReversoClient _reversoClient = new ReversoClient();

        static async Task Main(string[] args)
        {
            var text = "run";
            var source = Language.English;
            var target = Language.Russian;

            var path = $"C:\\Users\\Максим\\Desktop\\{source}.mp3";

            await DownloadSpeakingAsync(text, source, path);
            await PrintConjugationAsync(text, source);
            await PrintTranslationAsync(text, source, target);
            await PrintContextAsync(text, source, target);
        }

        private static async Task DownloadSpeakingAsync(string text, Language language, string path)
        {
            var pronunciation = await _reversoClient.Pronunciation.GetAsync(text, language);

            if (pronunciation is null)
            {
                Console.WriteLine("Failed to get pronunciation");
                return;
            }

            using (var fileStream = File.Create(path))
            {
                pronunciation!.CopyTo(fileStream);
            }
        }

        private static async Task PrintConjugationAsync(string text, Language language)
        {
            var conjugation = await _reversoClient.Conjugation.GetAsync(text, language);

            if (conjugation is null)
            {
                Console.WriteLine("Failed to get conjugation");
                return;
            }

            Console.WriteLine($"Input: {conjugation.Text} | Language: {conjugation.Language}");
            Console.WriteLine();

            foreach(var group in conjugation.Conjugations.Keys)
            {
                Console.WriteLine($"Group: {group}");
                if (!conjugation.Conjugations.TryGetValue(group, out var conjugations))
                    continue;
                
                foreach (var selection in conjugations.Select((c, i) => new { Index = i, Conjugation = c }))
                {
                    Console.WriteLine($"{selection.Index}. {selection.Conjugation.Verb}");
                }

                Console.WriteLine();
            }
        }

        private static async Task PrintTranslationAsync(string text, Language source, Language target)
        {
            var translation = await _reversoClient.Translation.GetAsync(text, source, target);

            if (translation is null)
            {
                Console.WriteLine("Failed to get translation");
                return;
            }

            Console.WriteLine($"Input: {translation.Text} | Source language: {translation.Source} | Target language: {translation.Target}");
            Console.WriteLine();

            foreach (var selection in translation.Translations.Select((t, i) => new { Index = i, Translation = t }))
            {
                Console.WriteLine($"Number {selection.Index}");

                Console.WriteLine($"Language: {selection.Translation.Source}");
                Console.WriteLine(selection.Translation.Text);

                Console.WriteLine($"Language: {selection.Translation.Target}");
                Console.WriteLine(selection.Translation.Value);

                if (selection.Translation.Frequency.HasValue)
                {
                    Console.WriteLine("Extra information:");
                    Console.WriteLine($"Is colloquial: {selection.Translation.IsColloquial}");
                    Console.WriteLine($"Is rude: {selection.Translation.IsRude}");
                    Console.WriteLine($"Frequency: {selection.Translation.Frequency}");
                    Console.WriteLine($"Transliteration: {selection.Translation.Transliteration}");
                }

                Console.WriteLine();
            }
        }

        private static async Task PrintContextAsync(string text, Language source, Language target)
        {
            var context = await _reversoClient.Context.GetAsync(text, source, target);

            if(context is null)
            {
                Console.WriteLine("Failed to get context");
                return;
            }

            Console.WriteLine($"Input: {context.Text} | Source language: {context.Source} | Target language: {context.Target}");
            Console.WriteLine();
            
            foreach(var selection in context.Examples.Select((e, i) => new { Index = i, Example = e }))
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