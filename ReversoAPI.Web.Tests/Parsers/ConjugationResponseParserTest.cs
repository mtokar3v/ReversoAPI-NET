using FluentAssertions;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Values;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class ConjugationResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(ConjugationData expectedResult, Stream html)
        {
            // Arrange
            var parser = new ConjugationResponseParser();

            // Act
            var result = parser.Invoke(html);
            html.Dispose();

            // Assert
            expectedResult.Should().BeEquivalentTo(result);
        }

        public static IEnumerable<object[]> HtmlResponsesForTest()
        {
            yield return new object[]
            {
                new ConjugationData
                {
                    Text = "run",
                    Language = Language.English,
                    Conjugations = new Dictionary<string, IEnumerable<Conjugation>>
                    {
                        { "Indicative Present", new[] 
                                                {
                                                    new Conjugation("Indicative Present", "run", Language.English),
                                                    new Conjugation("Indicative Present", "runs", Language.English),
                                                }},

                        { "Indicative Preterite", new[] { new Conjugation("Indicative Preterite", "ran", Language.English), }},
                        { "Indicative Present continuous", new[] { new Conjugation("Indicative Present continuous", "running", Language.English), }},

                        { "Indicative Present perfect", new[] {new Conjugation("Indicative Present perfect", "run", Language.English), }},
                        { "Indicative Future", new[] { new Conjugation("Indicative Future", "run", Language.English), }},
                        { "Indicative Future perfect", new[] { new Conjugation("Indicative Future perfect", "run", Language.English), }},

                        { "Indicative Past continous", new[] { new Conjugation("Indicative Past continous", "running", Language.English), }},
                        { "Indicative Past perfect", new[] { new Conjugation("Indicative Past perfect", "run", Language.English), }},
                        { "Indicative Future continuous", new[] { new Conjugation("Indicative Future continuous", "running", Language.English), }},

                        { "Indicative Present perfect continuous", new[] { new Conjugation("Indicative Present perfect continuous", "running", Language.English), }},
                        { "Indicative Past perfect continuous", new[] { new Conjugation("Indicative Past perfect continuous", "running", Language.English), }},
                        { "Indicative Future perfect continuous", new[] { new Conjugation("Indicative Future perfect continuous", "running", Language.English), }},

                        { "Imperative", new[] { new Conjugation("Imperative", "run", Language.English), }},
                        { "Participle Present", new[] { new Conjugation("Participle Present", "running", Language.English), }},
                        { "Participle Past", new[] { new Conjugation("Participle Past", "run", Language.English), }},

                        { "Infinitive", new[] { new Conjugation("Infinitive", "run", Language.English), }},
                        { "Perfect participle", new[] { new Conjugation("Perfect participle", "run", Language.English), }},
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.ConjugationParseTestRunEng)),
            };

            yield return new object[]
            {
                new ConjugationData
                {
                    Text = "бежать",
                    Language = Language.Russian,
                    Conjugations = new Dictionary<string, IEnumerable<Conjugation>>
                    {
                        { "настоящее", new[]
                                       {
                                            new Conjugation("настоящее", "бегу", Language.Russian),
                                            new Conjugation("настоящее", "бежишь", Language.Russian),
                                            new Conjugation("настоящее", "бежит", Language.Russian),
                                            new Conjugation("настоящее", "бежим", Language.Russian),
                                            new Conjugation("настоящее", "бежите", Language.Russian),
                                            new Conjugation("настоящее", "бегут", Language.Russian),
                                        }},

                        { "прошедшее", new[]
                                        {
                                            new Conjugation("прошедшее", "бежал", Language.Russian),
                                            new Conjugation("прошедшее", "бежала", Language.Russian),
                                            new Conjugation("прошедшее", "бежало", Language.Russian),
                                            new Conjugation("прошедшее", "бежали", Language.Russian),
                                        }},

                        { "будущее", new[]
                                        {
                                            new Conjugation("будущее", "бежать", Language.Russian),
                                        }},

                        { "Деепричастие настоящее", new List<Conjugation>() },

                        { "Деепричастие прошедшее", new[]
                                        {
                                            new Conjugation("Деепричастие прошедшее", "бежав/-жавши", Language.Russian),
                                        }},

                        { "Императив", new[]
                                        {
                                            new Conjugation("Императив", "беги", Language.Russian),
                                            new Conjugation("Императив", "бежим", Language.Russian),
                                            new Conjugation("Императив", "бегите", Language.Russian),
                                        }},

                        { "Причастие активный залог", new[]
                                        {
                                            new Conjugation("Причастие активный залог", "бегущий", Language.Russian),
                                            new Conjugation("Причастие активный залог", "бежавший", Language.Russian),
                                        }},

                        { "Причастие пассивный залог", new List<Conjugation>() },

                        { "Сослагательное наклонение", new[]
                                        {
                                            new Conjugation("Сослагательное наклонение", "бежал", Language.Russian),
                                            new Conjugation("Сослагательное наклонение", "бежала", Language.Russian),
                                            new Conjugation("Сослагательное наклонение", "бежало", Language.Russian),
                                            new Conjugation("Сослагательное наклонение", "бежали", Language.Russian),
                                        }},
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.ConjugationParseTestRunRus)),
            };
        }
    }
}
