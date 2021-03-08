using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class SqlCompanyRepo : ICompanyRepo
    {
        private readonly ApiDbContext _context;

        public SqlCompanyRepo(ApiDbContext context)
        {
            _context = context;
        }

        public void CreateCompany(Company companyToCreate)
        {
            if (companyToCreate == null)
            {
                throw new ArgumentNullException(nameof(companyToCreate));
            }

            _context.Companies.Add(companyToCreate);
        }

        public void DeleteCompany(Company companyToDelete)
        {
            if (companyToDelete == null)
            {
                throw new ArgumentNullException();
            }
            _context.Companies.Remove(companyToDelete);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _context.Companies.Include(c => c.Cases).ToList();
        }

        public Company GetCompanyById(Guid id)
        {
            return _context.Companies.Include(c => c.Cases).FirstOrDefault(p => p.Id == id);
        }
        // DbContext only changes data in db after calling SaveChanges
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void UpdateCompany(Company companyToUpdate)
        {
            //Nothing
        }
    }
}
