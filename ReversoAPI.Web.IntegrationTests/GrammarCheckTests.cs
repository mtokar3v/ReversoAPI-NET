using FluentAssertions;

namespace ReversoAPI.Web.IntegrationTests
{
    public class GrammarCheck
    {
        private static IReversoClient ReversoClient = new ReversoClient();

        [Theory]
        [MemberData(nameof(GetTestValues))]
        public async Task MakeGrammarCheckForValidLanguages(string text, Language source)
        {
            var spelling = await ReversoClient.Spelling.GetAsync(text, source);

            spelling.Should().NotBeNull();
            spelling.Text.Should().NotBeNullOrWhiteSpace();
            spelling.Language.Should().Be(source);
            spelling.Correction.Should().HaveCountGreaterThan(0);
            foreach (var example in spelling.Correction)
            {
                example.CorrectedText.Should().NotBeNullOrWhiteSpace();
                example.MistakeText.Should().NotBeNullOrWhiteSpace();
                example.EndIndex.Should().NotBe(0);
                example.ShortDescription.Should().NotBeNullOrWhiteSpace();
                example.LongDescription.Should().NotBeNullOrWhiteSpace();
                example.Suggestions.Should().HaveCountGreaterThan(0);
            }
        }

        public static IEnumerable<object[]> GetTestValues()
        {
            yield return new object[] { "encounterrr", Language.English };
            yield return new object[] { "allerrr", Language.French };
            yield return new object[] { "encuentrooo", Language.Spanish };
            yield return new object[] { "andareeee", Language.Italian };
        }
    }
}