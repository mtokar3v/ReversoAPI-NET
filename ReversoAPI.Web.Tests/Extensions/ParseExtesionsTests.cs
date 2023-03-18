using Xunit;
using ReversoAPI.Web.Shared.Domain.Extensions;

namespace ReversoAPI.Web.Tests.Extensions
{
    public class ParseExtesionsTests
    {
        [Theory]

        [InlineData(PartOfSpeech.Noun, "Noun")]
        [InlineData(PartOfSpeech.Noun, "noun")]
        [InlineData(PartOfSpeech.Noun, "Noun - Masculine")]

        [InlineData(PartOfSpeech.Verb, "Verb")]
        [InlineData(PartOfSpeech.Verb, "verb")]

        [InlineData(PartOfSpeech.Adjective, "Adjective")]
        [InlineData(PartOfSpeech.Adjective, "adjective")]

        [InlineData(PartOfSpeech.Adverb, "Adverb")]
        [InlineData(PartOfSpeech.Adverb, "adverb")]

        [InlineData(PartOfSpeech.Unknown, "")]
        [InlineData(PartOfSpeech.Unknown, null)]
        public void ToPartOfSpeech_Test(PartOfSpeech partOfSpeech, string input)
        {
            // Act
            var result = input.ToPartOfSpeech();

            // Assert
            Assert.Equal(partOfSpeech, result);
        }

        [Theory]

        [InlineData(Language.English, "en")]
        [InlineData(Language.Arabic, "ar")]
        [InlineData(Language.Hebrew, "he")]
        [InlineData(Language.Russian, "ru")]

        [InlineData(Language.Unknown, "")]
        [InlineData(Language.Unknown, null)]
        public void ToLanguageFromShortName_Test(Language language, string shortName)
        {
            // Act
            var result = shortName.ToLanguageFromShortName();

            // Assert
            Assert.Equal(language, result);
        }


        [Theory]

        [InlineData("en", Language.English)]
        [InlineData("ar", Language.Arabic)]
        [InlineData("he", Language.Hebrew)]
        [InlineData("ru", Language.Russian)]

        [InlineData(null, Language.Unknown)]
        public void ToShortName_Test(string shortName, Language language)
        {
            // Act
            var result = language.ToShortName();

            // Assert
            Assert.Equal(shortName, result);
        }

        [Theory]

        [InlineData("<h1> Hello, world </h1>" , "Hello, world")]
        [InlineData("<a src='https://somesite.com'> Hello, world </a>" , "Hello, world")]
        [InlineData("</br> Hello, world" , "Hello, world")]
        [InlineData("Hello, world" , "Hello, world")]
        [InlineData("" , "")]
        [InlineData(null , null)]
        public void RemoveHtmlTags_Test(string textWithHtml, string textWithoutHtml)
        {
            // Act
            var result = textWithHtml.RemoveHtmlTags();

            // Assert
            Assert.Equal(textWithoutHtml, result);
        }

        [Theory]

        [InlineData("בָּרַחְתְּ/בָּרַחַתְּ", "בָּרַחְתְּ")]
        [InlineData("בָּרַחַתְּ", "בָּרַחַתְּ")]
        public void RemoveAlternativeWord_Test(string textWithAlternative, string textWithoutAlternative)
        {
            // Act
            var result = textWithAlternative.RemoveAlternativeWord();

            // Assert
            Assert.Equal(textWithoutAlternative, result);
        }
    }
}
