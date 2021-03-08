using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class SqlCaseRepo : ICaseRepo
    {
        private readonly ApiDbContext _context;

        public SqlCaseRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateCase(Case caseToCreate)
        {
            if (caseToCreate == null)
            {
                throw new ArgumentNullException(nameof(caseToCreate));
            }
            _context.Cases.Add(caseToCreate);
        }

        public void DeleteCase(Case caseToDelete)
        {
            if (caseToDelete == null)
            {
                throw new ArgumentNullException();
            }
            _context.Cases.Remove(caseToDelete);
        }

        public IEnumerable<Case> GetAllCases()
        {
            return _context.Cases.Include(c => c.Company).Include(t => t.CaseTechniques).ThenInclude(t => t.Technique).ToList();
        }

        public Case GetCaseById(Guid id)
        {
            return _context.Cases.Include(c => c.Company).Include(ct => ct.CaseTechniques).ThenInclude(t => t.Technique).FirstOrDefault(p => p.Id == id);
        }
        // DbContext only changes data in db after calling SaveChanges
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void UpdateCase(Case caseToUpdate)
        {
            //Nothing
        }
    }
}
