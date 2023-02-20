using FluentAssertions;
using Moq;
using ReversoAPI.Web.Clients;
using ReversoAPI.Web.DTOs.TranslationObjects;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.IO;
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
                .Setup(c => c.PostAsync(It.IsAny<Uri>(), It.IsAny<TranslationRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _client.GetAsync(text, source, target);

            // Assert
            expectedResult.Should().BeEquivalentTo(result);
        }

        public static IEnumerable<object[]> SpellingDataForTest()
        {
            yield return new object[]
            {
                "Consience",
                Language.English,
                Language.Russian,
                new MemoryStream(Resource.translationTestConsienceEndRus),
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
                new MemoryStream(Resource.translationTestPeaceEngRus),
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
        }
    }
}
