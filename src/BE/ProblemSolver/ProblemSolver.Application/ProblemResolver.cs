namespace ProblemSolver.Application
{
    public class ProblemResolver : IProblemResolver
    {
        public Type GetProblemQuery(int problemId)
        {
            return MapIdToQuery(problemId);
        }

        private Type MapIdToQuery(int problemId)
        {
            return problemId switch
            {
                1 => typeof(CQRS.Euler.GetPrimeSum.GetPrimeSumQuery),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
