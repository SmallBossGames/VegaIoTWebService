using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VegaIoTWebService.HostedServices
{
    public class VegaDeviceDbSync : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<VegaDeviceDbSync> _logger;
        private Timer? _timer = null;

        public VegaDeviceDbSync(ILogger<VegaDeviceDbSync> logger)
        {
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer =  _timer = new Timer(UpdateDatabase, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void UpdateDatabase(object? state)
        {
            Console.WriteLine("I'm OK");
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