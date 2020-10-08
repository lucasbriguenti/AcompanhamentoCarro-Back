using System;

namespace OnboardingSIGDB1.Domain.Dto
{
    public class EmpresaFiltro
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataInicioFundacao { get; set; }
        public DateTime? DataFimFundacao { get; set; }
    }
}
