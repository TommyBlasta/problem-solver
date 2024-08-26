using MediatR;

namespace ProblemSolver.Application.CQRS.Euler.GetPrimeSum
{
    public class GetPrimeSumQuery : IRequest<GetPrimeSumResult>
    {
        public int PrimeLimit { get; set; } = 2000000;
    }

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

            //Sum of all primes
            var sum = range.Where(x => x.Value).Select(x => x.Key).Sum();

            return Task.FromResult(new GetPrimeSumResult { Result = sum });
        }
    }
}
