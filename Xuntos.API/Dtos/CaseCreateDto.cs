using System;
using System.ComponentModel.DataAnnotations;

namespace Xuntos.API.Dtos
{
    public class CaseCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
}
