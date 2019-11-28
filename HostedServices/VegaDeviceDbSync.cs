using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.Repositories;

namespace VegaIoTWebService.HostedServices
{
    public class VegaDeviceDbSync : IHostedService, IDisposable
    {
        private readonly ILogger<VegaDeviceDbSync> _logger;
        private readonly IVegaApiCommunicator communicator;
        private readonly ITemperatureDeviceDataRepository dataRepository;
        private readonly ITemperatureDeviceRepository deviceRepository;
        private Timer? _timer = null;

        public VegaDeviceDbSync(
            ILogger<VegaDeviceDbSync> logger,
            IVegaApiCommunicator communicator,
            ITemperatureDeviceDataRepository dataRepository,
            ITemperatureDeviceRepository deviceRepository)
        {
            _logger = logger;
            this.communicator = communicator;
            this.dataRepository = dataRepository;
            this.deviceRepository = deviceRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(UpdateDatabase, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void UpdateDatabase(object? state)
        {
            var devices = await deviceRepository.GetTempDevicesAsync();

            foreach (var item in devices)
            {
                var lastUpdateTime = await dataRepository.GetLastUpdateTime(item.Id);
                _logger.LogDebug("Last update time is lastUpdateTime");
                var vegaServerLoadedData = await communicator.GetTemperatureDeviceDatasAsync(item.Eui, lastUpdateTime);

                await dataRepository.AddTempDeviceDataAsync(vegaServerLoadedData);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}