using OnboardingSIGDB1.Models.Classes;
using System.Runtime.Serialization;

namespace OnBoardingSIGDB1.Models.Classes
{
    [DataContract]
    public class RegistroKilometragem : Entity
    {
        [DataMember]
        public virtual Carro Carro { get; set; }
        public int CarroId { get; set; }
        [DataMember]
        public double KilometragemRegistrada { get; set; }
        [DataMember]
        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }
    }
}
