using OnboardingSIGDB1.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnboardingSIGDB1.Domain.Models
{
    public class Empresa : Entity
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Nome { get; set; }
        [NotMapped]
        private string _cnpj { get; set; }
        [MaxLength(14)]
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value.LimpaMascaraCnpjCpf(); }
        }
        public DateTime? DataFundacao { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
