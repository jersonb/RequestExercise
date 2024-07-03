using System.Net;
using System.Net.Http.Json;
using Exercise.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace Exercise.Api.Tests
{
    public class HttpClientServiceTests
    {
        [Fact]
        public async Task Test1()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(new Response { Uuid = Guid.NewGuid(), Id = Random.Shared.Next(1, 1000) }),
                });
            using var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test/com")
            };

            var logger = new Mock<ILogger<HttpClientService>>();
            var service = new HttpClientService(httpClient, logger.Object);

            var result = await service.GetAsync(Guid.NewGuid(), It.IsAny<CancellationToken>());

            result = result;
        }
    }
}