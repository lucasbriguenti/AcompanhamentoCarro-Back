using OnboardingSIGDB1.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Funcionario : Entity
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Nome { get; set; }
        [NotMapped]
        private string _cpf { get; set; }
        [MaxLength(11)]
        public string Cpf {
            get { return _cpf; }
            set { _cpf = value.LimpaMascaraCnpjCpf(); }
        }
        public DateTime? DataContratacao { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<FuncionarioCargo> FuncionarioCargos { get; set; }
    }
}
