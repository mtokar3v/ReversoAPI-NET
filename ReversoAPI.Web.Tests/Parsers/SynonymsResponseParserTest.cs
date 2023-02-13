using FluentAssertions;
using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using ReversoAPI.Web.Tools.Parsers;
using System.Collections.Generic;
using Xunit;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class SynonymsResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(SynonymsResponse expectedResult, string html)
        {
            // Arrange
            var parser = new SynonymsResponseParser(html);

            // Act
            var result = parser.Invoke();

            // Assert
            expectedResult.Text.Should().Be(result.Text);
            expectedResult.Source.Should().Be(result.Source);
            expectedResult.Synonyms.Should().BeEquivalentTo(result.Synonyms);
        }

        public static IEnumerable<object[]> HtmlResponsesForTest()
        {
            yield return new object[]
            {
                new SynonymsResponse
                {
                    Text = "folk",
                    Source = Language.English,
                    Synonyms = new []
                    {
                        new Word("people's", Language.English, PartOfSpeech.Adjective),
                        new Word("traditional", Language.English, PartOfSpeech.Adjective),
                        new Word("popular", Language.English, PartOfSpeech.Adjective),
                        new Word("conventional", Language.English, PartOfSpeech.Adjective),
                        new Word("grassroots", Language.English, PartOfSpeech.Adjective),
                        new Word("old-fashioned", Language.English, PartOfSpeech.Adjective),
                        new Word("public", Language.English, PartOfSpeech.Adjective),
                        new Word("formal", Language.English, PartOfSpeech.Adjective),

                        new Word("standard", Language.English, PartOfSpeech.Adjective),
                        new Word("classic", Language.English, PartOfSpeech.Adjective),
                        new Word("classical", Language.English, PartOfSpeech.Adjective),
                        new Word("famous", Language.English, PartOfSpeech.Adjective),
                        new Word("long-standing", Language.English, PartOfSpeech.Adjective),
                        new Word("customary", Language.English, PartOfSpeech.Adjective),
                        new Word("conservative", Language.English, PartOfSpeech.Adjective),
                        new Word("time-honored", Language.English, PartOfSpeech.Adjective),

                        new Word("popular with", Language.English, PartOfSpeech.Adjective),
                        new Word("pop", Language.English, PartOfSpeech.Adjective),
                        new Word("mainstream", Language.English, PartOfSpeech.Adjective),
                        new Word("typical", Language.English, PartOfSpeech.Adjective),
                        new Word("custom", Language.English, PartOfSpeech.Adjective),
                        new Word("well-known", Language.English, PartOfSpeech.Adjective),
                        new Word("ancient", Language.English, PartOfSpeech.Adjective),
                        new Word("elderly", Language.English, PartOfSpeech.Adjective),

                        new Word("old", Language.English, PartOfSpeech.Adjective),
                        new Word("auld", Language.English, PartOfSpeech.Adjective),
                        new Word("long-running", Language.English, PartOfSpeech.Adjective),
                        new Word("older", Language.English, PartOfSpeech.Adjective),
                        new Word("regular", Language.English, PartOfSpeech.Adjective),
                        new Word("normal", Language.English, PartOfSpeech.Adjective),
                        new Word("usual", Language.English, PartOfSpeech.Adjective),
                        new Word("common", Language.English, PartOfSpeech.Adjective),

                        new Word("people", Language.English, PartOfSpeech.Noun),
                        new Word("kin", Language.English, PartOfSpeech.Noun),
                        new Word("common people", Language.English, PartOfSpeech.Noun),
                        new Word("parents", Language.English, PartOfSpeech.Noun),
                        new Word("family", Language.English, PartOfSpeech.Noun),
                        new Word("kinfolk", Language.English, PartOfSpeech.Noun),
                        new Word("peoples", Language.English, PartOfSpeech.Noun),
                        new Word("individuals", Language.English, PartOfSpeech.Noun),
                        new Word("population", Language.English, PartOfSpeech.Noun),

                        new Word("persons", Language.English, PartOfSpeech.Noun),
                        new Word("tribe", Language.English, PartOfSpeech.Noun),
                        new Word("inhabitants", Language.English, PartOfSpeech.Noun),
                        new Word("relations", Language.English, PartOfSpeech.Noun),
                        new Word("crowd", Language.English, PartOfSpeech.Noun),
                        new Word("nation", Language.English, PartOfSpeech.Noun),
                        new Word("pueblo", Language.English, PartOfSpeech.Noun),
                        new Word("grassroots", Language.English, PartOfSpeech.Noun),
                        new Word("populace", Language.English, PartOfSpeech.Noun),

                        new Word("relative", Language.English, PartOfSpeech.Noun),
                        new Word("household", Language.English, PartOfSpeech.Noun),
                        new Word("crew", Language.English, PartOfSpeech.Noun),
                        new Word("kinship", Language.English, PartOfSpeech.Noun),
                        new Word("mob", Language.English, PartOfSpeech.Noun),
                        new Word("parentage", Language.English, PartOfSpeech.Noun),
                        new Word("kinsfolk", Language.English, PartOfSpeech.Noun),
                        new Word("kith", Language.English, PartOfSpeech.Noun),
                        new Word("father", Language.English, PartOfSpeech.Noun),

                        new Word("kindred", Language.English, PartOfSpeech.Noun),
                        new Word("race", Language.English, PartOfSpeech.Noun),
                        new Word("home", Language.English, PartOfSpeech.Noun),
                        new Word("lot", Language.English, PartOfSpeech.Noun),
                        new Word("country", Language.English, PartOfSpeech.Noun),
                        new Word("kinsman", Language.English, PartOfSpeech.Noun),
                        new Word("guy", Language.English, PartOfSpeech.Noun),
                    }
                },
                Resource.folkSymonimsParseTest
            };
        }
    }
}
