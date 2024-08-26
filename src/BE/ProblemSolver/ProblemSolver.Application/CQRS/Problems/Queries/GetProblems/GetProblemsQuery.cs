using MediatR;
using Microsoft.EntityFrameworkCore;
using ProblemSolver.Application.Infrastructure;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Application.CQRS.Problems.Queries.GetProblems
{
    public class GetProblemsQuery : IRequest<List<Problem>>
    {
        public IEnumerable<Guid>? GuidFilter { get; set; }
    }

    public class GetProblemsQueryHandler : IRequestHandler<GetProblemsQuery, List<Problem>>
    {
        private readonly IApplicationDbContext _context;

        public GetProblemsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Problem>> Handle(GetProblemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Problems.AsQueryable();

            if (request.GuidFilter != null)
            {
                query = query.Where(x => request.GuidFilter.Contains(x.Id));
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
