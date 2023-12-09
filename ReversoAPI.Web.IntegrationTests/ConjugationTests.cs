using FluentAssertions;

namespace ReversoAPI.Web.IntegrationTests
{
    public class ConjugationTests
    {
        private static IReversoClient ReversoClient = new ReversoClient();

        [Theory]
        [MemberData(nameof(GetTestValues))]
        public async Task GetConjugationForAllValidLanguages(string text, Language source)
        {
            var conjugation = await ReversoClient.Conjugation.GetAsync(text, source);

            conjugation.Should().NotBeNull();
            conjugation.Text.Should().NotBeNullOrWhiteSpace();
            conjugation.Language.Should().Be(source);
            conjugation.Conjugations.Should().HaveCountGreaterThan(0);
            foreach (var conjugationTypes in conjugation.Conjugations)
            {
                conjugationTypes.Key.Should().NotBeNullOrWhiteSpace();
                foreach (var example in conjugationTypes.Value)
                {
                    example.Should().NotBeNull();
                    example.Language.Should().Be(source);
                    example.Group.Should().NotBeNullOrWhiteSpace();
                    example.Verb.Should().NotBeNullOrWhiteSpace();
                }
            }
        }

        public static IEnumerable<object[]> GetTestValues()
        {
            yield return new object[] { "go", Language.English };
            yield return new object[] { "aller", Language.French };
            yield return new object[] { "ir", Language.Spanish };
            yield return new object[] { "gehen", Language.German };
            yield return new object[] { "andare", Language.Italian };
            yield return new object[] { "ir", Language.Portuguese };
            yield return new object[] { "пойти", Language.Russian };
            yield return new object[] { "行く", Language.Japanese };
            yield return new object[] { "ללכת", Language.Hebrew };
            yield return new object[] { "ذهب", Language.Arabic };
        }
    }
}