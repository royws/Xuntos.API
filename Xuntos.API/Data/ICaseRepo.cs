using System;
using System.Collections.Generic;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public interface ICaseRepo
    {
        bool SaveChanges();

        IEnumerable<Case> GetAllCases();
        Case GetCaseById(Guid id);
        void CreateCase(Case caseToCreate);
        void UpdateCase(Case caseToUpdate);
        void DeleteCase(Case caseToDelete);
    }
}
