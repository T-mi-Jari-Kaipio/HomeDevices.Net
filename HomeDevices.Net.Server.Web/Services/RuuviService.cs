using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Services
{
    public class RuuviService : BackgroundService
    {
        private readonly ILogger<RuuviService> _logger;

        public RuuviService(ILogger<RuuviService> logger,
            [NotNull] IServiceProvider serviceProvider)
        {
            _logger = logger;
            Services = serviceProvider;
        }
        public IServiceProvider Services { get; }
        public string Status { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            Status = "Running";
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRuuviProcessingService>();

                await scopedProcessingService.DoWork(stoppingToken);
            }

            Status = "Stopped";
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);

            Status = "Stopped";
        }
    }
}
