using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Models.Classes
{
    public class Empresa : Entity
    {
        [MaxLength(150)]
        public string Nome { get; set; }
        [NotMapped]
        private string _cnpj { get; set; }
        [MaxLength(14)]
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = LimpaMascaraCnpjCpf(value); }
        }
        public DateTime? DataFundacao { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
        private string LimpaMascaraCnpjCpf(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
