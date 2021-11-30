using HomeDevices.Net.Server.Web.Pages;
using HomeDevices.Net.Server.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Service.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly RuuviService _ruuviService;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(RuuviService ruuviService,
            ILogger<IndexModel> logger, IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _ruuviService = ruuviService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await OnGetHealthAsync();
        }
        public async Task OnPostStartAsync()
        {
            await _ruuviService.StartAsync(new CancellationToken());
            await OnGetHealthAsync();
        }

        public async Task OnPostStopAsync()
        {
            await _ruuviService.StopAsync(new CancellationToken());
            await OnGetHealthAsync();
        }
    }
}
