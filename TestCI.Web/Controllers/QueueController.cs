using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestCI.DAL.Models;
using TestCI.DAL.Repositories;
using TestCI.Queue;
using TestCI.Queue.Messages;
using TestCI.Web.Models;

namespace TestCI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class QueueController : ControllerBase
    {
        private readonly IBlockingQueue _blockingQueue;

        public QueueController(IBlockingQueue blockingQueue)
        {
            _blockingQueue = blockingQueue;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult AddMessage([FromBody] SampleMessage sampleMessage)
        {
            var enqueued = _blockingQueue.TryAdd(sampleMessage);

            if (!enqueued)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
