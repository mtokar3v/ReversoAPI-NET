using FluentAssertions;

namespace ReversoAPI.Web.IntegrationTests
{
    public class TranslationTests
    {
        private static IReversoClient ReversoClient = new ReversoClient();
        private readonly static Language[] SupportedLanguages =
        {
            Language.Arabic, Language.Chinese, Language.Czech,
            Language.Danish, Language.Dutch, Language.French,
            Language.German, Language.Greek, Language.Hebrew,
            Language.Hindi, Language.Hungarian, Language.Italian,
            Language.Japanese, Language.Korean, Language.Persian,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian, Language.Slovak, Language.Spanish,
            Language.Swedish, Language.Thai, Language.Turkish,
            Language.Ukrainian, Language.English,
        };

        [Theory]
        [MemberData(nameof(GetLanguages))]
        public async Task TranslateToAllValidLanguages(Language source, Language target)
        {
            var testText = "peace";
            var translation = await ReversoClient.Translation.GetAsync(testText, source, target);

            translation.Should().NotBeNull();
            translation.Text.Should().NotBeNullOrWhiteSpace();
            translation.Source.Should().Be(source);
            translation.Target.Should().Be(target);
            translation.Translations.Should().HaveCountGreaterThan(0);
            foreach (var translationExample in translation.Translations)
            {
                translationExample.Text.Should().NotBeNullOrWhiteSpace();
                translationExample.Source.Should().Be(source);
                translationExample.Target.Should().Be(target);
            }
        }

        public static IEnumerable<object[]> GetLanguages()
        {
            return from source in SupportedLanguages.Where(x => x != Language.English)
                   select new object[] { Language.English, source };
        }
    }
}