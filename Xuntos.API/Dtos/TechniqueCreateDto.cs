using System.ComponentModel.DataAnnotations;

namespace Xuntos.API.Dtos
{
    public class TechniqueCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
