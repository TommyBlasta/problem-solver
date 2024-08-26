using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Infrastructure.Persistence.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Domain.Entities.Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder
           .HasOne(p => p.Category)
           .WithMany(c => c.Problems)
           .HasForeignKey(p => p.CategoryId);
    }
}

