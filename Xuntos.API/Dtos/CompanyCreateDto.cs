using System.ComponentModel.DataAnnotations;

namespace Xuntos.API.Dtos
{
    public class CompanyCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
