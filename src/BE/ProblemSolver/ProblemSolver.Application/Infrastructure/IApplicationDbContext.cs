using Microsoft.EntityFrameworkCore;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Application.Infrastructure
{
    public interface IApplicationDbContext
    {
        public DbSet<Problem> Problems { get; }
        public DbSet<ProblemCategory> ProblemCategories { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
