using OnBoardingSIGDB1.Models.Classes;
using System;

namespace OnboardingSIGDB1.Models.Classes
{
    public class FuncionarioCargo : EntityValidator
    {
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
        public DateTime DataVinculo { get; set; }

    }
}
