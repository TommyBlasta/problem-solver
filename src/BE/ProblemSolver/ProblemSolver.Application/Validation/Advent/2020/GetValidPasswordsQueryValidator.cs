using FluentValidation;
using ProblemSolver.Application.CQRS.Advent._2020.Day2;

namespace ProblemSolver.Application.Validation.Advent._2020
{
    public class GetValidPasswordsQueryValidator : AbstractValidator<GetValidPasswordsQuery>
    {
        public GetValidPasswordsQueryValidator()
        {
            RuleFor(x => x.PasswordCheckType).IsInEnum();

            RuleFor(x => x.Passwords)
                .NotEmpty();

            RuleForEach(x => x.Passwords)
                .Must(NumbersBeValidForTypeCheck);
        }

        private bool NumbersBeValidForTypeCheck(GetValidPasswordsQuery query, GetValidPasswordsQuery.PasswordDefinition passwordDefinition)
        {
            switch (query.PasswordCheckType)
            {
                case PasswordCheckType.Otoas:
                case PasswordCheckType.SledRental:
                    {
                        var minimumLength = passwordDefinition.LowNumber;
                        var maximumLength = passwordDefinition.HighNumber;

                        var passLength = passwordDefinition.Password.Length;

                        return passLength >= minimumLength && passLength <= maximumLength;
                    }
                default: return false;
            }
        }
    }
}
