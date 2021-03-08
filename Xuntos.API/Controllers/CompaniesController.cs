using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xuntos.API.Data;
using Xuntos.API.Dtos;
using Xuntos.API.Models;

namespace Xuntos.API.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/companies")]
    // Default API behaviours
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepo _repository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/companies
        [HttpGet]
        public ActionResult<IEnumerable<CompanyReadDto>> GetAllCompanies()
        {
            var companyItems = _repository.GetAllCompanies();
            return Ok(_mapper.Map<IEnumerable<CompanyReadDto>>(companyItems));
        }

        // GET api/companies/{id}
        [HttpGet("{id}", Name = "GetCompanyById")]
        public ActionResult<CompanyReadDto> GetCompanyById(Guid id)
        {
            var companyItem = _repository.GetCompanyById(id);
            // If found, return Model mapped as DTO object
            if (companyItem != null)
            {
                return Ok(_mapper.Map<CompanyReadDto>(companyItem));
            }
            return NotFound();
        }

        // POST api/companies
        [HttpPost]
        public ActionResult<CompanyCreateDto> CreateCompany(CompanyCreateDto companyCreateDto)
        {
            // Map Dto to Model
            var companyModel = _mapper.Map<Company>(companyCreateDto);
            _repository.CreateCompany(companyModel);
            _repository.SaveChanges();

            var companyReadDto = _mapper.Map<CompanyReadDto>(companyModel);

            // CreatedAtRoute creates 201 created with specified values. 
            return CreatedAtRoute(nameof(GetCompanyById), new { Id = companyReadDto.Id }, companyReadDto);
        }

        // PUT /api/companies/{id}
        [HttpPut("{id}")]
        public ActionResult<CompanyCreateDto> UpdateCompany(Guid id, CompanyUpdateDto companyUpdateDto)
        {
            var companyModelFromRepo = _repository.GetCompanyById(id);
            if (companyModelFromRepo != null)
            {
                //Dbcontext tracks changes mapped with the DTO class
                _mapper.Map(companyUpdateDto, companyModelFromRepo);

                _repository.UpdateCompany(companyModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        // PATCH /api/companies/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCompanyUpdate(Guid id, JsonPatchDocument<CompanyUpdateDto> patchDoc)
        {
            var companyModelFromRepo = _repository.GetCompanyById(id);
            if (companyModelFromRepo != null)
            {
                var companyToPatch = _mapper.Map<CompanyUpdateDto>(companyModelFromRepo);
                patchDoc.ApplyTo(companyToPatch, ModelState);

                if (!TryValidateModel(companyToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(companyToPatch, companyModelFromRepo);

                _repository.UpdateCompany(companyModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        //DELETE /api/companies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCompany(Guid id)
        {
            var companyModelFromRepo = _repository.GetCompanyById(id);
            if (companyModelFromRepo != null)
            {
                _repository.DeleteCompany(companyModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}
