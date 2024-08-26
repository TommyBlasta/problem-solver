using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProblemSolver.Controllers
{
    [ApiController]
    public class BaseController
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        public BaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }
    }
}
