using AutoMapper;
using ProblemSolver.Application.CQRS.Euler.GetPrimeSum;
using ProblemSolver.Contract;
using ProblemSolver.Domain.Entities;

namespace ProblemSolver.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetPrimeSumResult, ProblemResultDto>()
                .ForMember(d => d.Result, o => o.MapFrom(s => s.Result));

            CreateMap<ProblemInputDto, GetPrimeSumQuery>()
                .ForMember(d => d.PrimeLimit, o => o.MapFrom(s => Convert.ToInt32(s.Input)));

            CreateMap<Problem, ProblemDto>();
        }
    }
}
