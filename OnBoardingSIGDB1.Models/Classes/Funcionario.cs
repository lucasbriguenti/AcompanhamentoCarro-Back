using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace OnboardingSIGDB1.Models.Classes
{
    [DataContract]
    public class Funcionario : Entity
    {
        [DataMember]
        [MaxLength(150)]
        public string Nome { get; set; }
        [NotMapped]
        private string _cpf { get; set; }
        [DataMember]
        [MaxLength(11)]
        public string Cpf {
            get => _cpf;
            set => _cpf = LimpaMascaraCnpjCpf(value);
        }
        private string LimpaMascaraCnpjCpf(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
