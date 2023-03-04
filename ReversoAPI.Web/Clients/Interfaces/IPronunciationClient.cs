using ReversoAPI.Web.Values;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface IPronunciationClient
    {
        Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default);
    }
}