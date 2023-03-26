using Xunit;
using FluentAssertions;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ReversoAPI.Web.Tests.Parsers
{
    public class ContextResponseParserTest
    {
        [Theory]
        [MemberData(nameof(HtmlResponsesForTest))]
        public void Invoke_Test(ContextData expectedResult, Stream html)
        {
            // Arrange
            var parser = new ContextParseService(null);

            // Act
            var result = parser.Invoke(html);
            html.Dispose();

            // Assert
            expectedResult.Text.Should().Be(result.Text);
            expectedResult.Source.Should().Be(result.Source);
            expectedResult.Target.Should().Be(result.Target);
            expectedResult.Examples.Should().BeEquivalentTo(result.Examples);
        }

        #region TestData

        public static IEnumerable<object[]> HtmlResponsesForTest()
        {
            yield return new object[]
            {
                new ContextData
                {
                    Text = "hello",
                    Source = Language.English,
                    Target = Language.Russian,
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
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Context_Parse_Test_English_Russian ?? string.Empty))
            };

            // There is problem with dots (they are on the right, not left)
            yield return new object[]
            {
                new ContextData
                {
                    Text = "human",
                    Source = Language.English,
                    Target = Language.Arabic,
                    Examples = new[]
                    {
                        new Example(new Sentence(Language.English, "It's human nature to want to know more about ourselves."), new Sentence(Language.Arabic, "من الطبيعي أن يريد أي إنسان معرفة المزيد عن نفسه.")),
                        new Example(new Sentence(Language.English, "They can't do the ritual without a pure human soul."), new Sentence(Language.Arabic, "هم لا يُمكنُ أَنْ يَعملونَ الطّقوسَ بدون روح أنسان صافية.")),
                        new Example(new Sentence(Language.English, "The three astronauts had begun the longest human journey ever attempted."), new Sentence(Language.Arabic, "روّاد الفضاء الثلاثة بَدأوا أطول رحلة بشريّة تجرى على الإطلاق.")),
                        new Example(new Sentence(Language.English, "It is not any language at all, human or angelic."), new Sentence(Language.Arabic, "إنها ليست لغة على الإطلاق، سواء بشرية أو ملائكية.")),
                        new Example(new Sentence(Language.English, "For example, it sticks to cells in the human lung."), new Sentence(Language.Arabic, "على سبيل المثال، أنه يتمسك بالخلايا في الرئة البشرية.")),
                        new Example(new Sentence(Language.English, "The global economic crisis touches almost all fields in human activity."), new Sentence(Language.Arabic, "وتطرقت الأزمة الاقتصادية سنغافورةة تقريبا إلى جميع مجالات الأنشطة البشرية.")),
                        new Example(new Sentence(Language.English, "Only in the larger cities had human waste been centrally disposed."), new Sentence(Language.Arabic, "فقط في المدن الكبرى تم التخلص من النفايات البشرية مركزيا.")),
                        new Example(new Sentence(Language.English, "We have reusable rockets for the first time in human history."), new Sentence(Language.Arabic, "نملك صواريخ يمكن إعادة استخدامها لأوّل مرة في تاريخ البشرية.")),
                        new Example(new Sentence(Language.English, "I've only emulated human reactions, but to actually feel..."), new Sentence(Language.Arabic, "أنا فقط قمت بمحاكاة ردود الأفعال البشرية لكن لأشعر حقا...")),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Context_Parse_Test_English_Arabic ?? string.Empty))
            };
        }

        #endregion
    }
}
