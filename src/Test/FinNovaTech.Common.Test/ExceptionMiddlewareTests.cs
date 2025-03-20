using FinNovaTech.Common.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text.Json;

namespace FinNovaTech.Common.Test
{
    public class ExceptionMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenExceptionThrown_ShouldReturnInternalServerError()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            var mockLogger = new Mock<ILogger<ExceptionMiddleware>>();

            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate.Setup(m => m(It.IsAny<HttpContext>()))
                .Throws(new Exception("Test exception"));

            var middleware = new ExceptionMiddleware(mockRequestDelegate.Object, mockLogger.Object);

            //Act
            await middleware.Invoke(httpContext);

            //Assert
            httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            responseBody.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(responseBody);
            var result = await reader.ReadToEndAsync();

            var expectedResponse = new Response<string>(false, "Ocurrió un error inesperado", null, (int)HttpStatusCode.InternalServerError);
            var expectedJson = JsonSerializer.Serialize(expectedResponse);

            result.Should().Be(expectedJson);
        }
    }
}