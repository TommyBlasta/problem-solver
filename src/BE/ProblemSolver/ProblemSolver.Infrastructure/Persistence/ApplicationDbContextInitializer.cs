using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsRelational())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.ProblemCategories.Any())
        {
            var categories = new List<ProblemCategory>
            {
                new ProblemCategory { Name = "Uncategorized" },
            };

            await _context.ProblemCategories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }
    }
}
