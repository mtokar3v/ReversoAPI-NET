using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReversoAPI.Web.PronunciationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.Shared.Application.Extensions;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.PronunciationFeature.Application.Services
{
    public class PronunciationService : IPronunciationService
    {
        private const string PronunciationURL = "https://voice.reverso.net/RestPronunciation.svc/v1/output=json/GetVoiceStream/";

        private static Dictionary<Language, string> _voices = new Dictionary<Language, string>
        {
            { Language.Arabic, "Mehdi22k" },       { Language.German, "Mehdi22k" },
            { Language.Spanish, "Ines22k" },       { Language.French, "Alice22k" },
            { Language.Hebrew, "he-IL-Asaf" },     { Language.Italian, "Chiara22k" },
            { Language.Japanese, "Sakura22k" },    { Language.Dutch, "Femke22k" },
            { Language.Polish, "Ania22k" },        { Language.Portuguese, "Celia22k" },
            { Language.Romanian, "ro-RO-Andrei" }, { Language.Russian, "Alyona22k" },
            { Language.Turkish, "Ipek22k" },       { Language.Chinese, "Lulu22k" },
            { Language.English, "Heather22k" },
        };

        private readonly IAPIConnector _apiConnector;

        public PronunciationService(IAPIConnector apiConnector) => _apiConnector = apiConnector;

        public async Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            var url = CombineUrl(text, language, speed);

            var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            return response.Content;
        }

        private Uri CombineUrl(string text, Language language, int speed)
            => new Uri(PronunciationURL + $"voiceName={_voices[language]}?voiceSpeed={speed}&inputText={text.EncodeTo64()}");
    }
}
