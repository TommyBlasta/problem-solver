namespace ProblemSolver.Application
{
    public class ProblemResolver : IProblemResolver
    {
        public Type GetProblemQueryType(int problemId)
        {
            return MapIdToQuery(problemId);
        }

        private Type MapIdToQuery(int problemId)
        {
            return problemId switch
            {
                1 => typeof(CQRS.Euler.GetPrimeSum.GetPrimeSumQuery),
                2 => typeof(CQRS.Advent._2020.Day1.GetMultiplicationOfSumTargetQuery),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
