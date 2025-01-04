using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using stshadowbackend.Middleware;
using NUnit.Framework.Legacy;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StShadowBackend.Tests
{
    public class ExceptionMiddlewareTests
    {
        private async Task InvokeMiddlewareWithException(HttpContext context)
        {
            // Simulate an exception in the middleware pipeline
            throw new InvalidOperationException("Test Exception");
        }

        [Test]
        public async Task Middleware_Returns_Expected_Response_On_Exception()
        {
            // Arrange
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(env => env.EnvironmentName).Returns(Environments.Development); // Simulate development environment

            var context = new DefaultHttpContext();
            context.RequestServices = new ServiceCollection()
                .AddSingleton(mockEnvironment.Object) // Add the mocked IWebHostEnvironment
                .BuildServiceProvider();

            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionMiddleware(next: (innerContext) =>
            {
                throw new InvalidOperationException("Test Exception");
            });

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();
            ClassicAssert.IsTrue(responseText.Contains("Test Exception"));
            ClassicAssert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }


        [Test]
        public async Task Middleware_Handles_NullReferenceException()
        {
            // Arrange
            async Task ThrowNullReferenceException(HttpContext context)
            {
                string nullObject = null;
                var length = nullObject.Length; // Throws NullReferenceException
            }

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(env => env.EnvironmentName).Returns(Environments.Development); // Simulate development environment

            var context = new DefaultHttpContext();
            context.RequestServices = new ServiceCollection()
                .AddSingleton(mockEnvironment.Object) // Add the mocked IWebHostEnvironment
                .BuildServiceProvider();

            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionMiddleware(ThrowNullReferenceException);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();

            ClassicAssert.IsTrue(responseText.Contains("Object reference not set to an instance of an object."));
            ClassicAssert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

        [Test]
        public async Task Middleware_Handles_ArgumentException()
        {
            // Arrange
            async Task ThrowArgumentException(HttpContext context)
            {
                throw new ArgumentException("Test Argument Exception");
            }

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(env => env.EnvironmentName).Returns(Environments.Development); // Simulate development environment

            var context = new DefaultHttpContext();
            context.RequestServices = new ServiceCollection()
                .AddSingleton(mockEnvironment.Object) // Add the mocked IWebHostEnvironment
                .BuildServiceProvider();

            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionMiddleware(ThrowArgumentException);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();

            ClassicAssert.IsTrue(responseText.Contains("Test Argument Exception"));
            ClassicAssert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

        [Test]
        public async Task Middleware_Handles_FileNotFoundException()
        {
            // Arrange
            async Task ThrowFileNotFoundException(HttpContext context)
            {
                throw new FileNotFoundException("Test File Not Found Exception");
            }

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(env => env.EnvironmentName).Returns(Environments.Development); // Simulate development environment

            var context = new DefaultHttpContext();
            context.RequestServices = new ServiceCollection()
                .AddSingleton(mockEnvironment.Object) // Add the mocked IWebHostEnvironment
                .BuildServiceProvider();

            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionMiddleware(ThrowFileNotFoundException);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();

            ClassicAssert.IsTrue(responseText.Contains("Test File Not Found Exception"));
            ClassicAssert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

        [Test]
        public async Task Middleware_Handles_UnauthorizedAccessException()
        {
            // Arrange
            async Task ThrowUnauthorizedAccessException(HttpContext context)
            {
                throw new UnauthorizedAccessException("Test Unauthorized Access Exception");
            }

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(env => env.EnvironmentName).Returns(Environments.Development); // Simulate development environment

            var context = new DefaultHttpContext();
            context.RequestServices = new ServiceCollection()
                .AddSingleton(mockEnvironment.Object) // Add the mocked IWebHostEnvironment
                .BuildServiceProvider();

            context.Response.Body = new MemoryStream();

            var middleware = new ExceptionMiddleware(ThrowUnauthorizedAccessException);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();

            ClassicAssert.IsTrue(responseText.Contains("Test Unauthorized Access Exception"));
            ClassicAssert.AreEqual(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

    }
}
