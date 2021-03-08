// Object that gets mapped back to the end user
using System;
using System.Collections.Generic;
using Xuntos.API.Models;

namespace Xuntos.API.Dtos
{
    public class CompanyReadDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
