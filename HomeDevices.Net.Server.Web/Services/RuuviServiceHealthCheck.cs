using HomeDevices.Net.Server.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Services
{
    public class RuuviServiceHealthCheck : IHealthCheck
    {
        private readonly RuuviService _ruuviService;
        public RuuviServiceHealthCheck(RuuviService ruuviService)
        {
            _ruuviService = ruuviService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = true;

            if (healthCheckResultHealthy)
            {
                Dictionary<string, object> data = new();

                data.Add("Status", _ruuviService.Status);

                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result.", data: data));
            }

            return Task.FromResult(
                new HealthCheckResult(context.Registration.FailureStatus,
                "An unhealthy result."));
        }
    }
}
