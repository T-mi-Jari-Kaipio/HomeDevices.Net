using BleReaderNet.Device;
using BleReaderNet.Reader;
using BleReaderNet.Wrapper.DotNetBlueZ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Services
{
    public class RuuviProcessingService : IRuuviProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger<RuuviProcessingService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        private int _scanDurationSeconds = 5;
        private string _adapterName;
        private int _pollingInterval = 10000;
        public RuuviProcessingService(ILogger<RuuviProcessingService> logger,
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _scopeFactory = scopeFactory;

            ReadConfig();
        }

        private void ReadConfig()
        {
            _adapterName = _configuration["DeviceServer:AdapterName"].ToString();
            if (!int.TryParse(_configuration["DeviceServer:ScanDurationSeconds"], out _scanDurationSeconds))
            {
                _logger.LogWarning("DeviceServer:ScanDurationSeconds not found");
            }

            if (!int.TryParse(_configuration["DeviceServer:PollingInterval"], out _pollingInterval))
            {
                _logger.LogWarning("DeviceServer:PollingInterval not found");
            }
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation("RuuviProcessingService is working. Count: {Count}", executionCount);

                try
                {
                    List<Model.Device> devices = await GetDevices();

                    if (devices.Any())
                    {
                        foreach (var item in devices)
                        {
                            _logger.LogInformation(executionCount, "Getting data from {Name} RuuviTag", item.Name);
                            IBleReader reader = new BleReader(new DotNetBlueZService());

                            await reader.ScanAsync(_adapterName, _scanDurationSeconds);
                            string ruuviMacAddress = item.MacAddress.Replace("-", ":");
                            var ruuviTag = await reader.GetManufacturerDataAsync<RuuviTag>(ruuviMacAddress);
                            if (ruuviTag != null)
                            {
                                var dataAsJson = JsonSerializer.Serialize(ruuviTag, new JsonSerializerOptions() { WriteIndented = true });
                                _logger.LogDebug($"Data: {dataAsJson}");
                                await SaveData(ruuviTag);
                            }
                            else
                            {
                                _logger.LogDebug("No RuuviTag found");
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError("No devices in databse!");
                    }
                }
                catch (Tmds.DBus.DBusException ex)
                {
                    _logger.LogError(ex, "DBusException in DoWork!");
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in DoWork");
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }

        private async Task SaveData(RuuviTag ruuviTag)
        {
            try
            {
                Model.RuuviTag ruuviData = new()
                {
                    MacAddress = ruuviTag.MacAddress,
                    AccelerationX = ruuviTag.AccelerationX,
                    AccelerationY = ruuviTag.AccelerationY,
                    AccelerationZ = ruuviTag.AccelerationZ,
                    AirPressure = ruuviTag.AirPressure,
                    BatteryVoltage = ruuviTag.BatteryVoltage,
                    CreatedOn = DateTime.Now,
                    DataFormat = ruuviTag.DataFormat,
                    Deleted = false,
                    Humidity = ruuviTag.Humidity,
                    Id = Guid.NewGuid().ToString(),
                    MeasurementSequenceNumber = ruuviTag.MeasurementSequenceNumber,
                    MovementCounter = ruuviTag.MovementCounter,
                    Temperature = ruuviTag.Temperature,
                    TxPower = ruuviTag.TxPower,
                    UpdatedOn = DateTime.Now
                };

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    context.RuuviTags.Add(ruuviData);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveData");
                await Task.Yield();
            }
        }

        private async Task<List<Model.Device>> GetDevices()
        {
            List<Model.Device> result = new();

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                result = await context.Devices
                .OrderByDescending(o => o.CreatedOn)
                .AsNoTracking()
                .Include(l => l.Location)
                .ToListAsync();
            }

            return result;
        }
    }
}
