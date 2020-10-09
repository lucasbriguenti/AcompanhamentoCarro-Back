using System;

namespace OnboardingSIGDB1.Domain.Dto
{
    public class FuncionarioFiltro
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataInicioContratacao { get; set; }
        public DateTime? DataFimContratacao { get; set; }

        public bool IsNull => Nome == null && Cpf == null && DataInicioContratacao == null && DataFimContratacao == null;
    }
}
