using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCI.DAL.Models;

namespace TestCI.DAL.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Todo> Get() => _dbContext.Todos;

        public ValueTask<Todo> GetAsync(int id) => _dbContext.Todos.FindAsync(id);

        public async Task CreateAsync(Todo entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Todo entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
