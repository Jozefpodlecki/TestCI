using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestCI.Api;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    internal class SampleMessageHandler : BaseMessageHandler<SampleMessage>
    {
        private readonly ILogger<SampleMessageHandler> _logger;
        private readonly IApiWrapper _apiWrapper;
        private readonly IServiceProvider _serviceProvider;
        
        public SampleMessageHandler(
            ILogger<SampleMessageHandler> logger,
            IApiWrapper apiWrapper,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _serviceProvider = serviceProvider;
        }

        public override async Task HandleAsync(SampleMessage message)
        {
            var text = await _apiWrapper.GetAsync();

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception();
            }
        }
    }
}
