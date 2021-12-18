using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestCI.Api;
using TestCI.DAL.Repositories;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    internal class AnotherMessageHandler : BaseMessageHandler<AnotherMessage>
    {
        private readonly ILogger<SampleMessageHandler> _logger;
        private readonly IApiWrapper _apiWrapper;
        private readonly ITodoRepository _todoRepository;
        
        public AnotherMessageHandler(
            ILogger<SampleMessageHandler> logger,
            IApiWrapper apiWrapper,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _apiWrapper = apiWrapper;
            _todoRepository = todoRepository;
        }

        public override async Task HandleAsync(AnotherMessage message)
        {
            var text = await _apiWrapper.GetAsync();

            if (string.IsNullOrEmpty(text))
            {
                throw new Exception();
            }
        }
    }
}
