// Implementation of IApiRepo
using System;
using System.Collections.Generic;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class MockApiRepo : ICaseRepo
    {
        public Case GetCaseById(Guid id)
        {
            return new Case { Id = new System.Guid(), Type = "Testcase", Description = "Testcase description" };
        }

        public IEnumerable<Case> GetAllCases()
        {
            var cases = new List<Case>
            {
                new Case { Id = new System.Guid(), Type = "Testcase", Description = "Testcase description" },
                new Case { Id = new System.Guid(), Type = "Second testcase", Description = "Second testcase description" },
                new Case { Id = new System.Guid(), Type = "Third case", Description = "Third description" }
            };
            return cases;
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCase(Case @case)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCase(Case caseToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCase(Case caseToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}
