using OnboardingSIGDB1.Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [NotMapped]
        private string _cnpj { get; set; }
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value.LimpaMascaraCnpj(); }
        }
        public DateTime? DataFundacao { get; set; }

    }
}
