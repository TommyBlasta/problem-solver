namespace ProblemSolver.Contract
{
    public class ProblemCreationInputDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DefaultInput { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
