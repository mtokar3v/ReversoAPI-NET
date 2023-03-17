using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.PronunciationFeature.Application.Interfaces.Services
{
    public interface IPronunciationService
    {
        Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default);
    }
}