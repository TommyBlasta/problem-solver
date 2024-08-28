using System.Text.Json;

namespace ProblemSolver.Contract.Problems
{
    public class ProblemInputDto
    {
        public int ProblemId { get; set; }
        public JsonElement InputData { get; set; }
    }
}
