// Mapping from source object to destination object and vice versa
using AutoMapper;
using Xuntos.API.Dtos;
using Xuntos.API.Models;

namespace Xuntos.API.Profiles
{
    public class CasesProfile : Profile
    {
        public CasesProfile()
        {
            // Source -> Target
            CreateMap<Case, CaseReadDto>();
            CreateMap<CaseCreateDto, Case>();
            CreateMap<CaseUpdateDto, Case>();
            CreateMap<Case, CaseUpdateDto>();
        }
    }
}
