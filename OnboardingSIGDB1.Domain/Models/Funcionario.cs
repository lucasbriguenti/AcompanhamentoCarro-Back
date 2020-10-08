using OnboardingSIGDB1.Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Funcionario
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
    }
}
