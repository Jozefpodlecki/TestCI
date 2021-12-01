using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sanakan.Web.Tests;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestCI.DAL.Models;
using TestCI.Web.Controllers;
using TestCI.Web.Models;

namespace TestCI.Web.Tests.IntegrationTests
{
    [TestClass]
    public partial class TodoControllerTests
    {
        protected static HttpClient _client;

        public TodoControllerTests()
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
        }

        public async Task Cleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task Should_Add_Todo()
        {
            var model = new NewTodo
            {
                Name = "test todo 1",
            };

            var response = await _client.PostAsJsonAsync("api/todo", model);
            response.EnsureSuccessStatusCode();
            var todo = response.Content.ReadFromJsonAsync<Todo>();
            todo.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Should_Get_Todos()
        {
            var todos = await _client.GetFromJsonAsync<IEnumerable<Todo>>("api/todo");
            todos.Should().NotBeNull();
            todos.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Should_Remove_Todo()
        {
            var model = new NewTodo
            {
                Name = "test todo 2",
            };

            var response = await _client.PostAsJsonAsync("api/todo", model);
            var todo = response.Content.ReadFromJsonAsync<Todo>();

            response = await _client.DeleteAsync($"api/todo/{todo.Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
