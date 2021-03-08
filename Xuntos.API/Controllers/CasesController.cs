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
    [Route("api/cases")]
    // Default API behaviours
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly ICaseRepo _repository;
        private readonly ICompanyRepo _companyrepository;
        private readonly IMapper _mapper;

        public CasesController(ICaseRepo repository, ICompanyRepo companyrepository, IMapper mapper)
        {
            _repository = repository;
            _companyrepository = companyrepository;
            _mapper = mapper;
        }

        // GET api/cases
        [HttpGet]
        public ActionResult<IEnumerable<CaseReadDto>> GetAllCases()
        {
            var caseItems = _repository.GetAllCases();
            return Ok(_mapper.Map<IEnumerable<CaseReadDto>>(caseItems));
        }

        // GET api/cases/{id}
        [HttpGet("{id}", Name = "GetCaseById")]
        public ActionResult<CaseReadDto> GetCaseById(Guid id)
        {
            var caseItem = _repository.GetCaseById(id);
            // If found, return Model mapped as DTO object
            if (caseItem != null)
            {
                return Ok(_mapper.Map<CaseReadDto>(caseItem));
            }
            return NotFound();
        }

        // POST api/cases
        [HttpPost]
        public ActionResult<CaseCreateDto> CreateCase(CaseCreateDto caseCreateDto)
        {
            // Map Dto to Model
            var caseModel = _mapper.Map<Case>(caseCreateDto);
            if (!companyExists(caseModel.CompanyId))
            {
                return NotFound("CompanyId invalid");
            };
            _repository.CreateCase(caseModel);
            _repository.SaveChanges();

            var caseReadDto = _mapper.Map<CaseReadDto>(caseModel);

            // CreatedAtRoute creates 201 created with specified values. 
            return CreatedAtRoute(nameof(GetCaseById), new { Id = caseReadDto.Id }, caseReadDto); 
        }

        // PUT /api/cases/{id}
        [HttpPut("{id}")]
        public ActionResult<CaseCreateDto> UpdateCase(Guid id, CaseUpdateDto caseUpdateDto)
        {
            var caseModelFromRepo = _repository.GetCaseById(id);
            if (caseModelFromRepo != null)
            {
                //Dbcontext tracks changes mapped with the DTO class
                _mapper.Map(caseUpdateDto, caseModelFromRepo);

                if (!companyExists(caseModelFromRepo.CompanyId)){
                    return NotFound("CompanyId invalid"); };

                _repository.UpdateCase(caseModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        // PATCH /api/cases/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCaseUpdate(Guid id, JsonPatchDocument<CaseUpdateDto> patchDoc)
        {
            var caseModelFromRepo = _repository.GetCaseById(id);
            if (caseModelFromRepo != null)
            {
                var caseToPatch = _mapper.Map<CaseUpdateDto>(caseModelFromRepo);
                try
                {
                    patchDoc.ApplyTo(caseToPatch, ModelState);
                }
                catch (ArgumentNullException)
                {
                    return NotFound("Value cannot be null. (Parameter 'path')");
                }

                if (!TryValidateModel(caseToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(caseToPatch, caseModelFromRepo);

                if (!companyExists(caseModelFromRepo.CompanyId))
                {
                    return NotFound("CompanyId invalid");
                };

                _repository.UpdateCase(caseModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        //DELETE /api/cases/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCase(Guid id)
        {
            var caseModelFromRepo = _repository.GetCaseById(id);
            if (caseModelFromRepo != null)
            {
                _repository.DeleteCase(caseModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        private bool companyExists(Guid companyId)
        {
            var checkCompanyId = _companyrepository.GetCompanyById(companyId);
            if (checkCompanyId == null)
            {
                return false;
            }
            return true;
        }
    }
}
