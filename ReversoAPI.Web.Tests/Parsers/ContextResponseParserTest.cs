using ReversoAPI.Web.Tools.Parsers;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class ContextResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(ContextData expectedResult, string html)
        {
            // Arrange
            var parser = new ContextResponseParser();

            // Act
            var result = parser.Invoke(html);

            // Assert
            expectedResult.Text.Should().Be(result.Text);
            expectedResult.Source.Should().Be(result.Source);
            expectedResult.Target.Should().Be(result.Target);
            expectedResult.Examples.Should().BeEquivalentTo(result.Examples);
            expectedResult.Translations.Should().BeEquivalentTo(result.Translations);
        }

        public static IEnumerable<object[]> HtmlResponsesForTest()
        {
            yield return new object[]
            {
                new ContextData
                {
                    Text = "hello",
                    Source = Language.English,
                    Target = Language.Russian,
                    Translations = new[]
                    {
                        new Word("привет", Language.Russian, PartOfSpeech.Noun),
                        new Word("эй", Language.Russian, PartOfSpeech.Noun),
                        new Word("приветствие", Language.Russian, PartOfSpeech.Noun),
                        new Word("приветик", Language.Russian, PartOfSpeech.Noun),
                        new Word("салют", Language.Russian, PartOfSpeech.Noun),

                        new Word("алло", Language.Russian, PartOfSpeech.Adverb),
                        new Word("приветствую", Language.Russian, PartOfSpeech.Adverb),
                        new Word("здорово", Language.Russian, PartOfSpeech.Adverb),

                        new Word("здравствуйте", Language.Russian, PartOfSpeech.Unknown),
                        new Word("здравствуй", Language.Russian, PartOfSpeech.Unknown),
                        new Word("добрый день", Language.Russian, PartOfSpeech.Unknown),
                        new Word("здрасьте", Language.Russian, PartOfSpeech.Unknown),
                        new Word("здрасте", Language.Russian, PartOfSpeech.Unknown),
                        new Word("але", Language.Russian, PartOfSpeech.Unknown),
                        new Word("слушать", Language.Russian, PartOfSpeech.Unknown),

                        new Word("Добрый вечер", Language.Russian, PartOfSpeech.Adverb),
                        new Word("Хелло", Language.Russian, PartOfSpeech.Adverb),

                        new Word("Hello", Language.Russian, PartOfSpeech.Unknown),
                    },
                    Examples = new[]
                    {
                        new Example(new Sentence(Language.English, "Hence 07734 became \"hello\"."), new Sentence(Language.Russian, "Следовательно 07734 стал hello - \"привет\".")),
                        new Example(new Sentence(Language.English, "So long physical buttons and hello on-screen controls."), new Sentence(Language.Russian, "Так что длинные физические кнопки и привет на экране управления.")),
                        new Example(new Sentence(Language.English, "It can mean both hello and goodbye."), new Sentence(Language.Russian, "Означать оно может одновременно как приветствие, так и прощание.")),
                        new Example(new Sentence(Language.English, "Start with the easy things like hello and goodbye."), new Sentence(Language.Russian, "Начните с простых стандартных фраз, например, приветствие и прощание.")),
                        new Example(new Sentence(Language.English, "Tell your boy I said hello."), new Sentence(Language.Russian, "Скажи своему пацану, что я передаю привет.")),
                        new Example(new Sentence(Language.English, "And now for a proper hello."), new Sentence(Language.Russian, "Ну а теперь, в качестве нормального приветствия.")),
                        new Example(new Sentence(Language.English, "When you said hello, you had me."), new Sentence(Language.Russian, "Как только ты сказал «привет», я уже была твоей.")),
                        new Example(new Sentence(Language.English, "Never said hello, timelines and all that."), new Sentence(Language.Russian, "Никогда не говорил, привет, о времени и всем-таком.")),
                        new Example(new Sentence(Language.English, "Instead of you know, hello."), new Sentence(Language.Russian, "Вместо того, чего тебе еще нужно, привет.")),
                    }
                },
                Resource.helloParseTest
            };
        }
    }
}
