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
            get => _cnpj; 
            set => LimpaMascaraCnpjCpf(value);
        }
        public DateTime? DataFundacao { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
        private void LimpaMascaraCnpjCpf(string cnpj)
        {
            _cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
