using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.SynonymsFeature.Application.Interfaces.Services;
using ReversoAPI.Web.SynonymsFeature.Application.Validators;

namespace ReversoAPI
{
    public class SynonymsClient : ISynonymsClient
    {
        private readonly ISynonymsService _synonymsService;

        public SynonymsClient(ISynonymsService synonymsService) => _synonymsService = synonymsService;

        public async Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            var validationResult = new SynonymsRequestValidator(text, language).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _synonymsService
                .GetAsync(text, language, cancellationToken)
                .ConfigureAwait(false);

            return output;
        }
    }
}
