using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Services
{
    internal interface IRuuviProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
