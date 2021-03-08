using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Xuntos.API.Models
{
    public class Case
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Company Company { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<CaseTechnique> CaseTechniques { get; set; }
    }
}
