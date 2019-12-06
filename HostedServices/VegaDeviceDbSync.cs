using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.Repositories;

namespace VegaIoTWebService.HostedServices
{
    public sealed class VegaDeviceDbSync : IHostedService, IDisposable
    {
        private readonly ILogger<VegaDeviceDbSync> _logger;
        private readonly IVegaApiCommunicator communicator;
        private readonly IServiceScopeFactory serviceScopeFactory;

        private Timer? _timer = null;
        private CancellationToken _cancellationToken = default;

        public VegaDeviceDbSync(
            ILogger<VegaDeviceDbSync> logger,
            IVegaApiCommunicator communicator,
            IServiceScopeFactory serviceScopeFactory)
        {

            _logger = logger;
            this.communicator = communicator;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _cancellationToken = cancellationToken;
            _timer = new Timer(UpdateDatabase, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private async void UpdateDatabase(object? state)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var deviceRepository = scope.ServiceProvider.GetRequiredService<ITemperatureDeviceRepository>();
            var dataRepository = scope.ServiceProvider.GetRequiredService<ITemperatureDeviceDataRepository>();

            var devices = await deviceRepository.GetTempDevicesAsync(_cancellationToken);
            Console.WriteLine($"{devices.Count}");

            foreach (var item in devices)
            {
                var lastUpdateTime = await dataRepository.GetLastUpdateTime(item.Id, _cancellationToken);

                var vegaServerLoadedData = await communicator.GetTemperatureDeviceDatasAsync
                    (item.Eui, item.Id, lastUpdateTime, _cancellationToken);

                await dataRepository.AddTempDeviceDataAsync(vegaServerLoadedData, _cancellationToken);
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