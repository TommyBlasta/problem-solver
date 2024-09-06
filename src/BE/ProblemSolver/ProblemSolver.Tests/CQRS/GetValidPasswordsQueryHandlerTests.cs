using ProblemSolver.Application.CQRS.Advent._2020.Day2;
using static ProblemSolver.Application.CQRS.Advent._2020.Day2.GetValidPasswordsQuery;

namespace ProblemSolver.Tests.CQRS
{
    public class GetValidPasswordsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectResult_WhenValidInputForSledRental()
        {
            // Arrange
            var handler = new GetValidPasswordsQueryHandler();
            var query = new GetValidPasswordsQuery
            {
                PasswordCheckType = PasswordCheckType.SledRental,
                Passwords = new List<PasswordDefinition>
                {
                    new PasswordDefinition()
                    {
                        LowNumber = 1,
                        HighNumber = 3,
                        Password = "abcde",
                        RequiredCharacter = 'a'
                    },
                    new PasswordDefinition()
                    {
                        LowNumber = 1,
                        HighNumber = 3,
                        Password = "cdefg",
                        RequiredCharacter = 'b'
                    },
                    new PasswordDefinition()
                    {
                        LowNumber = 2,
                        HighNumber = 9,
                        Password = "ccccccccc",
                        RequiredCharacter = 'c'
                    }
                }
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ValidPasswordsCount);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectResult_WhenValidInputForOtoas()
        {
            // Arrange
            var handler = new GetValidPasswordsQueryHandler();
            var query = new GetValidPasswordsQuery
            {
                PasswordCheckType = PasswordCheckType.Otoas,
                Passwords = new List<PasswordDefinition>
                {
                    new PasswordDefinition()
                    {
                        LowNumber = 1,
                        HighNumber = 3,
                        Password = "abcde",
                        RequiredCharacter = 'a'
                    },
                    new PasswordDefinition()
                    {
                        LowNumber = 1,
                        HighNumber = 3,
                        Password = "cdefg",
                        RequiredCharacter = 'b'
                    },
                    new PasswordDefinition()
                    {
                        LowNumber = 2,
                        HighNumber = 9,
                        Password = "ccccccccc",
                        RequiredCharacter = 'c'
                    }
                }
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ValidPasswordsCount);
        }
    }
}
