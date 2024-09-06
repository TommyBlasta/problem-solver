using MediatR;
using ProblemSolver.Application.Validation;

namespace ProblemSolver.Application.CQRS.Advent._2020.Day2
{
    public class GetValidPasswordsQuery : IRequest<GetValidPasswordsResult>, IValidableQuery
    {
        public PasswordCheckType PasswordCheckType { get; set; }
        public List<PasswordDefinition> Passwords { get; set; } = new List<PasswordDefinition>();

        public class PasswordDefinition
        {
            public char RequiredCharacter { get; set; }
            public int LowNumber { get; set; }
            public int HighNumber { get; set; }
            public string Password { get; set; } = string.Empty;
        }
    }

    //https://adventofcode.com/2020/day/2
    public class GetValidPasswordsQueryHandler : IRequestHandler<GetValidPasswordsQuery, GetValidPasswordsResult>
    {
        public async Task<GetValidPasswordsResult> Handle(GetValidPasswordsQuery request, CancellationToken cancellationToken)
        {
            switch (request.PasswordCheckType)
            {
                case PasswordCheckType.SledRental:
                    {
                        return new GetValidPasswordsResult { ValidPasswordsCount = await EvaluateSledRental(request) };
                    }
                case PasswordCheckType.Otoas:
                    {
                        return new GetValidPasswordsResult { ValidPasswordsCount = await EvaluateOtoas(request) };
                    }
                default: throw new NotImplementedException("PasswordCheckType not implemented.");
            }
        }
        private Task<int> EvaluateSledRental(GetValidPasswordsQuery request)
        {
            var validPasswords = 0;

            foreach (var password in request.Passwords)
            {
                var grouppedByChar = password.Password.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());

                if (grouppedByChar.TryGetValue(password.RequiredCharacter, out var count) && count >= password.LowNumber && count <= password.HighNumber)
                {
                    validPasswords++;
                }
            }

            return Task.FromResult(validPasswords);
        }

        private Task<int> EvaluateOtoas(GetValidPasswordsQuery request)
        {
            var validPasswords = 0;

            foreach (var password in request.Passwords)
            {
                if (password.Password.ToArray()[password.LowNumber - 1] == password.RequiredCharacter &&
                    password.Password.ToArray()[password.HighNumber - 1] == password.RequiredCharacter)
                {
                    validPasswords++;
                }
            }

            return Task.FromResult(validPasswords);
        }
    }
}
