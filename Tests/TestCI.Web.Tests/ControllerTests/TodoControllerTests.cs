using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TestCI.DAL.Models;
using TestCI.DAL.Repositories;
using TestCI.Web.Controllers;
using TestCI.Web.Models;

namespace TestCI.Web.Tests.ControllerTests
{
    [TestClass]
    public class TodoControllerTests
    {
        private readonly TodoController _controller;
        private readonly Mock<ITodoRepository> _todoRepositoryMock = new(MockBehavior.Strict);

        public TodoControllerTests()
        {
            _controller = new TodoController(_todoRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Should_Return_Todo_List()
        {
            var todos = new[]
            {
                new Todo()
            };

            _todoRepositoryMock
                .Setup(pr => pr.Get())
                .Returns(todos);

            var result = await _controller.GetTodos();
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Should_Add_Todo()
        {
            var todo = new NewTodo
            {
                Name = "test todo",
            };

            _todoRepositoryMock
                .Setup(pr => pr.CreateAsync(It.IsAny<Todo>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddToDo(todo);
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().NotBeNull();
        }
    }
}
