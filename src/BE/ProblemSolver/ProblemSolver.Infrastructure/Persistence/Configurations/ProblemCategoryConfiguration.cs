using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Infrastructure.Persistence.Configurations;

internal class ProblemCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.ProblemCategory>
{
    public void Configure(EntityTypeBuilder<ProblemCategory> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder
            .HasMany(c => c.Problems)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
    }
}
