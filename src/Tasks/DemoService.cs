using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundService.Tasks
{
    public class DemoService : Base.BackgroundService
    {
        private readonly ILogger<DemoService> _logger;

        public DemoService(ILogger<DemoService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"task is starting.");

            stoppingToken.Register(() => _logger.LogDebug($"#1 GracePeriodManagerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"background task is doing background work.");
              
                TaskProgrammed();

                await Task.Delay(30000, stoppingToken);
            }

            _logger.LogDebug($"background task is stopping.");

            await Task.CompletedTask;
        }


        private void TaskProgrammed()
        {
            _logger.LogDebug($"task");

        }
    }
}
