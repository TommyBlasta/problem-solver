using ProblemSolver.Application.CQRS.Advent._2020.Day1;

namespace ProblemSolver.Tests.CQRS
{
    public class GetMultiplicationOfSumTargetQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectResult_WhenValidInput()
        {
            // Arrange
            var handler = new GetMultiplicationOfSumTargetQueryHandler();
            var query = new GetMultiplicationOfSumTargetQuery
            {
                NumberOfSummands = 2,
                Target = 2020,
                Input = new List<int> { 1721, 979, 366, 299, 675, 1456 }
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Numbers.Count);
            Assert.Contains(1721, result.Numbers);
            Assert.Contains(299, result.Numbers);
            Assert.Equal(514579, result.Result);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenNoCombinationFound()
        {
            // Arrange
            var handler = new GetMultiplicationOfSumTargetQueryHandler();
            var query = new GetMultiplicationOfSumTargetQuery
            {
                NumberOfSummands = 2,
                Target = 2020,
                Input = new List<int> { 1, 2, 3, 4, 5 }
            };
            var cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, cancellationToken));
        }
    }
}
