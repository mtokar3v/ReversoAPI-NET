using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Pronunciation.App.Interfaces.Services
{
    internal interface IPronunciationService
    {
        Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default);
    }
}