using Microsoft.EntityFrameworkCore;
using ProblemSolver.Application.Infrastructure;
using System.Reflection;

namespace ProblemSolver.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("problem_solver");
    }

    public DbSet<Domain.Entities.Problem> Problems => Set<Domain.Entities.Problem>();
    public DbSet<Domain.Entities.ProblemCategory> ProblemCategories => Set<Domain.Entities.ProblemCategory>();

    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}
