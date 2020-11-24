using OnboardingSIGDB1.Models.Classes;
using System.Runtime.Serialization;

namespace OnBoardingSIGDB1.Models.Classes
{
    [DataContract]
    public class Carro : Entity
    {
        [DataMember]
        public string Marca { get; set; }
        [DataMember]
        public string Modelo { get; set; }
        [DataMember]
        public string Placa { get; set; }
        [DataMember]
        public double Kilometragem { get; set; }
    }
}
