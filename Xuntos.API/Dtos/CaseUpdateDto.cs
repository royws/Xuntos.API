using System;
using System.ComponentModel.DataAnnotations;

namespace Xuntos.API.Dtos
{
    public class CaseUpdateDto
    {
        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
    }
}
