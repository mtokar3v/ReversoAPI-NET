using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.PronunciationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.PronunciationFeature.Application.Validators;

namespace ReversoAPI
{
    public class PronunciationClient : IPronunciationClient
    {
        private readonly IPronunciationService _pronunciationService;

        public PronunciationClient(IPronunciationService pronunciationService) => _pronunciationService = pronunciationService;

        public async Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default)
        {
            var validationResult = new PronunciationValidator(text, language).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _pronunciationService
                .GetAsync(text, language, speed, cancellationToken)
                .ConfigureAwait(false);

            return output;
        }
    }
}
