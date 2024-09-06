using FluentValidation;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProblemSolver.Application.Validation;
using System.Reflection;

namespace ProblemSolver.Tests.Validation
{
    public class ValidatorRegistrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        // Assuming _serviceProvider is injected via constructor or initialized with a test fixture
        private readonly WebApplicationFactory<Program> _factory;

        public ValidatorRegistrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void All_ValidableQueries_Should_Have_Validators_Registered()
        {
            // Arrange
            var validableQueryTypes = Assembly.GetAssembly(typeof(IValidableQuery))!
                                              .GetTypes()
                                              .Where(t => typeof(IValidableQuery).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                              .ToList();

            var serviceCollection = _factory.Services;

            foreach (var queryType in validableQueryTypes)
            {
                var validatorType = typeof(IValidator<>).MakeGenericType(queryType);
                using var scope = serviceCollection.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                // Act
                var validator = serviceProvider.GetService(validatorType);

                // Assert
                Assert.NotNull(validator);
            }
        }
    }
}
