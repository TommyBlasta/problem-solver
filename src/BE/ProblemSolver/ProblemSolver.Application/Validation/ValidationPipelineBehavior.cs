using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProblemSolver.Application.Validation
{
    public class ValidationPipelineBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult> where TRequest : IValidableQuery
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResult>> _logger;

        public ValidationPipelineBehavior(IServiceProvider serviceProvider,
            ILogger<ValidationPipelineBehavior<TRequest, TResult>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<TRequest>>();

            if (validator == null)
            {
                return next();
            }

            if (request is not TRequest validableObject)
            {
                return next();
            }

            var result = validator.Validate(validableObject);

            if (result.IsValid)
            {
                return next();
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
