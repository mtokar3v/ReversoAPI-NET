using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.PronunciationFeature.Application.Interfaces.Services
{
    public interface IPronunciationService
    {
        Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default);
    }
}