using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheckFeature.Application.DTOs;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Services;
using ReversoAPI.Web.Shared.Infrastructure.Http;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;
using System.Net;

namespace ReversoAPI.Web.Tests.Clients
{
    public class SpellingClientTests
    { 
        private readonly Mock<IAPIConnector> _apiConnector;
        private readonly ISpellingService _client;

        public SpellingClientTests()
        {
            _apiConnector = new Mock<IAPIConnector>();
            _client = new SpellingService(_apiConnector.Object, null);
        }

        [Theory]
        [MemberData(nameof(SpellingDataForTest))]
        public async Task GetAsync_Success(string text, Language language, Locale locale, Stream json, SpellingData expectedResult)
        {
            // Arrange
            var response = new HttpResponse("text/html", json, HttpStatusCode.OK);
            _apiConnector
                .Setup(c => c.PostAsync(It.IsAny<Uri>(), It.IsAny<SpellingRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _client.GetAsync(text, language, locale);

            // Assert
            expectedResult.Text.Should().BeEquivalentTo(result.Text);
            expectedResult.Language.Should().Be(result.Language);
            expectedResult.Correction.Should().NotBeNull();
            expectedResult.Correction.Should().BeEquivalentTo(result.Correction);
        }

        #region TestData

        public static IEnumerable<object[]> SpellingDataForTest()
        {
            yield return new object[]
            {
                "maney",
                Language.English,
                Locale.None,
                new MemoryStream(Resource.Spelling_API_Test_English),
                new SpellingData
                {
                    Text = "Maney",
                    Language = Language.English,
                    Correction = new []
                    {
                        new Correction(
                            "Maney",
                            "maney",
                            startIndex: 0,
                            endIndex: 4,
                            "Spelling Mistake",
                            "A proper or common noun is not capitalized",
                            suggestions: new[] { "Maney", "money", "many" })
                    }
                }
            };
        }

        #endregion
    }
}
