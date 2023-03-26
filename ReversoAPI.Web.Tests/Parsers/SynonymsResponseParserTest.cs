using Xunit;
using FluentAssertions;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class SynonymsResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(SynonymsData expectedResult, Stream html)
        {
            // Arrange
            var parser = new SynonymsParseService(null);

            // Act
            var result = parser.Invoke(html);
            html.Dispose();

            // Assert
            expectedResult.Text.Should().Be(result.Text);
            expectedResult.Language.Should().Be(result.Language);
            expectedResult.Synonyms.Should().BeEquivalentTo(result.Synonyms);
        }

        #region TestData

        public static IEnumerable<object[]> HtmlResponsesForTest()
        {
            yield return new object[]
            {
                new SynonymsData
                {
                    Text = "folk",
                    Language = Language.English,
                    Synonyms = new []
                    {
                        new Synonim("people's", Language.English, PartOfSpeech.Adjective),
                        new Synonim("traditional", Language.English, PartOfSpeech.Adjective),
                        new Synonim("popular", Language.English, PartOfSpeech.Adjective),
                        new Synonim("conventional", Language.English, PartOfSpeech.Adjective),
                        new Synonim("grassroots", Language.English, PartOfSpeech.Adjective),
                        new Synonim("old-fashioned", Language.English, PartOfSpeech.Adjective),
                        new Synonim("public", Language.English, PartOfSpeech.Adjective),
                        new Synonim("formal", Language.English, PartOfSpeech.Adjective),

                        new Synonim("standard", Language.English, PartOfSpeech.Adjective),
                        new Synonim("classic", Language.English, PartOfSpeech.Adjective),
                        new Synonim("classical", Language.English, PartOfSpeech.Adjective),
                        new Synonim("famous", Language.English, PartOfSpeech.Adjective),
                        new Synonim("long-standing", Language.English, PartOfSpeech.Adjective),
                        new Synonim("customary", Language.English, PartOfSpeech.Adjective),
                        new Synonim("conservative", Language.English, PartOfSpeech.Adjective),
                        new Synonim("time-honored", Language.English, PartOfSpeech.Adjective),

                        new Synonim("popular with", Language.English, PartOfSpeech.Adjective),
                        new Synonim("pop", Language.English, PartOfSpeech.Adjective),
                        new Synonim("mainstream", Language.English, PartOfSpeech.Adjective),
                        new Synonim("typical", Language.English, PartOfSpeech.Adjective),
                        new Synonim("custom", Language.English, PartOfSpeech.Adjective),
                        new Synonim("well-known", Language.English, PartOfSpeech.Adjective),
                        new Synonim("ancient", Language.English, PartOfSpeech.Adjective),
                        new Synonim("elderly", Language.English, PartOfSpeech.Adjective),

                        new Synonim("old", Language.English, PartOfSpeech.Adjective),
                        new Synonim("auld", Language.English, PartOfSpeech.Adjective),
                        new Synonim("long-running", Language.English, PartOfSpeech.Adjective),
                        new Synonim("older", Language.English, PartOfSpeech.Adjective),
                        new Synonim("regular", Language.English, PartOfSpeech.Adjective),
                        new Synonim("normal", Language.English, PartOfSpeech.Adjective),
                        new Synonim("usual", Language.English, PartOfSpeech.Adjective),
                        new Synonim("common", Language.English, PartOfSpeech.Adjective),

                        new Synonim("people", Language.English, PartOfSpeech.Noun),
                        new Synonim("kin", Language.English, PartOfSpeech.Noun),
                        new Synonim("common people", Language.English, PartOfSpeech.Noun),
                        new Synonim("parents", Language.English, PartOfSpeech.Noun),
                        new Synonim("family", Language.English, PartOfSpeech.Noun),
                        new Synonim("kinfolk", Language.English, PartOfSpeech.Noun),
                        new Synonim("peoples", Language.English, PartOfSpeech.Noun),
                        new Synonim("individuals", Language.English, PartOfSpeech.Noun),
                        new Synonim("population", Language.English, PartOfSpeech.Noun),

                        new Synonim("persons", Language.English, PartOfSpeech.Noun),
                        new Synonim("tribe", Language.English, PartOfSpeech.Noun),
                        new Synonim("inhabitants", Language.English, PartOfSpeech.Noun),
                        new Synonim("relations", Language.English, PartOfSpeech.Noun),
                        new Synonim("crowd", Language.English, PartOfSpeech.Noun),
                        new Synonim("nation", Language.English, PartOfSpeech.Noun),
                        new Synonim("pueblo", Language.English, PartOfSpeech.Noun),
                        new Synonim("grassroots", Language.English, PartOfSpeech.Noun),
                        new Synonim("populace", Language.English, PartOfSpeech.Noun),

                        new Synonim("relative", Language.English, PartOfSpeech.Noun),
                        new Synonim("household", Language.English, PartOfSpeech.Noun),
                        new Synonim("crew", Language.English, PartOfSpeech.Noun),
                        new Synonim("kinship", Language.English, PartOfSpeech.Noun),
                        new Synonim("mob", Language.English, PartOfSpeech.Noun),
                        new Synonim("parentage", Language.English, PartOfSpeech.Noun),
                        new Synonim("kinsfolk", Language.English, PartOfSpeech.Noun),
                        new Synonim("kith", Language.English, PartOfSpeech.Noun),
                        new Synonim("father", Language.English, PartOfSpeech.Noun),

                        new Synonim("kindred", Language.English, PartOfSpeech.Noun),
                        new Synonim("race", Language.English, PartOfSpeech.Noun),
                        new Synonim("home", Language.English, PartOfSpeech.Noun),
                        new Synonim("lot", Language.English, PartOfSpeech.Noun),
                        new Synonim("country", Language.English, PartOfSpeech.Noun),
                        new Synonim("kinsman", Language.English, PartOfSpeech.Noun),
                        new Synonim("guy", Language.English, PartOfSpeech.Noun),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Symonims_Parse_Test_English ?? string.Empty))
            };

            yield return new object[]
            {
                new SynonymsData
                {
                    Text = "איש",
                    Language = Language.Hebrew,
                    Synonyms = new []
                    {
                        new Synonim("אדם", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("מישהו", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("בן אדם", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אף אחד", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("גבר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אחד", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אנשים", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("בחור", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("חבר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("מישהי", Language.Hebrew, PartOfSpeech.Noun),
                        
                        new Synonim("אחי", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("טיפוס", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("חבוב", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("ברנש", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אנוש", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("יחיד", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("פרט", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("בן", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("שוטר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אדוני", Language.Hebrew, PartOfSpeech.Noun),

                        new Synonim("נער", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("סוכן", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("זכר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("כלומניק", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("בנאדם", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("בן זוג", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("מר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אחת", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("ילד", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("המפקד", Language.Hebrew, PartOfSpeech.Noun),

                        new Synonim("אדון", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("סר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("מתווך", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("סוחר", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("סוג", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("ידיד", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("אשה", Language.Hebrew, PartOfSpeech.Noun),
                        new Synonim("נקבה", Language.Hebrew, PartOfSpeech.Noun),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Synonims_Parse_Test_Herbew ?? string.Empty))
            };

            yield return new object[]
            {
                new SynonymsData
                {
                    Text = "جمل",
                    Language = Language.Arabic,
                    Synonyms = new []
                    {
                        new Synonim("ناقة", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("إبل", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("بعير", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("كلام", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("عبارة", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("قول", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("مقولة", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("تعبير", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("كلمة", Language.Arabic, PartOfSpeech.Noun),
                        new Synonim("مصطلح", Language.Arabic, PartOfSpeech.Noun),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Synonim_Parse_Text_Arabic ?? string.Empty))
            };
        }

        #endregion
    }
}
