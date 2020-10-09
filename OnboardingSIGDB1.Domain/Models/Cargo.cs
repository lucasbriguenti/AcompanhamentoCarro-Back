using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Cargo : Entity
    {
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Descricao { get; set; }

        public ICollection<FuncionarioCargo> FuncionarioCargos { get; set; }
    }
}
