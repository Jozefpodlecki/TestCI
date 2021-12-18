using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using TestCI.Common;
using TestCI.Queue;
using TestCI.Queue.Builder;
using TestCI.Services;

namespace TestCI.HostedService
{
    internal class MessageHandlingHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IBlockingQueue _blockingQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageHandlingHostedService(
            ILogger<MessageHandlingHostedService> logger,
            IBlockingQueue blockingQueue,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _blockingQueue = blockingQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Run(async () =>
                {
                    foreach (var message in _blockingQueue.GetEnumerable(stoppingToken))
                    {
                        stoppingToken.ThrowIfCancellationRequested();

                        using var serviceScope = _serviceScopeFactory.CreateScope();
                        var serviceProvider = serviceScope.ServiceProvider;
                        var messageHandler = serviceProvider.GetMessageHandler(message);

                        await messageHandler.HandleAsync(message);

                        stoppingToken.ThrowIfCancellationRequested();
                    }
                }, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Task queue has been stopped");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing task", ex);
            }
        }
    }
}
