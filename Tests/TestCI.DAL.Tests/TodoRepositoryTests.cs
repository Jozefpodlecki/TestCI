using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TestCI.DAL;
using TestCI.DAL.Models;
using TestCI.DAL.Repositories;

namespace TestCI.Tests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        private readonly ITodoRepository _todoRepository;

        public TodoRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("database"));
            services.AddScoped<ITodoRepository, TodoRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _todoRepository = serviceProvider.GetRequiredService<ITodoRepository>();
        }

        [TestMethod]
        public async Task Should_Add_Todo()
        {
            var entity = new Todo
            {
                Name = "test todo",
                CreatedOn = DateTime.UtcNow,
            };
            await _todoRepository.CreateAsync(entity);
            var expected = await _todoRepository.GetAsync(entity.Id);
            expected.Should().Be(entity);
        }

        [TestMethod]
        public async Task Should_Return_Todos()
        {
            var entity1 = new Todo
            {
                Name = "test todo",
                CreatedOn = DateTime.UtcNow,
            };
            var entity2 = new Todo
            {
                Name = "test todo",
                CreatedOn = DateTime.UtcNow,
            };
            await _todoRepository.CreateAsync(entity1);
            await _todoRepository.CreateAsync(entity2);
            var expected = _todoRepository.Get();
            expected.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Should_Remove_Todo()
        {
            var entity = new Todo
            {
                Name = "test todo",
                CreatedOn = DateTime.UtcNow,
            };
            await _todoRepository.CreateAsync(entity);
            await _todoRepository.RemoveAsync(entity);
            var expected = await _todoRepository.GetAsync(entity.Id);
            expected.Should().BeNull();
        }
    }
}
