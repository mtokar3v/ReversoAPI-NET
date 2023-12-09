using FluentAssertions;

namespace ReversoAPI.Web.IntegrationTests
{
    public class SynonymsTests
    {
        private static IReversoClient ReversoClient = new ReversoClient();

        [Theory]
        [MemberData(nameof(GetTestValues))]
        public async Task GetSynonymsAllValidLanguages(string text, Language source)
        {
            var synonyms = await ReversoClient.Synonyms.GetAsync(text, source);

            synonyms.Should().NotBeNull();
            synonyms.Text.Should().NotBeNullOrWhiteSpace();
            synonyms.Language.Should().Be(source);
            synonyms.Synonyms.Should().HaveCountGreaterThan(0);
            foreach (var example in synonyms.Synonyms)
            {
                example.Should().NotBeNull();
                example.Value.Should().NotBeNullOrWhiteSpace();
                example.Language.Should().Be(source);
            }
        }

        public static IEnumerable<object[]> GetTestValues()
        {
            yield return new object[] { "go", Language.English };
            yield return new object[] { "aller", Language.French };
            yield return new object[] { "encuentro", Language.Spanish };
            yield return new object[] { "gehen", Language.German };
            yield return new object[] { "andare", Language.Italian };
            yield return new object[] { "ir", Language.Portuguese };
            yield return new object[] { "пойти", Language.Russian };
            yield return new object[] { "行く", Language.Japanese };
            yield return new object[] { "ללכת", Language.Hebrew };
            yield return new object[] { "ذهب", Language.Arabic };
            yield return new object[] { "tegenkomen", Language.Dutch };
            yield return new object[] { "spotkanie", Language.Polish };
            yield return new object[] { "întâlnire", Language.Romanian };
        }
    }
}