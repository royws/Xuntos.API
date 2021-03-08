// Mapping from source object to destination object and vice versa
using AutoMapper;
using Xuntos.API.Dtos;
using Xuntos.API.Models;

namespace Xuntos.API.Profiles
{
    public class TechniquesProfile : Profile
    {
        public TechniquesProfile()
        {
            // Source -> Target
            CreateMap<Technique, TechniqueReadDto>();
            CreateMap<TechniqueCreateDto, Technique>();
            CreateMap<TechniqueUpdateDto, Technique>();
            CreateMap<Technique, TechniqueUpdateDto>();
        }
    }
}
