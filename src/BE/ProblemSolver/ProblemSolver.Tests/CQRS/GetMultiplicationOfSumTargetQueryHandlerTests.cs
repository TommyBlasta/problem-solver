using ProblemSolver.Application.CQRS.Advent._2020.Day1;

namespace ProblemSolver.Tests.CQRS
{
    public class GetMultiplicationOfSumTargetQueryHandlerTests
    {
        [Theory]
        [InlineData(2, 2020, new int[] { 1721, 979, 366, 299, 675, 1456 }, 514579)]
        [InlineData(3, 2020, new int[] { 1721, 979, 366, 299, 675, 1456 }, 241861950)]
        public async Task Handle_ReturnsCorrectResult_WhenValidInput(int numberOfSummands, int target, int[] input, int expected)
        {
            // Arrange
            var handler = new GetMultiplicationOfSumTargetQueryHandler();
            var query = new GetMultiplicationOfSumTargetQuery
            {
                NumberOfSummands = numberOfSummands,
                Target = target,
                Input = input.ToList()
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfSummands, result.Numbers.Count);
            foreach (var number in result.Numbers)
            {
                Assert.Contains(number, input);
            }
            Assert.Equal(expected, result.Result);
        }
    }
}
