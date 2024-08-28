using ProblemSolver.Application.CQRS.Euler.GetPrimeSum;
using System.Diagnostics;

namespace ProblemSolver.Tests.CQRS
{
    public class GetPrimeSumQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectResult_WhenValidInput()
        {
            // Arrange
            var handler = new GetPrimeSumQueryHandler();
            var query = new GetPrimeSumQuery
            {
                PrimeLimit = 10
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(17, result.Result); // Primes below 10 are 2, 3, 5, 7
        }

        [Fact]
        public async Task Handle_ReturnsCorrectResult_ForLargePrimeLimit()
        {
            // Arrange
            var handler = new GetPrimeSumQueryHandler();
            var query = new GetPrimeSumQuery
            {
                PrimeLimit = 2000000
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Result > 0); // Just a basic check to ensure it runs for large input
        }

        [Fact]
        public async Task Handle_ReturnsUnderTimeLimit_ForLargePrimeLimit()
        {
            // Arrange
            var timeLimitMiliseconds = 1000;
            var handler = new GetPrimeSumQueryHandler();
            var query = new GetPrimeSumQuery
            {
                PrimeLimit = 2000000
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var stopwatch = Stopwatch.StartNew();
            var result = await handler.Handle(query, cancellationToken);
            stopwatch.Stop();

            // Assert
            Assert.NotNull(result);
            Assert.True(stopwatch.ElapsedMilliseconds < timeLimitMiliseconds); // Check if execution runs for less than the limit
        }
    }
}

