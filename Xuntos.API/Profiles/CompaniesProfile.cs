// Mapping from source object to destination object and vice versa
using AutoMapper;
using Xuntos.API.Dtos;
using Xuntos.API.Models;

namespace Xuntos.API.Profiles
{
    public class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            // Source -> Target
            CreateMap<Company, CompanyReadDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();
            CreateMap<Company, CompanyUpdateDto>();
        }
    }
}
