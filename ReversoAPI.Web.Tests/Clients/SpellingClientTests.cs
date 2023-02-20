using FluentAssertions;
using Moq;
using ReversoAPI.Web.Clients;
using ReversoAPI.Web.DTOs.SpellingResponseData;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReversoAPI.Web.Tests.Clients
{
    public class SpellingClientTests
    { 
        private readonly Mock<IAPIConnector> _apiConnector;
        private readonly SpellingClient _client;

        public SpellingClientTests()
        {
            _apiConnector = new Mock<IAPIConnector>();
            _client = new SpellingClient(_apiConnector.Object);
        }

        [Theory]
        [MemberData(nameof(SpellingDataForTest))]
        public async Task GetAsync_Success(string text, Language language, Locale locale, Stream json, SpellingData expectedResult)
        {
            // Arrange
            var response = new HttpResponse() { Content = json };
            _apiConnector
                .Setup(c => c.PostAsync(It.IsAny<Uri>(), It.IsAny<SpellingRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _client.GetAsync(text, language, locale);

            // Assert
            expectedResult.Text.Should().BeEquivalentTo(result.Text);
            expectedResult.Language.Should().Be(result.Language);
            expectedResult.Correction.Should().NotBeNull();
            expectedResult.Correction.Should().BeEquivalentTo(result.Correction);
        }

        public static IEnumerable<object[]> SpellingDataForTest()
        {
            yield return new object[]
            {
                "maney",
                Language.English,
            Locale.None,
                new MemoryStream(Resource.spellingTestMoneyEng),
                new SpellingData
                {
                    Text = "Maney",
                    Language = Language.English,
                    Correction = new []
                    {
                        new Correction("Maney", "maney")
                        {
                            StartIndex = 0,
                            EndIndex = 4,
                            ShortDescription = "Spelling Mistake",
                            LongDescription = "A proper or common noun is not capitalized",
                            Suggestions = new[] { "Maney", "money", "many" }
                        },
                    }
                }
            };
        }
    }
}
