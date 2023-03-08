using FluentAssertions;
using Moq;
using ReversoAPI.Web.Clients;
using ReversoAPI.Web.Domain.Generic.ValueObjects;
using ReversoAPI.Web.DTOs.TranslationObjects;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReversoAPI.Web.Tests.Clients
{
    public class TranslationClientTests
    {
        private readonly Mock<IAPIConnector> _apiConnector;
        private readonly TranslationClient _client;

        public TranslationClientTests()
        {
            _apiConnector = new Mock<IAPIConnector>();
            _client = new TranslationClient(_apiConnector.Object);
        }

        [Theory]
        [MemberData(nameof(SpellingDataForTest))]
        public async Task GetFromOneWordAsync_Success(string text, Language source, Language target, Stream json, TranslationData expectedResult)
        {
            // Arrange
            var response = new HttpResponse() { Content = json };
            _apiConnector
                .Setup(c => c.PostAsync(It.IsAny<Uri>(), It.IsAny<TranslationRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _client.GetAsync(text, source, target);

            // Assert
            expectedResult.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetFromOneWordAsync_Length_Exceeded()
        {
            // Arrange
            var text = new string('x', 2001);

            // Act
            var act = async () => await _client.GetAsync(text, It.IsAny<Language>(), It.IsAny<Language>());

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("The text provided exceeds the limit of 2000 symbols. (Parameter 'text')", exception.Message);
        }

        #region TestData

        public static IEnumerable<object[]> SpellingDataForTest()
        {
            yield return new object[]
            {
                "Consience",
                Language.English,
                Language.Russian,
                new MemoryStream(Resource.Translation_API_Test_English_Russian),
                new TranslationData
                {
                    Text = "Consience",
                    Source = Language.English,
                    Target = Language.Russian,
                    Translations = new[] { new Translation("Consience", "Разумность", Language.English, Language.Russian) },
                }
            };

            yield return new object[]
            {
                "peace",
                Language.English,
                Language.Russian,
                new MemoryStream(Resource.Translation_API_Test_English_Russian_2_),
                new TranslationData
                {
                    Text = "peace",
                    Source = Language.English,
                    Target = Language.Russian,
                    Translations = new[]
                    {
                        new Translation("peace", "мир", Language.English, Language.Russian, "mir", PartOfSpeech.Noun, 238642, false, false),
                        new Translation("peace", "мирный", Language.English, Language.Russian, "mirny", PartOfSpeech.Adjective, 35616, false, false),
                        new Translation("peace", "мирных", Language.English, Language.Russian, "mirnykh", PartOfSpeech.Unknown, 10123, false, false),
                        new Translation("peace", "покой", Language.English, Language.Russian, "pokoy", PartOfSpeech.Noun, 7443, false, false),
                        new Translation("peace", "спокойствие", Language.English, Language.Russian, "spokoystviye", PartOfSpeech.Noun, 4409, false, false),
                        new Translation("peace", "миротворческих", Language.English, Language.Russian, "mirotvorcheskikh", PartOfSpeech.Unknown, 1510, false, false),
                        new Translation("peace", "тишина", Language.English, Language.Russian, "tishina", PartOfSpeech.Noun, 1064, false, false),
                        new Translation("peace", "умиротворение", Language.English, Language.Russian, "umirotvoreniye", PartOfSpeech.Noun, 985, false, false),
                        new Translation("peace", "миротворческие", Language.English, Language.Russian, "mirotvorcheskiye", PartOfSpeech.Unknown, 361, false, false),
                        new Translation("peace", "миротворческой", Language.English, Language.Russian, "mirotvorcheskoy", PartOfSpeech.Adjective, 318, false, false),
                    }
                }
            };

            yield return new object[]
            {
                "hello",
                Language.English,
                Language.Hebrew,
                new MemoryStream(Resource.Translation_API_Test_English_Hebrew),
                new TranslationData
                {
                    Text = "hello",
                    Source = Language.English,
                    Target = Language.Hebrew,
                    Translations = new[]
                    {
                        new Translation("hello", "שלום", Language.English, Language.Hebrew, "shalom", PartOfSpeech.Adverb, 17186, false, false),
                        new Translation("hello", "הלו", Language.English, Language.Hebrew, "halo", PartOfSpeech.Unknown, 3968, false, false),
                        new Translation("hello", "היי", Language.English, Language.Hebrew, null, PartOfSpeech.Unknown, 754, false, false),
                        new Translation("hello", "ד\"ש", Language.English, Language.Hebrew, "da\"sh", PartOfSpeech.Unknown, 207, false, false),
                        new Translation("hello", "בוקר טוב", Language.English, Language.Hebrew, "buqar tov", PartOfSpeech.Noun, 124, false, false),
                        new Translation("hello", "הי", Language.English, Language.Hebrew, "hei", PartOfSpeech.Unknown, 89, false, false),
                        new Translation("hello", "לשלום", Language.English, Language.Hebrew, null, PartOfSpeech.Adverb, 67, false, false),
                        new Translation("hello", "ערב טוב", Language.English, Language.Hebrew, "'erev tov", PartOfSpeech.Noun, 36, false, false),
                        new Translation("hello", "אהלן", Language.English, Language.Hebrew, "ahelann", PartOfSpeech.Unknown, 35, false, false),
                        new Translation("hello", "מה שלומך", Language.English, Language.Hebrew, "mah shelomekha", PartOfSpeech.Unknown, 19, false, false),
                    }
                }
            };

            yield return new object[]
            {
                "hello world!",
                Language.English,
                Language.Korean,
                new MemoryStream(Resource.Translation_API_Test_English_Korean),
                new TranslationData
                {
                    Text = "hello world!",
                    Source = Language.English,
                    Target = Language.Korean,
                    Translations = new[] 
                    { 
                        new Translation("hello world!", "헬로우 월드!", Language.English, Language.Korean),
                    }
                }
            };

            yield return new object[]
            {
                "مرحبا العالم",
                Language.Arabic,
                Language.Hindi,
                new MemoryStream(Resource.Translation_API_Test_Arabic_Hindi),
                new TranslationData
                {
                    Text = "مرحبا العالم",
                    Source = Language.Arabic,
                    Target = Language.Hindi,
                    Translations = new[]
                    {
                        new Translation("مرحبا العالم", "नमस्ते दुनिया", Language.Arabic, Language.Hindi),
                    }
                }
            };
        }

        #endregion
    }
}
