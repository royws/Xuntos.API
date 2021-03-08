// Mapping from source object to destination object and vice versa
using AutoMapper;
using Xuntos.API.Dtos;
using Xuntos.API.Models;

namespace Xuntos.API.Profiles
{
    public class CaseTechniquesProfile : Profile
    {
        public CaseTechniquesProfile()
        {
            // Source -> Target
            CreateMap<CaseTechnique, CaseTechniqueReadDto>();
            CreateMap<CaseTechniqueCreateDto, CaseTechnique>();
            CreateMap<CaseTechniqueUpdateDto, CaseTechnique>();
            CreateMap<CaseTechnique, CaseTechniqueUpdateDto>();
        }
    }
}
