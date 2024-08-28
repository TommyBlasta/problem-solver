using MediatR;

namespace ProblemSolver.Application.CQRS.Advent._2020.Day1
{
    public class GetMultiplicationOfSumTargetQuery : IRequest<GetMultiplicationOfSumTargetResult>
    {
        public int NumberOfSummands { get; set; } = 2;
        public int Target { get; set; } = 2020;
        public List<int> Input { get; set; } = new List<int>();
    }

    //https://adventofcode.com/2020/day/1
    public class GetMultiplicationOfSumTargetQueryHandler : IRequestHandler<GetMultiplicationOfSumTargetQuery, GetMultiplicationOfSumTargetResult>
    {
        public async Task<GetMultiplicationOfSumTargetResult> Handle(GetMultiplicationOfSumTargetQuery request, CancellationToken cancellationToken)
        {
            var sumNumbers = FindSumNumbers(request.Input.ToArray(), request.Target, request.NumberOfSummands);

            return new GetMultiplicationOfSumTargetResult()
            {
                Numbers = sumNumbers,
                Result = sumNumbers.Aggregate((x, y) => x * y)
            };
        }

        private List<int> FindSumNumbers(int[] numbersInArray, int target, int numberOfSummands)
        {
            numbersInArray = numbersInArray.Where(x => x < target).ToArray();

            var combinations = GetCombinations(numbersInArray, numberOfSummands);

            foreach (var combination in combinations)
            {
                if (combination.Sum() == target)
                {
                    return combination;
                }
            }
            throw new Exception("No combination found");
        }

        public static List<List<T>> GetCombinations<T>(T[] array, int combinationLength)
        {
            var result = new List<List<T>>();
            GenerateCombinations(array, combinationLength, 0, new List<T>(), result);
            return result;
        }

        private static void GenerateCombinations<T>(T[] array, int combinationLength, int start, List<T> currentCombination, List<List<T>> result)
        {
            if (currentCombination.Count == combinationLength)
            {
                result.Add(new List<T>(currentCombination));
                return;
            }

            for (int i = start; i < array.Length; i++)
            {
                currentCombination.Add(array[i]);
                GenerateCombinations(array, combinationLength, i + 1, currentCombination, result);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }
}
