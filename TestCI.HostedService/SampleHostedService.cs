using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCI.Common;
using TestCI.Services;

namespace TestCI.HostedService
{
    internal class SampleHostedService : BackgroundService
    {
        private readonly ISampleService _sampleService;
        private readonly ITimer _timer;
        private readonly ITaskManager _taskManager;

        public SampleHostedService(
            ISampleService sampleService,
            ITimer timer,
            ITaskManager taskManager)
        {
            _sampleService = sampleService;
            _timer = timer;
            _taskManager = taskManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer.Tick += OnTick;
            _timer.Start(TimeSpan.Zero, TimeSpan.FromHours(1));
            await _taskManager.Delay(Timeout.InfiniteTimeSpan);
        }

        private async void OnTick(object sender, TimerEventArgs e)
        {
            if (await _sampleService.ShouldExit())
            {
                return; 
            }

            throw new Exception();
        }
    }
}
