using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Xuntos.API.Models
{
    public class CaseTechnique
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [JsonIgnore]
        [IgnoreDataMember]
        public Guid CaseId { get; set; }

        [Required]
        [JsonIgnore]
        [IgnoreDataMember]
        public Case Case { get; set; }

        [Required]
        [JsonIgnore]
        [IgnoreDataMember]
        public Guid TechniqueId { get; set; }

        public Technique Technique { get; set; }
    }
}