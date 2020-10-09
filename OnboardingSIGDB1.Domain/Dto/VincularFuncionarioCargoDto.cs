using System;

namespace OnboardingSIGDB1.Domain.Dto
{
    public class VincularFuncionarioCargoDto
    {
        public int FuncionarioId { get; set; }
        public int CargoId { get; set; }
        public DateTime DataVinculo { get; set; }
    }
}
