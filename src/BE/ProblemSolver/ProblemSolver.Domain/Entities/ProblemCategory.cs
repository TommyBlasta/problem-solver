namespace ProblemSolver.Domain.Entities
{
    public class ProblemCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Year { get; set; }

        public List<Problem> Problems { get; set; } = new List<Problem>();
    }
}
