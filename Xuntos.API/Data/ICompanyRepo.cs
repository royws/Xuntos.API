using System;
using System.Collections.Generic;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public interface ICompanyRepo
    {
        bool SaveChanges();

        IEnumerable<Company> GetAllCompanies();
        Company GetCompanyById(Guid id);
        void CreateCompany(Company companyToCreate);
        void UpdateCompany(Company companyToUpdate);
        void DeleteCompany(Company companyToDelete);
    }
}
