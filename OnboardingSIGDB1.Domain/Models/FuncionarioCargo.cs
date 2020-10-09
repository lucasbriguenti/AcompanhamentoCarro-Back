using System;

namespace OnboardingSIGDB1.Domain.Models
{
    public class FuncionarioCargo : Entity
    {
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public DateTime DataVinculo { get; set; }
    }
}
