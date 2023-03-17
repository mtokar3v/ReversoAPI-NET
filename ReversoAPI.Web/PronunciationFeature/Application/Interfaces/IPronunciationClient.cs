using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{ 
    public interface IPronunciationClient
    {
        Task<Stream> GetAsync(string text, Language language, int speed = 100, CancellationToken cancellationToken = default);
    }
}