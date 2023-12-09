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
                        new Example(new Sentence(Language.English, "Practice saying 'hello' to people you meet."), new Sentence(Language.Russian, "Люди автоматически говорят: «Привет» тем, кого встречают.")),
                        new Example(new Sentence(Language.English, "\"hello\" I said into phone."), new Sentence(Language.Russian, "«Привет», - сказал я по телефону.")),
                        new Example(new Sentence(Language.English, "Amanda seemed a little alarmed when I said hello."), new Sentence(Language.Russian, "Венди немного задержалась и показалась странной, когда она сказала привет.")),
                        new Example(new Sentence(Language.English, "Finally got a personalized hello from Earl."), new Sentence(Language.Russian, "Но наконец-то у меня есть персональное приветствие от Эрла.")),
                        new Example(new Sentence(Language.English, "They said ni hao which means hello in Mandarin."), new Sentence(Language.Russian, "«Ни хау» - так звучит приветствие на мандаринском языке.")),
                        new Example(new Sentence(Language.English, "Tell Martha... I said hello."), new Sentence(Language.Russian, "Скажи Марте... что я передаю ей привет.")),
                        new Example(new Sentence(Language.English, "Why, hello, there, Admiral."), new Sentence(Language.Russian, "А, Адмирал, привет, что здесь делаешь.")),
                        new Example(new Sentence(Language.English, "Tell her slipping' Jimmy says hello."), new Sentence(Language.Russian, "Передай ей, что Скользкий Джимми передает ей привет.")),
                        new Example(new Sentence(Language.English, "Tell him Norma Bates said hello."), new Sentence(Language.Russian, "Скажите ему, что Норма Бейтс передаёт привет.")),
                        new Example(new Sentence(Language.English, "Say hello to Brooke for me."), new Sentence(Language.Russian, "«Передай от меня привет Бродвею» (англ.).")),
                        new Example(new Sentence(Language.English, "Well, hello, hello, hello."), new Sentence(Language.Russian, "Ну, привет, привет, привет.")),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Context_Parse_Test_English_Russian))
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
                        new Example(new Sentence(Language.English, "There's only one other human heat signature on this ship."), new Sentence(Language.Arabic, "ليس هناك سوى إنسان آخر الحرارة التوقيع على هذه السفينة.")),
                        new Example(new Sentence(Language.English, "If not, there will be no human on this earth."), new Sentence(Language.Arabic, "وإن لم يكن كذلك فلن يبق إنسان على سطح الأرض.")),
                        new Example(new Sentence(Language.English, "One witness said the boy was used as a human shield."), new Sentence(Language.Arabic, "وقال عدد من الشهود إنّ الطفل تمّ استخدامه كقنبلة بشرية.")),
                        new Example(new Sentence(Language.English, "If He were not human, this would have been impossible."), new Sentence(Language.Arabic, "ولو لم تكن له طبيعة بشرية كان هذا سيكون مستحيلاً.")),
                        new Example(new Sentence(Language.English, "We have reusable rockets for the first time in human history."), new Sentence(Language.Arabic, "نملك صواريخ يمكن إعادة استخدامها لأوّل مرة في تاريخ البشرية.")),
                        new Example(new Sentence(Language.English, "For example, it sticks to cells in the human lung."), new Sentence(Language.Arabic, "على سبيل المثال، أنه يتمسك بالخلايا في الرئة البشرية.")),
                        new Example(new Sentence(Language.English, "I've only emulated human reactions, but to actually feel..."), new Sentence(Language.Arabic, "أنا فقط قمت بمحاكاة ردود الأفعال البشرية لكن لأشعر حقا...")),
                        new Example(new Sentence(Language.English, "Only in the larger cities had human waste been centrally disposed."), new Sentence(Language.Arabic, "فقط في المدن الكبرى تم التخلص من النفايات البشرية مركزيا.")),
                        new Example(new Sentence(Language.English, "But these skeletons fall within the normal range of human variation."), new Sentence(Language.Arabic, "لكن هذه الهياكل العظمية تقع ضمن نطاق الاختلافات البشرية العادية.")),
                        new Example(new Sentence(Language.English, "The greatest thing that human lips utter is the word mother."), new Sentence(Language.Arabic, "إنّ أعظم ما تتفوّه به الشّفاه البشريّة هو لفظة الأم.")),
                        new Example(new Sentence(Language.English, "It's not likely that human nature is going to change."), new Sentence(Language.Arabic, "ويرى أنه من غير المحتمل أن الطبيعة البشرية قد تغيرت.")),
                        new Example(new Sentence(Language.English, "It's so hard to understand in our limited human experience."), new Sentence(Language.Arabic, "كم من الصعب علينا أن نفهم ذلك بعقولنا البشرية المحدودة.")),
                        new Example(new Sentence(Language.English, "Every human is born with the capacity for good and evil."), new Sentence(Language.Arabic, "وأن كل إنسان يولد بقدرة كاملة على اختيار الخير والشر.")),
                        new Example(new Sentence(Language.English, "To treat every human as an end and not a means."), new Sentence(Language.Arabic, "ومن ثمة يجب أن يعامل كل إنسان كغاية لا كوسيلة.")),
                        new Example(new Sentence(Language.English, "The human evidence takes years and requires that people get sick."), new Sentence(Language.Arabic, "الأدلة البشرية يستغرق سنوات ويتطلب أن الناس الحصول على المرضى.")),
                        new Example(new Sentence(Language.English, "Indonesia is aware of its large number of human resource potential."), new Sentence(Language.Arabic, "تعي إندونيسيا إمكانياتها المتمثلة في العدد الكبير من الموارد البشرية.")),
                        new Example(new Sentence(Language.English, "But AI alone without the human touch wouldn't work either."), new Sentence(Language.Arabic, "لكن الذكاء الاصطناعي وحده لن يعمل أيضًا بدون اللمسة البشرية.")),
                        new Example(new Sentence(Language.English, "If they are sitting idle... Put them in human resources."), new Sentence(Language.Arabic, "إذا كانوا جالسين بصمت وهدوء، ضعهم في الموارد البشرية.")),
                        new Example(new Sentence(Language.English, "Both Tribunals have shared the human resources burden in this exercise."), new Sentence(Language.Arabic, "وتتقاسم المحكمتان الأعباء المتعلقة بالموارد البشرية في إطار هذه العملية.")),
                        new Example(new Sentence(Language.English, "No cases of human infection have been detected in South Korea."), new Sentence(Language.Arabic, "ولم تسجل اي حالة اصابة بشرية بالفيروس في كوريا الجنوبية.")),
                    }
                },
                new MemoryStream(Encoding.UTF8.GetBytes(Resource.Context_Parse_Test_English_Arabic))
            };
        }

        #endregion
    }
}
