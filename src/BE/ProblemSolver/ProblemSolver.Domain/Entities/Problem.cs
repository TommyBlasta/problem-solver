namespace ProblemSolver.Domain.Entities
{
    public class Problem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultInput { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }

        public ProblemCategory Category { get; set; } = new ProblemCategory();
    }
}
