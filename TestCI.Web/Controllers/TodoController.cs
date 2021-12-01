using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCI.DAL;
using TestCI.DAL.Models;
using TestCI.DAL.Repositories;
using TestCI.Web.Models;

namespace TestCI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodos()
        {
            var result = _todoRepository.Get().Take(5);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodo(int id)
        {
            var result = await _todoRepository.GetAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToDo([FromBody] NewTodo model)
        {
            var entity = new Todo
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
            };

            var result = _todoRepository.CreateAsync(entity);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _todoRepository.GetAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _todoRepository.RemoveAsync(entity);
            return Ok();
        }
    }
}
