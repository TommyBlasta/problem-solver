using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProblemSolver.Application;
using ProblemSolver.Application.CQRS.Problems.Commands.CreateProblem;
using ProblemSolver.Application.CQRS.Problems.Queries.GetProblems;
using ProblemSolver.Contract;
using ProblemSolver.Contract.Problems;

namespace ProblemSolver.Controllers
{
    [Route("problems")]
    public class ProblemsController : BaseController
    {
        private readonly IProblemResolver problemResolver;

        public ProblemsController(IMediator mediator, IMapper mapper, IProblemResolver problemResolver) : base(mediator, mapper)
        {
            this.problemResolver = problemResolver;
        }

        [HttpGet]
        public async Task<List<ProblemDto>> GetProblems(CancellationToken cancellationToken)
        {
            var query = new GetProblemsQuery();

            var result = await Mediator.Send(query);

            return Mapper.Map<List<ProblemDto>>(result);
        }

        [HttpPost]
        public async Task<Guid> CreateProblem(ProblemCreationInputDto problemCreationInputDto, CancellationToken cancellationToken)
        {
            var command = new CreateProblemCommand();

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPost]
        [Route("solve")]
        public async Task<ProblemResultDto> SolveProblem(ProblemInputDto input)
        {
            var query = Mapper.Map(input, typeof(ProblemInputDto), problemResolver.GetProblemQueryType(input.ProblemId));

            var result = await Mediator.Send(query);

            return Mapper.Map<ProblemResultDto>(result);
        }
    }
}
