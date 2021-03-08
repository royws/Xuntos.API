using System;
using System.Collections.Generic;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public interface ITechniqueRepo
    {
        bool SaveChanges();

        IEnumerable<Technique> GetAllTechniques();
        Technique GetTechniqueById(Guid id);
        void CreateTechnique(Technique techniqueToCreate);
        void UpdateTechnique(Technique techniqueToUpdate);
        void DeleteTechnique(Technique techniqueToDelete);
    }
}
