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

            await PrintTranslationsAsync(text, source, target);

            await PrintContextsAsync(text, source, target);

            await PrintSynonimsAsync(text, source);

            await PrintConjugationsAsync(text, source);

            await PrintSpellingsAsync(text, source);

            await DownloadPronunciationAsync(text, source, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{source}.mp3"));
        }

        private static async Task PrintSpellingsAsync(string text, Language language)
        {
            var spellingsData = await _reversoClient.Spelling.GetAsync(text, language);

            if (spellingsData is null)
            {
                Console.WriteLine("Failed to get spellings");
                return;
            }

            Console.WriteLine($"Input: {spellingsData.Text} | Language: {spellingsData.Language}");
            Console.WriteLine();

            foreach (var selection in spellingsData.Correction.Select((s, i) => new { Index = i, Spelling = s }))
            {
                Console.WriteLine($"Number {selection.Index}");

                Console.WriteLine("Before:");
                Console.WriteLine(selection.Spelling.MistakeText);
                Console.WriteLine();
                Console.WriteLine("After:");
                Console.WriteLine(selection.Spelling.CorrectedText);
                Console.WriteLine();
                Console.WriteLine("Short description: " + selection.Spelling.ShortDescription);
                Console.WriteLine("Long description: " + selection.Spelling.LongDescription);
                Console.WriteLine("Other:");
                Console.WriteLine($"Start index: {selection.Spelling.StartIndex} End index: {selection.Spelling.EndIndex}");
                Console.WriteLine($"Suggestions: {string.Join("; ", selection.Spelling.Suggestions)}");
            }
        }

        private static async Task PrintSynonimsAsync(string text, Language language)
        {
            var synonimsData = await _reversoClient.Synonyms.GetAsync(text, language);

            if (synonimsData is null)
            {
                Console.WriteLine("Failed to get synonims");
                return;
            }

            Console.WriteLine($"Input: {synonimsData.Text} | Language: {synonimsData.Language}");
            Console.WriteLine();

            foreach (var selection in synonimsData.Synonyms.Select((s, i) => new { Index = i, Synonim = s }))
            {
                Console.WriteLine($"{selection.Index}. {selection.Synonim.Value} ({selection.Synonim.PartOfSpeech})");
            }

            Console.WriteLine();
        }

        private static async Task DownloadPronunciationAsync(string text, Language language, string path)
        {
            var pronunciationStream = await _reversoClient.Pronunciation.GetAsync(text, language);

            if (pronunciationStream is null)
            {
                Console.WriteLine("Failed to get pronunciation");
                return;
            }

            using (var fileStream = File.Create(path))
            {
                pronunciationStream!.CopyTo(fileStream);
            }
        }

        private static async Task PrintConjugationsAsync(string text, Language language)
        {
            var conjugationsData = await _reversoClient.Conjugation.GetAsync(text, language);

            if (conjugationsData is null)
            {
                Console.WriteLine("Failed to get conjugations");
                return;
            }

            Console.WriteLine($"Input: {conjugationsData.Text} | Language: {conjugationsData.Language}");
            Console.WriteLine();

            foreach(var group in conjugationsData.Conjugations.Keys)
            {
                Console.WriteLine($"Group: {group}");
                if (!conjugationsData.Conjugations.TryGetValue(group, out var conjugations))
                    continue;
                
                foreach (var selection in conjugations.Select((c, i) => new { Index = i, Conjugation = c }))
                {
                    Console.WriteLine($"{selection.Index}. {selection.Conjugation.Verb}");
                }

                Console.WriteLine();
            }
        }

        private static async Task PrintTranslationsAsync(string text, Language source, Language target)
        {
            var translationsData = await _reversoClient.Translation.GetAsync(text, source, target);

            if (translationsData is null)
            {
                Console.WriteLine("Failed to get translation");
                return;
            }

            Console.WriteLine($"Input: {translationsData.Text} | Source language: {translationsData.Source} | Target language: {translationsData.Target}");
            Console.WriteLine();

            foreach (var selection in translationsData.Translations.Select((t, i) => new { Index = i, Translation = t }))
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

        private static async Task PrintContextsAsync(string text, Language source, Language target)
        {
            var contextsData = await _reversoClient.Context.GetAsync(text, source, target);

            if(contextsData is null)
            {
                Console.WriteLine("Failed to get context");
                return;
            }

            Console.WriteLine($"Input: {contextsData.Text} | Source language: {contextsData.Source} | Target language: {contextsData.Target}");
            Console.WriteLine();
            
            foreach(var selection in contextsData.Examples.Select((e, i) => new { Index = i, Example = e }))
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