using MediatR;
using ProblemSolver.Application.Infrastructure;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Application.CQRS.Problems.Commands.CreateProblem
{
    public class CreateProblemCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultInput { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }

    public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateProblemCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Guid> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
        {
            var problem = new Problem
            {
                Name = request.Name,
                Description = request.Description,
                DefaultInput = request.DefaultInput,
                CategoryId = request.CategoryId
            };

            _applicationDbContext.Problems.Add(problem);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return problem.Id;
        }
    }
}
