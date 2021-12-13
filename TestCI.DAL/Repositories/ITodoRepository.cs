using System.Collections.Generic;
using System.Threading.Tasks;
using TestCI.DAL.Models;

namespace TestCI.DAL.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> Get();

        ValueTask<Todo> GetAsync(int id);

        Task CreateAsync(Todo entity);

        Task RemoveAsync(Todo entity);
    }
}
