using System;

namespace Xuntos.API.Dtos
{
    public class CaseTechniqueReadDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CaseId { get; set; }
        public Guid TechniqueId { get; set; }
    }
}
