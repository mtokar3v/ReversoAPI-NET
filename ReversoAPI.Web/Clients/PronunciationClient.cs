using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients
{
    public class PronunciationClient : APIClient, IPronunciationClient
    {
        private const string PronunciationURL = "https://voice.reverso.net/RestPronunciation.svc/v1/output=json/GetVoiceStream/";

        private static Dictionary<Language, string> _voices = new Dictionary<Language, string>
        {
            { Language.Arabic, "Mehdi22k" },
            { Language.German, "Mehdi22k" },
            { Language.Spanish, "Ines22k" },
            { Language.French, "Alice22k" },
            { Language.Hebrew, "he-IL-Asaf" },
            { Language.Italian, "Chiara22k" },
            { Language.Japanese, "Sakura22k" },
            { Language.Dutch, "Femke22k" },
            { Language.Polish, "Ania22k" },
            { Language.Portuguese, "Celia22k" },
            { Language.Romanian, "ro-RO-Andrei" },
            { Language.Russian, "Alyona22k" },
            { Language.Turkish, "Ipek22k" },
            { Language.Chinese, "Lulu22k" },
            { Language.English, "Heather22k" },
        };

        public PronunciationClient(IAPIConnector apiConnector) : base(apiConnector)
        {
        }

        public async Task<Stream> GetAsync(string text, Language language, int speed = 100)
        {
            if (!_voices.Keys.Contains(language)) throw new NotSupportedException($"'{language}' is not supported");
            // TO DO: add text validation (length)

            var url = CombineUrl(text, language, speed);

            var response = await _apiConnector
                .GetAsync(url)
                .ConfigureAwait(false);

            return response.Content;
        }

        private Uri CombineUrl(string text, Language language, int speed)
            => new Uri(PronunciationURL + $"voiceName={_voices[language]}?voiceSpeed={speed}&inputText={text.EncodeTo64()}");
    }
}
