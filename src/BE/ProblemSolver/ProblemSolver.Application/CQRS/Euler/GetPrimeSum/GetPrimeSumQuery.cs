using MediatR;

namespace ProblemSolver.Application.CQRS.Euler.GetPrimeSum
{
    public class GetPrimeSumQuery : IRequest<GetPrimeSumResult>
    {
        public int PrimeLimit { get; set; } = 2000000;
    }

    //https://projecteuler.net/problem=10
    public class GetPrimeSumQueryHandler : IRequestHandler<GetPrimeSumQuery, GetPrimeSumResult>
    {
        public Task<GetPrimeSumResult> Handle(GetPrimeSumQuery request, CancellationToken cancellationToken)
        {
            var primeLimit = request.PrimeLimit;

            //Sieve of Eratosthenes implementation
            var range = Enumerable.Range(0, primeLimit).ToDictionary(x => x, x => true);

            range[0] = false;
            range[1] = false;

            for (int i = 2; i <= Math.Sqrt(primeLimit); i++)
            {
                if (range[i])
                {
                    for (int j = i * i; j < primeLimit; j += i)
                    {
                        range[j] = false;
                    }
                }
            }

            var primes = range.Where(x => x.Value).Select(x => x.Key);

            //Sum of all primes
            long result = 0;

            foreach (var prime in primes)
            {
                result += prime;
            }

            return Task.FromResult(new GetPrimeSumResult { Result = result });
        }
    }
}
