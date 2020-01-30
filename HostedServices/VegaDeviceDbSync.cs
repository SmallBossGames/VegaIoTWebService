using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VegaIoTApi.AppServices;
using VegaIoTApi.Data.Models;
using VegaIoTApi.Repositories.Interfaces;
using VegaIoTWebService.Data.Models;

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

            var deviceRepository = scope.ServiceProvider
                .GetRequiredService<IDeviceRepository>();

            var tempDataRepository = scope.ServiceProvider
                .GetRequiredService<ITemperatureDeviceDataRepository>();

            var moveDataRepository = scope.ServiceProvider
                .GetRequiredService<IMovingDeviceDataRepository>();

            var devices = await deviceRepository
                .GetDevicesAsync(_cancellationToken)
                .ConfigureAwait(false);

            foreach (var item in devices)
            {
                switch(item.DeviceType)
                {
                    case DeviceType.Temperature:
                        await UpdateTemperatureDataAsync(tempDataRepository, item).ConfigureAwait(false);
                        break;
                    case DeviceType.Move:
                        await UpdateMoveDataAsync(moveDataRepository, item).ConfigureAwait(false);
                        break;
                    default:
                        break;
                };
            }
        }

        private async Task UpdateTemperatureDataAsync(ITemperatureDeviceDataRepository repository, VegaDevice device)
        {
            var lastUpdateTime = await repository
                .GetLastUpdateTime(device.Id, _cancellationToken)
                .ConfigureAwait(false);

            var vegaServerLoadedData = await communicator
                .GetTemperatureDeviceDatasAsync(device.Eui, device.Id, lastUpdateTime, _cancellationToken)
                .ConfigureAwait(false);

            await repository
                .AddTempDeviceDataAsync(vegaServerLoadedData, _cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task UpdateMoveDataAsync(IMovingDeviceDataRepository repository, VegaDevice device)
        {
            var lastUpdateTime = await repository
                 .GetLastUpdateTime(device.Id, _cancellationToken)
                 .ConfigureAwait(false);

            var vegaServerLoadedData = await communicator
                .GetMoveDeviceDataAsync(device.Eui, device.Id, lastUpdateTime, _cancellationToken)
                .ConfigureAwait(false);

            await repository
                .AddVegaMovingDeviceDataAsync(vegaServerLoadedData, _cancellationToken)
                .ConfigureAwait(false);
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