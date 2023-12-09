using FluentAssertions;

namespace ReversoAPI.Web.IntegrationTests
{
    public class ContextTests
    {
        private static IReversoClient ReversoClient = new ReversoClient();
        private readonly static Language[] SupportedLanguages =
        {
            Language.Arabic, Language.German, Language.Spanish,
            Language.French, Language.Hebrew, Language.Italian,
            Language.Japanese, Language.Korean, Language.Dutch,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian, Language.Swedish, Language.Turkish,
            Language.Ukrainian, Language.Chinese, Language.English,
        };

        [Theory]
        [MemberData(nameof(GetLanguages))]
        public async Task GetContextForAllValidLanguages(Language source, Language target)
        {
            var testText = "peace";
            var context = await ReversoClient.Context.GetAsync(testText, source, target);

            context.Should().NotBeNull();
            context.Text.Should().NotBeNullOrWhiteSpace();
            context.Source.Should().Be(source);
            context.Target.Should().Be(target);
            context.Examples.Should().HaveCountGreaterThan(0);
            foreach (var example in context.Examples)
            {
                example.Source.Should().NotBeNull();
                example.Source.Text.Should().NotBeNullOrWhiteSpace();
                example.Source.Language.Should().Be(source);

                example.Target.Should().NotBeNull();
                example.Target.Text.Should().NotBeNullOrWhiteSpace();
                example.Target.Language.Should().Be(target);
            }
        }

        public static IEnumerable<object[]> GetLanguages()
        {
            return from source in SupportedLanguages.Where(x => x != Language.English)
                   select new object[] { Language.English, source };
        }
    }
}