using AutoMapper;
using ProblemSolver.Application.CQRS.Advent._2020.Day1;
using ProblemSolver.Application.CQRS.Advent._2020.Day2;
using ProblemSolver.Application.CQRS.Euler.GetPrimeSum;
using ProblemSolver.Application.CQRS.Problems.Commands.CreateProblem;
using ProblemSolver.Contract;
using ProblemSolver.Contract.Problems;
using ProblemSolver.Domain.Entities;
using System.Text.Json;

namespace ProblemSolver.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetPrimeSumResult, ProblemResultDto>()
                .ForMember(d => d.Result, o => o.MapFrom(s => s.Result));

            CreateMap<ProblemInputDto, GetPrimeSumQuery>()
                .ConstructUsing((i) => DeserializeToType<GetPrimeSumQuery>(i));

            CreateMap<GetMultiplicationOfSumTargetResult, ProblemResultDto>()
                .ForMember(d => d.Result, o => o.MapFrom(s => s.Result));

            CreateMap<ProblemInputDto, GetMultiplicationOfSumTargetQuery>()
                .ConstructUsing((i) => DeserializeToType<GetMultiplicationOfSumTargetQuery>(i));

            CreateMap<ProblemInputDto, GetValidPasswordsQuery>()
                .ConstructUsing((i) => DeserializeToType<GetValidPasswordsQuery>(i));

            CreateMap<GetValidPasswordsResult, ProblemResultDto>()
                .ForMember(d => d.Result, o => o.MapFrom(s => s.ValidPasswordsCount));

            CreateMap<Problem, ProblemDto>();

            CreateMap<ProblemCreationInputDto, CreateProblemCommand>();
        }

        private T DeserializeToType<T>(ProblemInputDto input) where T : new()
        {
            return input.InputData.Deserialize<T>() ?? new T();
        }
    }
}
