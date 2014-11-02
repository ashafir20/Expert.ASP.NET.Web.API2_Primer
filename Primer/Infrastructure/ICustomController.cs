using System.Threading;
using System.Threading.Tasks;

namespace Primer.Infrastructure
{
    public interface ICustomController
    {
        Task<long> GetPageSizeAsync(CancellationToken cToken);
        Task<long> GetPageSize10TimesAsync(CancellationToken cToken);
        Task<long> GetPageSizeSelfContained(CancellationToken cToken);

        Task PostUrl(string newUrl, CancellationToken cToken);
    }
}
