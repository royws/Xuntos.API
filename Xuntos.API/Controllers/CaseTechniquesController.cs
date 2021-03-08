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
    [Route("api/caseTechniques")]
    // Default API behaviours
    [ApiController]
    public class CaseTechniquesController : ControllerBase
    {
        private readonly ICaseTechniqueRepo _repository;
        private readonly ICaseRepo _caserepository;
        private readonly ITechniqueRepo _techniquerepository;
        private readonly IMapper _mapper;

        public CaseTechniquesController(ICaseTechniqueRepo repository, ICaseRepo caserepository, ITechniqueRepo techniquerepository, IMapper mapper)
        {
            _repository = repository;
            _caserepository = caserepository;
            _techniquerepository = techniquerepository;
            _mapper = mapper;
        }

        // GET api/casetechniques
        [HttpGet]
        public ActionResult<IEnumerable<CaseTechniqueReadDto>> GetAllCaseTechniques()
        {
            var caseTechniqueItems = _repository.GetAllCaseTechniques();
            return Ok(_mapper.Map<IEnumerable<CaseTechniqueReadDto>>(caseTechniqueItems));
        }

        // GET api/casetechniques/{id}
        [HttpGet("{id}", Name = "GetCaseTechniqueById")]
        public ActionResult<CaseTechniqueReadDto> GetCaseTechniqueById(Guid id)
        {
            var caseTechniqueItem = _repository.GetCaseTechniqueById(id);
            // If found, return Model mapped as DTO object
            if (caseTechniqueItem != null)
            {
                return Ok(_mapper.Map<CaseTechniqueReadDto>(caseTechniqueItem));
            }
            return NotFound();
        }

        // POST api/casetechniques
        [HttpPost]
        public ActionResult<CaseTechniqueCreateDto> CreateCaseTechnique(CaseTechniqueCreateDto caseTechniqueCreateDto)
        {
            // Map Dto to Model
            var caseTechniqueModel = _mapper.Map<CaseTechnique>(caseTechniqueCreateDto);

            var checkCaseId = _caserepository.GetCaseById(caseTechniqueModel.CaseId);
            if (checkCaseId == null) { return NotFound("CaseId invalid"); }
            var checkTechniqueId = _techniquerepository.GetTechniqueById(caseTechniqueModel.TechniqueId);
            if (checkTechniqueId == null) { return NotFound("TechniqueId invalid"); }

            _repository.CreateCaseTechnique(caseTechniqueModel);
            _repository.SaveChanges();

            var caseTechniqueReadDto = _mapper.Map<CaseTechniqueReadDto>(caseTechniqueModel);

            // CreatedAtRoute creates 201 created with specified values. 
            return CreatedAtRoute(nameof(GetCaseTechniqueById), new { Id = caseTechniqueReadDto.Id }, caseTechniqueReadDto);
        }

        // PUT /api/casetechniques/{id}
        [HttpPut("{id}")]
        public ActionResult<CaseTechniqueCreateDto> UpdateCaseTechnique(Guid id, CaseTechniqueUpdateDto caseTechniqueUpdateDto)
        {
            var caseTechniqueModelFromRepo = _repository.GetCaseTechniqueById(id);
            if (caseTechniqueModelFromRepo != null)
            {
                //Dbcontext tracks changes mapped with the DTO class
                _mapper.Map(caseTechniqueUpdateDto, caseTechniqueModelFromRepo);

                _repository.UpdateCaseTechnique(caseTechniqueModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        // PATCH /api/casetechniques/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCaseTechniqueUpdate(Guid id, JsonPatchDocument<CaseTechniqueUpdateDto> patchDoc)
        {
            var caseTechniqueModelFromRepo = _repository.GetCaseTechniqueById(id);
            if (caseTechniqueModelFromRepo != null)
            {
                var caseTechniqueToPatch = _mapper.Map<CaseTechniqueUpdateDto>(caseTechniqueModelFromRepo);
                patchDoc.ApplyTo(caseTechniqueToPatch, ModelState);

                if (!TryValidateModel(caseTechniqueToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(caseTechniqueToPatch, caseTechniqueModelFromRepo);

                _repository.UpdateCaseTechnique(caseTechniqueModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        //DELETE /api/casetechniques/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCaseTechnique(Guid id)
        {
            var caseTechniqueModelFromRepo = _repository.GetCaseTechniqueById(id);
            if (caseTechniqueModelFromRepo != null)
            {
                _repository.DeleteCaseTechnique(caseTechniqueModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}
