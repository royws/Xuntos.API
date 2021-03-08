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
    [Route("api/techniques")]
    // Default API behaviours
    [ApiController]
    public class TechniquesController : ControllerBase
    {
        private readonly ITechniqueRepo _repository;
        private readonly IMapper _mapper;

        public TechniquesController(ITechniqueRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/techniques
        [HttpGet]
        public ActionResult<IEnumerable<TechniqueReadDto>> GetAllTechniques()
        {
            var techniqueItems = _repository.GetAllTechniques();
            return Ok(_mapper.Map<IEnumerable<TechniqueReadDto>>(techniqueItems));
        }

        // GET api/techniques/{id}
        [HttpGet("{id}", Name = "GetTechniqueById")]
        public ActionResult<TechniqueReadDto> GetTechniqueById(Guid id)
        {
            var techniqueItem = _repository.GetTechniqueById(id);
            // If found, return Model mapped as DTO object
            if (techniqueItem != null)
            {
                return Ok(_mapper.Map<TechniqueReadDto>(techniqueItem));
            }
            return NotFound();
        }

        // POST api/techniques
        [HttpPost]
        public ActionResult<TechniqueCreateDto> CreateTechnique(TechniqueCreateDto techniqueCreateDto)
        {
            // Map Dto to Model
            var techniqueModel = _mapper.Map<Technique>(techniqueCreateDto);
            _repository.CreateTechnique(techniqueModel);
            _repository.SaveChanges();

            var techniqueReadDto = _mapper.Map<TechniqueReadDto>(techniqueModel);

            // CreatedAtRoute creates 201 created with specified values. 
            return CreatedAtRoute(nameof(GetTechniqueById), new { Id = techniqueReadDto.Id }, techniqueReadDto);
        }

        // PUT /api/techniques/{id}
        [HttpPut("{id}")]
        public ActionResult<TechniqueCreateDto> UpdateTechnique(Guid id, TechniqueUpdateDto techniqueUpdateDto)
        {
            var techniqueModelFromRepo = _repository.GetTechniqueById(id);
            if (techniqueModelFromRepo != null)
            {
                //Dbcontext tracks changes mapped with the DTO class
                _mapper.Map(techniqueUpdateDto, techniqueModelFromRepo);

                _repository.UpdateTechnique(techniqueModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        // PATCH /api/techniques/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialTechniqueUpdate(Guid id, JsonPatchDocument<TechniqueUpdateDto> patchDoc)
        {
            var techniqueModelFromRepo = _repository.GetTechniqueById(id);
            if (techniqueModelFromRepo != null)
            {
                var techniqueToPatch = _mapper.Map<TechniqueUpdateDto>(techniqueModelFromRepo);
                patchDoc.ApplyTo(techniqueToPatch, ModelState);

                if (!TryValidateModel(techniqueToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(techniqueToPatch, techniqueModelFromRepo);

                _repository.UpdateTechnique(techniqueModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }

        //DELETE /api/techniques/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTechnique(Guid id)
        {
            var techniqueModelFromRepo = _repository.GetTechniqueById(id);
            if (techniqueModelFromRepo != null)
            {
                _repository.DeleteTechnique(techniqueModelFromRepo);
                _repository.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
    }
}
