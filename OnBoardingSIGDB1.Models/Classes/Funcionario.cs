using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Models.Classes
{
    public class Funcionario : Entity
    {
        [MaxLength(150)]
        public string Nome { get; set; }
        [NotMapped]
        private string _cpf { get; set; }
        [MaxLength(11)]
        public string Cpf {
            get { return _cpf; }
            set { _cpf = LimpaMascaraCnpjCpf(value); }
        }
        public DateTime? DataContratacao { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<FuncionarioCargo> FuncionarioCargos { get; set; }
        private string LimpaMascaraCnpjCpf(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
