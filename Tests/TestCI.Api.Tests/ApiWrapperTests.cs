using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TestCI.Api.Models;

namespace TestCI.Api.Tests
{
    [TestClass]
    public class ApiWrapperTests
    {
        private readonly ApiWrapper _apiWrapper;
        protected readonly Mock<HttpClientHandler> _httpClientHandlerMock = new(MockBehavior.Strict);
        protected HttpClient _httpClient;

        public ApiWrapperTests()
        {
            _httpClient = new HttpClient(_httpClientHandlerMock.Object);
            _httpClient.BaseAddress = new Uri("https://test.com");

            _apiWrapper = new ApiWrapper(
                _httpClient);
        }

        protected void MockHttpOk(string content, HttpMethod httpMethod)
        {
            _httpClientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(pr => pr.Method == httpMethod),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                });
        }

        [TestMethod]
        public async Task Should_Return_Message()
        {
            var expected = "message";
            MockHttpOk(expected, HttpMethod.Get);

            var text = await _apiWrapper.GetAsync();
            text.Should().Be(expected);
        }

        [TestMethod]
        public async Task Should_Return_Object()
        {
            _httpClientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(pr => pr.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(() => {
                    var stream = File.OpenRead(Path.Combine("TestData", "sample-object.json"));
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StreamContent(stream),
                    };
                });

            var expected = new SampleObject
            {
                Id = 1,
                Name = "test",
            };

            var text = await _apiWrapper.GetObjectAsync();
            text.Should().BeEquivalentTo(expected);
        }
    }
}
