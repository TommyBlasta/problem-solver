using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;
using ProblemSolver.ErrorHandling;
using System.Net;

namespace ProblemSolver.Tests.Middleware
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ShouldReturnBadRequest_ForValidationException()
        {
            // Arrange
            var middleware = new ErrorHandlingMiddleware();
            var context = new DefaultHttpContext();
            var next = new RequestDelegate((innerHttpContext) => throw new ValidationException("Validation error"));

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_ShouldReturnInternalServerError_ForGenericException()
        {
            // Arrange
            var middleware = new ErrorHandlingMiddleware();
            var context = new DefaultHttpContext();
            var next = new RequestDelegate((innerHttpContext) => throw new Exception("Generic error"));

            // Act
            await middleware.InvokeAsync(context, next);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_ShouldCallNextDelegate_Always()
        {
            // Arrange
            var middleware = new ErrorHandlingMiddleware();
            var context = new DefaultHttpContext();
            var next = new Mock<RequestDelegate>();
            next.Setup(n => n(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await middleware.InvokeAsync(context, next.Object);

            // Assert
            next.Verify(n => n(It.IsAny<HttpContext>()), Times.Once);
        }
    }
}

