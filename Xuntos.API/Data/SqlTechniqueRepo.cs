using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class SqlTechniqueRepo : ITechniqueRepo
    {
        private readonly ApiDbContext _context;

        public SqlTechniqueRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateTechnique(Technique techniqueToCreate)
        {
            if (techniqueToCreate == null)
            {
                throw new ArgumentNullException(nameof(techniqueToCreate));
            }

            _context.Techniques.Add(techniqueToCreate);
        }

        public void DeleteTechnique(Technique techniqueToDelete)
        {
            if (techniqueToDelete == null)
            {
                throw new ArgumentNullException();
            }
            _context.Techniques.Remove(techniqueToDelete);
        }

        public IEnumerable<Technique> GetAllTechniques()
        {
            return _context.Techniques.Include(c => c.CaseTechniques).ToList();
        }

        public Technique GetTechniqueById(Guid id)
        {
            return _context.Techniques.Include(c => c.CaseTechniques).FirstOrDefault(p => p.Id == id);
        }
        // DbContext only changes data in db after calling SaveChanges
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void UpdateTechnique(Technique techniqueToUpdate)
        {
            //Nothing
        }
    }
}
