using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Models.Classes
{
    public class Cargo : Entity
    {
        [MaxLength(250)]
        [Required]
        public string Descricao { get; set; }

        public ICollection<FuncionarioCargo> FuncionarioCargos { get; set; }

    }
}
