using Xunit;
using FluentAssertions;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Services;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class ConjugationResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(ConjugationData expectedResult, Stream html)
        {
            // Arrange
            var parser = new ConjugationParser();

            // Act
            var result = parser.Invoke(html);
            html.Dispose();

            // Assert
            expectedResult.Should().BeEquivalentTo(result);
        }

        #region TestData

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
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Conjugation_Parse_Test_English)),
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
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Conjugation_Parse_Test_Russian)),
            };

            yield return new object[]
{
                new ConjugationData
                {
                    Text = "cair",
                    Language = Language.Portuguese,
                    Conjugations = new Dictionary<string, IEnumerable<Conjugation>>
                    {
                        { "Indicativo Presente", new[]
                                                {
                                                    new Conjugation("Indicativo Presente", "caio", Language.Portuguese),
                                                    new Conjugation("Indicativo Presente", "cais", Language.Portuguese),
                                                    new Conjugation("Indicativo Presente", "cai", Language.Portuguese),
                                                    new Conjugation("Indicativo Presente", "caímos", Language.Portuguese),
                                                    new Conjugation("Indicativo Presente", "caís", Language.Portuguese),
                                                    new Conjugation("Indicativo Presente", "caem", Language.Portuguese),
                                                }},

                        { "Indicativo Pretérito Perfeito", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caí", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caíste", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caiu", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caímos", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caístes", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Perfeito", "caíram", Language.Portuguese),
                                                }},
                        { "Indicativo Pretérito Imperfeito", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Imperfeito", "caía", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Imperfeito", "caías", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Imperfeito", "caíamos", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Imperfeito", "caíeis", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Imperfeito", "caíam", Language.Portuguese),
                                                }},

                        { "Indicativo Pretérito Mais-que-Perfeito Composto", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito Composto", "caído", Language.Portuguese),
                                                }},


                        { "Indicativo Pretérito Mais-que-Perfeito", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito", "caíra", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito", "caíras", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito", "caíramos", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito", "caíreis", Language.Portuguese),
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito", "caíram", Language.Portuguese),
                                                }},
                        { "Indicativo Pretérito Perfeito Composto", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Perfeito Composto", "caído", Language.Portuguese),
                                                }},

                        { "Indicativo Pretérito Mais-que-Perfeito Anterior", new[]
                                                {
                                                    new Conjugation("Indicativo Pretérito Mais-que-Perfeito Anterior", "caído", Language.Portuguese),
                                                }},
                        { "Indicativo Futuro do Presente Simples", new[]
                                                {
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "cairei", Language.Portuguese),
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "cairás", Language.Portuguese),
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "cairá", Language.Portuguese),
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "cairemos", Language.Portuguese),
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "caireis", Language.Portuguese),
                                                    new Conjugation("Indicativo Futuro do Presente Simples", "cairão", Language.Portuguese),
                                                }},
                        { "Indicativo Futuro do Presente Composto", new[]
                                                {
                                                    new Conjugation("Indicativo Futuro do Presente Composto", "caído", Language.Portuguese),
                                                }},

                        { "Conjuntivo / Subjuntivo Presente", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Presente", "caia", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Presente", "caias", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Presente", "caiamos", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Presente", "caiais", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Presente", "caiam", Language.Portuguese),
                                                }},
                        { "Conjuntivo / Subjuntivo Pretérito Perfeito", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Perfeito", "caído", Language.Portuguese),
                                                }},
                        { "Conjuntivo / Subjuntivo Pretérito Imperfeito", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Imperfeito", "caísse", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Imperfeito", "caísses", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Imperfeito", "caíssemos", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Imperfeito", "caísseis", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Imperfeito", "caíssem", Language.Portuguese),
                                                }},

                        { "Conjuntivo / Subjuntivo Pretérito Mais-que-Perfeito Composto", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Pretérito Mais-que-Perfeito Composto", "caído", Language.Portuguese),
                                                }},

                        { "Conjuntivo / Subjuntivo Futuro", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro", "cair", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro", "caíres", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro", "cairmos", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro", "cairdes", Language.Portuguese),
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro", "caírem", Language.Portuguese),
                                                }},

                        { "Conjuntivo / Subjuntivo Futuro Composto", new[]
                                                {
                                                    new Conjugation("Conjuntivo / Subjuntivo Futuro Composto", "caído", Language.Portuguese),
                                                }},
                        { "Condicional Futuro do Pretérito Simples", new[]
                                                {
                                                    new Conjugation("Condicional Futuro do Pretérito Simples", "cairia", Language.Portuguese),
                                                    new Conjugation("Condicional Futuro do Pretérito Simples", "cairias", Language.Portuguese),
                                                    new Conjugation("Condicional Futuro do Pretérito Simples", "cairíamos", Language.Portuguese),
                                                    new Conjugation("Condicional Futuro do Pretérito Simples", "cairíeis", Language.Portuguese),
                                                    new Conjugation("Condicional Futuro do Pretérito Simples", "cairiam", Language.Portuguese),
                                                }},

                        { "Condicional Futuro do Pretérito Composto", new[]
                                                {
                                                    new Conjugation("Condicional Futuro do Pretérito Composto", "caído", Language.Portuguese),
                                                }},
                        { "Gerúndio", new[] { new Conjugation("Gerúndio", "caindo", Language.Portuguese), }},

                        { "Infinitivo", new[]
                                            {
                                                    new Conjugation("Infinitivo", "cair", Language.Portuguese),
                                                    new Conjugation("Infinitivo", "caíres", Language.Portuguese),
                                                    new Conjugation("Infinitivo", "cairmos", Language.Portuguese),
                                                    new Conjugation("Infinitivo", "cairdes", Language.Portuguese),
                                                    new Conjugation("Infinitivo", "caírem", Language.Portuguese),
                                            }},

                        { "Imperativo", new[]
                                            {
                                                    new Conjugation("Imperativo", "cai", Language.Portuguese),
                                                    new Conjugation("Imperativo", "caia", Language.Portuguese),
                                                    new Conjugation("Imperativo", "caiamos", Language.Portuguese),
                                                    new Conjugation("Imperativo", "caí", Language.Portuguese),
                                                    new Conjugation("Imperativo", "caiam", Language.Portuguese),
                                            }},

                        { "Imperativo Negativo", new[]
                                            {
                                                    new Conjugation("Imperativo Negativo", "caias", Language.Portuguese),
                                                    new Conjugation("Imperativo Negativo", "caia", Language.Portuguese),
                                                    new Conjugation("Imperativo Negativo", "caiamos", Language.Portuguese),
                                                    new Conjugation("Imperativo Negativo", "caiais", Language.Portuguese),
                                                    new Conjugation("Imperativo Negativo", "caiam", Language.Portuguese),
                                            }},

                        { "Particípio", new[]
                                            {
                                                    new Conjugation("Particípio", "caído", Language.Portuguese),
                                            }},
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Conjugation_Parse_Test_Portuguese)),
                };
        }

        #endregion
    }
}
