using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class SqlCaseTechniqueRepo : ICaseTechniqueRepo
    {
        private readonly ApiDbContext _context;

        public SqlCaseTechniqueRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateCaseTechnique(CaseTechnique caseTechniqueToCreate)
        {
            if (caseTechniqueToCreate == null)
            {
                throw new ArgumentNullException(nameof(caseTechniqueToCreate));
            }

            _context.CaseTechniques.Add(caseTechniqueToCreate);
        }

        public void DeleteCaseTechnique(CaseTechnique caseTechniqueToDelete)
        {
            if (caseTechniqueToDelete == null)
            {
                throw new ArgumentNullException();
            }
            _context.CaseTechniques.Remove(caseTechniqueToDelete);
        }

        public IEnumerable<CaseTechnique> GetAllCaseTechniques()
        {
            return _context.CaseTechniques.Include(c => c.Case).Include(t => t.Technique).ToList();
        }

        public CaseTechnique GetCaseTechniqueById(Guid id)
        {
            return _context.CaseTechniques.Include(c => c.Case).Include(t => t.Technique).FirstOrDefault(p => p.Id == id);
        }
        // DbContext only changes data in db after calling SaveChanges
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void UpdateCaseTechnique(CaseTechnique caseTechniqueToUpdate)
        {
            //Nothing
        }
    }
}
